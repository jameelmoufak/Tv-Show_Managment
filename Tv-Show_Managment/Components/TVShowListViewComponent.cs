using Microsoft.AspNetCore.Mvc;
using TV.Domain.Models;
using TV.Infrastructure.Repositories;

namespace Tv_Show_Managment.Components
{
    public class TVShowListViewComponent : ViewComponent
    {
        private readonly ITVShowRepository repository;

        public TVShowListViewComponent(ITVShowRepository repository)
        {
            this.repository = repository;
        }
        public Task<IViewComponentResult> InvokeAsync()
        {
            var TVShows = repository.GetTVShowsWithDetails();
            return Task.FromResult<IViewComponentResult>(View(TVShows));
        }
    }
}
