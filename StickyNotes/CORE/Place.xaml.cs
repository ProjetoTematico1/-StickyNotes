using StickyNotes.CORE.DAL;
using StickyNotes.CORE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StickyNotes.CORE
{
    /// <summary>
    /// Lógica interna para Place.xaml
    /// </summary>
    public partial class Place : Window
    {
        private DBContext db = new DBContext();
        PlaceModel _Place = new PlaceModel();

        public Place()
        {
        }

        public void NewPlace()
        {

            InitializeComponent();

            _Place = GeneratePlace();
    
            this.DataContext = _Place;
            this.Show();
        }


        private PlaceModel GeneratePlace()
        {
            PlaceModel newPlace = new PlaceModel()
            {
                title = "Sem título"
            };

            db.Add(newPlace);
            db.SaveChanges();

            List<PlaceColumnModel> listColumns = new List<PlaceColumnModel>()
            {
                new PlaceColumnModel()
                {
                    title = "Pendente",
                    cod_place = newPlace.cod_place
                },
                new PlaceColumnModel()
                {
                    title = "Fazendo",
                    cod_place = newPlace.cod_place
                },
                new PlaceColumnModel()
                {
                    title = "Feito",
                    cod_place = newPlace.cod_place
                }
            };

            db.AddRange(listColumns);
            db.SaveChanges();
            newPlace = db.Place.Include(i => i.IColumns).Where(s => s.cod_place == newPlace.cod_place).FirstOrDefault();
            return newPlace;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
            Maximize.Visibility = Visibility.Collapsed;
            Restore.Visibility = Visibility.Visible;
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Restore_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            Restore.Visibility = Visibility.Collapsed;
            Maximize.Visibility = Visibility.Visible;
        }

        private void CardWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
