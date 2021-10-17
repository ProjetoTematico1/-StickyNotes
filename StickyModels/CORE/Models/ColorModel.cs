using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StickyNotes.CORE.Models
{
    [Table("Color")]
    public class ColorModel
    {
        [Key]
        public int cod_color { get; set; }
        public string hex_text { get; set; }
        public string font_color { get; set; }
        public string stroke_color { get; set; }
    }
}