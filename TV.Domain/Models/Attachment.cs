using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TV.Domain.Models
{
    public class Attachment
    {
        public Guid AttachmentId { get; set; }
        [Required]
        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public required string Name { get; set; }
        [Required]
        public required string Path { get; set; }
        [Required]
        [FileExtensions]
        [AllowedValues(".jpg",".jpeg",".png")]
        public required string FileType { get; set; }
        public Guid TVShowId { get; set; }
        public Attachment()
        {
            AttachmentId = Guid.NewGuid();
            //Name = System.IO.Path.GetFileName(imagePath);
            //Path = System.IO.Path.GetFullPath(imagePath);
            //FileType = System.IO.Path.GetExtension(imagePath);
        }
        
    }
}
