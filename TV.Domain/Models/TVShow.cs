using System.ComponentModel.DataAnnotations;

namespace TV.Domain.Models
{
    public class TVShow
    {
        public Guid TVShowId { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required]
        public required DateOnly RealeseDate { get; set; }
        [Required]
        [AllowedValues(0, 1, 2, 3, 4, 5)]
        public required int Rating { get; set; } = 1;
        [Required]
        [Url]
        public required string URL { get; set; }
        public bool IsHidden { get; set; } = false;
        public Attachment Attachment { get; set; }
        public ICollection<Languages> Languages { get; set; }
        public TVShow()
        {
            TVShowId = Guid.NewGuid();
            Languages = new List<Languages>();
        }
    }
}
