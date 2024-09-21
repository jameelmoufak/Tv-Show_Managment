using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using TV.Infrastructure.Repositories;
using Tv_Show_Managment.Attributes;
using Tv_Show_Managment.Filters;
using Tv_Show_Managment.Models;
using Tv_Show_Managment.Repositories;

namespace Tv_Show_Managment.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITVShowRepository repository;
        private readonly IStateRepository stateRepository;

        public HomeController(ILogger<HomeController> logger,
            ITVShowRepository repository,
            IStateRepository stateRepository)
        {
            _logger = logger;
            this.repository = repository;
            this.stateRepository = stateRepository;
        }

        [ServiceFilter(typeof(TimerFilter))]
        public IActionResult Index()
        {
            return View();
        }

        [TimerFilter]
        [Route("details/{TVShowId:guid}/{slug:SlugTransformer}")]
        public IActionResult Details(Guid TVShowId, [RegularExpression("^[a-zA-Z-0-9- ]+$")] string slug)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var TvShow = repository.GetTvShowWithDetails(TVShowId);
            return View(TvShow);
        }

        public IActionResult ChangLang(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)));
            new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) };
            return Redirect(Request.Headers["Refere"].ToString());
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}