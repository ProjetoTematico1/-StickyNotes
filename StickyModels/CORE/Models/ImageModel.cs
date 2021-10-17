using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StickyNotes.CORE.Models
{
    [Table("Image")]
    public class ImageModel
    {
        [Key]
        public int cod_image { get; set; }
        public int cod_card { get; set; }
        public string path { get; set; }

        [ForeignKey("cod_card")]
        public virtual CardModel FK_Card { get; set; }
    }
}
