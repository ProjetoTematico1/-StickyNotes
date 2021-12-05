using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StickyNotes.CORE.Models
{
    [Table("PlaceColumn")]
    public class PlaceColumnModel
    {
        [Key]
        public int cod_column { get; set; }
        public int cod_place { get; set; }
        public string title { get; set; }
        public int order { get; set; }

        [ForeignKey("cod_place")]
        public virtual PlaceModel FK_Place { get; set; }
        public virtual ICollection<CardModel> ICards { get; set; }

    }
}
