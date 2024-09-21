using Microsoft.EntityFrameworkCore;
using TV.Domain.Models;

namespace TV.Infrastructure.Repositories
{
    public class TVShowRepository : GenericRepository<TVShow>, ITVShowRepository
    {
        public TVShowRepository(TVDbContext context) : base(context)
        {
        }

        public IList<TVShow> GetTVShowsWithDetails()
        {
            return context.TVShows.Include(t => t.Attachment).Include(t => t.Languages).ToList();
        }

        public TVShow GetTvShowWithDetails(Guid TVShowId)
        {
            var tvshow = context.TVShows.Include(t => t.Languages).Include(t => t.Attachment).FirstOrDefault(t => t.TVShowId == TVShowId);
            return tvshow!;

        }
        public void CreateNewTVShow(TVShow tvshow, string fileExtension)
        {
            string fileName = tvshow.TVShowId.ToString();            
            Attachment attachment = new Attachment
            {
                Name = tvshow.TVShowId.ToString(),
                FileType = fileExtension,
                Path = $"~/Attachments/{fileName}{fileExtension}"
            };
            tvshow.Attachment = attachment;
            Add(tvshow);
            SaveChanges();
        }


        public void SoftDelete(TVShow show)
        {
            show.IsHidden = true;
            Update(show);
            SaveChanges();
        }
        
    }
}
