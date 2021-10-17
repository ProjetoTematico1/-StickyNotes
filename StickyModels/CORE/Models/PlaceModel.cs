using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StickyNotes.CORE.Models
{
    [Table("Place")]
    public class PlaceModel
    {
        [Key]
        public int cod_place { get; set; }
        public string title { get; set; }

        public int place_position_x { get; set; }
        public int place_position_y { get; set; }
        public virtual ICollection<CardModel> ICards { get; set; }

    }
}
