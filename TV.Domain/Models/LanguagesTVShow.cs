using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV.Domain.Models
{
    public class LanguagesTVShow
    {
        //[Column(TypeName = "primarykey")]
        public Guid id { get; set; }
        public Guid TVShowId { get; set; }
        public Guid LanguagesId { get; set; }
        public LanguagesTVShow()
        {
            id = Guid.NewGuid();
        }
    }
}
