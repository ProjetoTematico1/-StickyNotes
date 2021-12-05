using Microsoft.EntityFrameworkCore;
using StickyNotes.CORE.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StickyNotes.CORE.DAL
{
    public class DBContext : DbContext
    {
        public DbSet<ColorModel> Color { get; set; }
        public DbSet<CardModel> Card { get; set; }
        public DbSet<ImageModel> Image { get; set; }
        public DbSet<PlaceModel> Place { get; set; }
        public DbSet<PlaceColumnModel> PlaceColumn { get; set; }
        public DbSet<TagModel> Tag { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(
                "Data Source=StickyNotes.db");
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<PlaceColumnModel>().HasMany(s => s.ICards).WithOne(s => s.FK_PlaceColumn);
            base.OnModelCreating(builder);
        }

        public void InitDatabase()
        {
            this.Database.Migrate();

            if (this.Color.Count() == 0)
            {
                this.Color.Add(new ColorModel()
                {
                    hex_text = "#fff8cc",
                    font_color = "#3E3606",
                    stroke_color = "#FFEF90"
                });

                this.Color.Add(new ColorModel()
                {
                    hex_text = "#e6ffd6",
                    font_color = "#193606",
                    stroke_color = "#ABE983"
                });

                this.Color.Add(new ColorModel()
                {
                    hex_text = "#fcdeef",
                    font_color = "#34061F",
                    stroke_color = "#DC7DB3"
                });


                this.Color.Add(new ColorModel()
                {
                    hex_text = "#f3deff",
                    font_color = "#1D072B",
                    stroke_color = "#A775C4"
                });

                this.Color.Add(new ColorModel()
                {
                    hex_text = "#d4e9ff",
                    font_color = "#07182A",
                    stroke_color = "#749AC1"
                });

                this.Color.Add(new ColorModel()
                {
                    hex_text = "#c7c7c7",
                    font_color = "#475461",
                    stroke_color = "#10151A"
                });



                this.SaveChanges();
            }
        }
    }
}
