using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TV.Domain.Models;
using TV.Infrastructure.Repositories;
using Tv_Show_Managment.Models;
using Tv_Show_Managment.Repositories;

namespace Tv_Show_Managment.Controllers
{
    [Authorize]
    public class TVShowController : Controller
    {
        private readonly ITVShowRepository repository;
        private readonly IStateRepository stateRepository;
        private readonly IRepository<Languages> langRepo;

        public TVShowController(ITVShowRepository repository,
            IStateRepository stateRepository,
            IRepository<Languages> langRepo)
        {
            this.repository = repository;
            this.stateRepository = stateRepository;
            this.langRepo = langRepo;
        }
        [Route("Index/{TVShowId?}")]
        public IActionResult Index(string? TVShowId)
        {
            if (TVShowId != null)
                stateRepository.SetValue("tvshowId", TVShowId);
            else
                stateRepository.Delete("tvshowId");
            return View();
        }
        [HttpPost]
        [Route("CreateOrUpdate")]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrUpdate(TVShowModel tVShowModel)
        {
            DateOnly dateOnly = DateOnly.FromDateTime(tVShowModel.RealeseDate);
            if (!ModelState.IsValid) return View("Index");
            TVShow? tvshow;
            Guid tvshowId;
            string extension;
            var languages = new List<Languages>();
            foreach (var langId in tVShowModel.LangId)
            {
                languages.Add(langRepo.Get(Guid.Parse(langId))!);
            }
            //Update Case
            if (stateRepository.GetValue("tvshowId") != " ")
            {
                tvshow = repository.GetTvShowWithDetails(Guid.Parse(stateRepository.GetValue("tvshowId")));
                tvshow.Title = tVShowModel.Title;
                tvshow.Rating = tVShowModel.Rating;
                tvshow.RealeseDate = dateOnly;
                tvshow.URL = tVShowModel.URL;
                tvshow.Languages = languages;
                extension = Path.GetExtension(tVShowModel.File.FileName);
                tvshow.Attachment.FileType = extension;
                tvshowId = Guid.Parse(stateRepository.GetValue("tvshowId"));
                repository.Update(tvshow);
                repository.SaveChanges();
            }
            //Create Case
            else
            {
                tvshow = new TVShow
                {
                    Title = tVShowModel.Title,
                    Rating = tVShowModel.Rating,
                    RealeseDate = dateOnly,
                    URL = tVShowModel.URL,
                    Languages = languages
                };
                extension = Path.GetExtension(tVShowModel.File.FileName);
                tvshowId = tvshow.TVShowId;
                repository.CreateNewTVShow(tvshow, extension);
            }
            //for paste image in folder
            string FullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Attachments", $"{tvshow.TVShowId}{extension}");
            if (stateRepository.GetValue("tvshowId") != " ")
            {
                System.IO.File.Delete(FullPath);
            }
            Directory.CreateDirectory(Path.GetDirectoryName(FullPath));
            FileStream stream = new FileStream(FullPath, FileMode.Create);
            tVShowModel.File.CopyTo(stream);
            stream.Close();
            stateRepository.Delete("tvshowId");
            string slug = tvshow.Title.Replace(" ", "-").ToLower();
            return RedirectToAction("Details", "Home", new { tvshowId, slug} );
        }
        [Route("Remove/{id}")]
        public IActionResult Remove(Guid id)
        {
            repository.Get(id).IsHidden = true;
            repository.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
