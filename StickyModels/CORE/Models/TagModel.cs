using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StickyNotes.CORE.Models
{
    [Table("Tag")]
    public class TagModel
    {
        [Key]
        public int cod_tag { get; set; }
        public string text { get; set; }
    }
}
