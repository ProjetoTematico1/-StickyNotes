﻿using Microsoft.EntityFrameworkCore;
using StickyNotes.CORE;
using StickyNotes.CORE.DAL;
using StickyNotes.CORE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace StickyNotes
{
    public partial class MainWindow : Window
    {
        private DBContext db = new DBContext();
       


        public MainWindow()
        {
            db.InitDatabase();
            InitializeComponent();

            List<CardModel> listCard = db.Card.Where(s => s.cod_column == null).ToList();
            cardListView.ItemsSource = listCard;

            ReloadPlaceMenu();



            foreach (var card in listCard.Where(s => s.open == true))
            {
                Card newCard = new Card();
                newCard.OpenCard(card.cod_card);

            }

            

        }

        public void ReloadPlaceMenu()
        {


            this.ClearPlaceMenu();


            List<MenuItem> Places = db.Place.Select(s => new MenuItem()
            {
                DataContext = s,
                Header = s.title,
                Icon = new MaterialDesignThemes.Wpf.PackIcon
                { Kind = MaterialDesignThemes.Wpf.PackIconKind.ViewColumn },
                Cursor = Cursors.Hand

            }).ToList();
            Places.ForEach(s => s.Click += new RoutedEventHandler(OpenPlace_Click));

            Places.ForEach(s => placeMenu.Items.Add(s));

        }

        private void ClearPlaceMenu() {

            var place = placeMenu.Items;
            for (int i = 2; i < place.Count; i++)
            {
                var item = place[i];
                placeMenu.Items.Remove(item);
            }
        
        }

        private void NewCard_Click(object sender, RoutedEventArgs e)
        {
            Card newCard = new Card();
            newCard.NewCard();
        }  
        
        private void NewPlace_Click(object sender, RoutedEventArgs e)
        {
            Place newPlace = new Place();
            newPlace.NewPlace();
        }

        private void OpenPlace_Click(object sender, RoutedEventArgs e)
        {
            PlaceModel placeCtx = (sender as MenuItem).DataContext as PlaceModel;
            Place Place = new Place();
            Place.OpenPlace(placeCtx.cod_place);
        }

        private void OpenCard_Click(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            var dataCxtx = tb.DataContext;
            CardModel selectedCard = (CardModel)dataCxtx;

            Card newCard = new Card();
            newCard.OpenCard(selectedCard.cod_card);
        }


        private void OpenOptions_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }


    }
}
