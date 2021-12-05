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
            ((MainWindow)Application.Current.MainWindow).ReloadPlaceMenu();
            this.DataContext = _Place;
            this.Show();
        }

        public void OpenPlace(int cod_place)
        {

            InitializeComponent();

            _Place = db.Place.AsNoTracking().Where(S => S.cod_place == cod_place).FirstOrDefault();

            this.DataContext = _Place;
            this.Show();
        }

        private void EditPlace(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(_Place.title)) _Place.title = "Sem Título";
            if (_Place.cod_place == 0)
                db.Place.Add(_Place);

            db.SaveChanges();
        }

        private void EditCollumn(object sender, TextChangedEventArgs e)
        {
            PlaceColumnModel col = (sender as TextBox).DataContext as PlaceColumnModel;

            if (col != null)
            {
                PlaceColumnModel placeColumn = db.PlaceColumn.Find(col.cod_column);
                if (placeColumn != null)
                {
                    if (String.IsNullOrEmpty(col.title))
                        placeColumn.title = "Sem Título";
                    else
                        placeColumn.title = col.title;
                }
                db.SaveChanges();
            }

        }

        private void EditCard(object sender, TextChangedEventArgs e)
        {
            CardModel card = (sender as TextBox).DataContext as CardModel;

            if (card != null)
            {
                CardModel cardModel = db.Card.Find(card.cod_card);
                if (cardModel != null)
                {
                    cardModel.text = card.text;

                }

                db.SaveChanges();

            }



        }

        public void AddColumn(object sender, RoutedEventArgs e)
        {
            PlaceColumnModel placeColumn = new PlaceColumnModel() {
                cod_place = _Place.cod_place,
                title = "Sem Titulo",
            };

            db.PlaceColumn.Add(placeColumn);
            db.SaveChanges();
            ReloadColumns();
        
        }
        public void DeletePlace(object sender, RoutedEventArgs e)
        {

            List<PlaceColumnModel> Columns = db.PlaceColumn.Where(s => s.cod_place == _Place.cod_place).ToList();


            foreach (var col in Columns)
            {
                List<CardModel> Cards = db.Card.Where(s => s.cod_column == col.cod_column).ToList();

                foreach (var card in Cards)
                {
                    List<ImageModel> images = db.Image.Where(s => s.cod_card == card.cod_card).ToList();
                    db.Image.RemoveRange(images);
                }

                db.Card.RemoveRange(Cards);
            }

            db.PlaceColumn.RemoveRange(Columns);
            PlaceModel placeModel = db.Place.Find(_Place.cod_place);
            if (placeModel != null)
            {
                db.Place.Remove(placeModel);
                db.SaveChanges();
            }


            ((MainWindow)Application.Current.MainWindow).ReloadPlaceMenu();


            this.Close();
        }

        private void DeleteCard_Click(object sender, RoutedEventArgs e)
        {
            CardModel cardCtx = (sender as Button).DataContext as CardModel;

            CardModel cardModel = db.Card.Find(cardCtx.cod_card);

            db.Card.Remove(cardModel);

            db.SaveChanges();

            ReloadColumns();

        }

        private void ChangeColor_Click(object sender, RoutedEventArgs e)
        {
            var tb = (Button)e.Source;
            var dataCxtx = tb.DataContext;
            ColorsCardViewModel selectedColor = (ColorsCardViewModel)dataCxtx;

            CardModel cardModel = db.Card.Find(selectedColor.cod_card);
            if (cardModel != null)
            {
                cardModel.cod_color = selectedColor.cod_color;
                db.SaveChanges();
                ReloadColumns();
            }

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

        private void AddCard_Click(object sender, RoutedEventArgs e)
        {
            int? cod_column = (sender as Button).DataContext as int?;
            if (cod_column != null && cod_column > 0)
            {
                CardModel card = new Card().GenerateCard(cod_column);
                ReloadColumns();
            }
        }  
        
        
        private void DeleteColumn(object sender, RoutedEventArgs e)
        {
            PlaceColumnModel col = (sender as Button).DataContext as PlaceColumnModel;
            if (col != null)
            {
                List<CardModel> Cards = db.Card.Where(s => s.cod_column == col.cod_column).ToList();
                foreach (var card in Cards)
                {
                    List<ImageModel> images = db.Image.Where(s => s.cod_card == card.cod_card).ToList();
                    db.Image.RemoveRange(images);
                }

                db.Card.RemoveRange(Cards);

                PlaceColumnModel placeColumnModel = db.PlaceColumn.Find(col.cod_column);
                db.PlaceColumn.Remove(placeColumnModel);

                db.SaveChanges();
                ReloadColumns();
            }
        }

        private void ReloadColumns()
        {

            db.PlaceColumn.Include(s => s.ICards).Where(s => s.cod_place == _Place.cod_place).Select(s => s.ICards).ToList();
            List<PlaceColumnModel> listColumns = db.PlaceColumn.Include(s => s.ICards).Where(s => s.cod_place == _Place.cod_place).ToList();
            columnsListView.ItemsSource = listColumns;
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).ReloadPlaceMenu();
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
