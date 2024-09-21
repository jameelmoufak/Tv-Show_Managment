using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV.Domain.Models;

namespace TV.Infrastructure.Repositories
{
    public interface ITVShowRepository : IRepository<TVShow>
    {
        TVShow GetTvShowWithDetails(Guid TVShowId);
        IList<TVShow> GetTVShowsWithDetails();
        void CreateNewTVShow(TVShow tvshow, string fileExtension);
        void SoftDelete(TVShow show);
    }
}
