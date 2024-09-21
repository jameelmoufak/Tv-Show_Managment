using System.ComponentModel.DataAnnotations;

namespace TV.Domain.Models
{
    public class Languages
    {
        public Guid LanguagesId { get; set; }
        [Required]
        public required string Name { get; set; }
        public virtual ICollection<TVShow> TVShows { get; set; }
        public Languages()
        {
            LanguagesId = Guid.NewGuid();
            TVShows = new List<TVShow>();
        }
    }
}
