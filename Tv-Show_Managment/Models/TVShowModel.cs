using System.ComponentModel.DataAnnotations;
using TV.Domain.Models;

namespace Tv_Show_Managment.Models
{
    public class TVShowModel
    {
        [Required]
        public required string Title { get; set; }
        [Required]
        public required DateTime RealeseDate { get; set; }
        [Required]
        [AllowedValues(0, 1, 2, 3, 4, 5)]
        public required int Rating { get; set; } = 0;
        [Required]
        [Url]
        public required string URL { get; set; }
        [Required]
        public required IList<string> LangId { get; set; }
        [Required]
        public required IFormFile File { get; set; }
        
    }
}
