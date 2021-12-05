using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StickyNotes.CORE.Models
{
    [Table("Card")]
    public class CardModel
    {
        [Key]
        public int cod_card { get; set; }
        public int? cod_tag { get; set; }
        public int? cod_color { get; set; }
        public int? cod_column { get; set; }
        public string text { get; set; }
        public DateTime? dt_reminder { get; set; }
        public bool reminder_confirm { get; set; } = false;
        public bool open { get; set; } = false;
        public int card_position_x { get; set; }
        public int card_position_y { get; set; }

        [NotMapped]
        public CardModel thisCard
        {
            get
            {
                return this;

            }
        }

        [NotMapped]
        public String get_reminder_date
        {
            get
            {
                return this.dt_reminder.GetValueOrDefault(DateTime.Now).ToString("dd/MM/yyyy");
            }
        }
        [NotMapped]
        public String get_reminder_hour
        {
            get
            {
                return this.dt_reminder.GetValueOrDefault(DateTime.Now).ToString("HH:mm");
            }
        }

        [ForeignKey("cod_color")]
        public virtual ColorModel FK_Color { get; set; }

        [ForeignKey("cod_tag")]
        public virtual TagModel FK_Tag { get; set; }

        [ForeignKey("cod_column")]
        public virtual PlaceColumnModel FK_PlaceColumn { get; set; }

        public virtual ICollection<ImageModel> IImages { get; set; }

    }
}
