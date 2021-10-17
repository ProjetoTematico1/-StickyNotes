using StickyNotes.CORE.DAL;
using StickyNotes.CORE.Models;
using System;
using System.Collections.Generic;
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
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace StickyNotes.CORE
{
    /// <summary>
    /// Lógica interna para Card.xaml
    /// </summary>
    public partial class Card : Window
    {
        private DBContext db = new DBContext();
        CardModel _Card = new CardModel();
        public Card()
        {
        }

        public void NewCard()
        {

            InitializeComponent();
            colorListView.ItemsSource = db.Color.ToList();
            _Card = GenerateCard();
            _Card.open = true;
            this.DataContext = _Card;
            this.Show();
        }

        public void OpenCard(int cod_card)
        {
            _Card = db.Card.Where(s => s.cod_card == cod_card).FirstOrDefault();
            if (_Card == null)
                NewCard();
            else
            {
                InitializeComponent();
                colorListView.ItemsSource = db.Color.ToList();
                _Card.open = true;
                this.DataContext = _Card;
                this.Show();
            }
        }



        private CardModel GenerateCard()
        {
            CardModel newCard = new CardModel()
            {
                cod_color = 1
            };

            db.Card.Add(newCard);
            db.SaveChanges();
            newCard = db.Card.Include(i => i.FK_Color).Where(s => s.cod_card == newCard.cod_card).FirstOrDefault();
            return newCard;
        }

        private void EditCard(object sender, TextChangedEventArgs e)
        {

            if (_Card.cod_card == 0)
                db.Card.Add(_Card);

            db.SaveChanges();
        }

        private void CloseWindowEvent(object sender, CancelEventArgs e)
        {
            if (String.IsNullOrEmpty(_Card.text) && (_Card.IImages == null || _Card.IImages.Count() == 0))
            {
                db.Card.Remove(_Card);
                db.SaveChanges();
            }
            else
            {
                int checkCardExist = db.Card.Where(s => s.cod_card == _Card.cod_card).Count();
                if(checkCardExist > 0)
                {
                    _Card.open = false;
                    db.Entry(_Card).State = EntityState.Modified;
                    db.SaveChanges();
                }
             
            }

        }

        private void ChangeColor_Click(object sender, RoutedEventArgs e)
        {
            var tb = (Button)e.Source;
            var dataCxtx = tb.DataContext;
            ColorModel selectedColor = (ColorModel)dataCxtx;
            _Card.cod_color = selectedColor.cod_color;
            db.Entry(_Card).State = EntityState.Modified;
            db.SaveChanges();

            CardWindow.Background = (Brush)new BrushConverter().ConvertFrom(selectedColor.hex_text);
            CardTextBox.Foreground = (Brush)new BrushConverter().ConvertFrom(selectedColor.font_color);

        }

        private void BoldText_Click(object sender, RoutedEventArgs e)
        {
            CardTextBox.SetResourceReference(TextElement.FontWeightProperty, FontWeights.Bold);
        }

        private void SetReminderDate_Click(object sender, RoutedEventArgs e)
        {
            string Date = ReminderDate.Text;
            string Hour = ReminderHour.Text;

            DateTime reminderDate = DateTime.Parse(String.Format("{0} {1}", Date, Hour));

            if (_Card.dt_reminder != reminderDate)
            {
                _Card.dt_reminder = reminderDate;
                _Card.reminder_confirm = false;
                db.Entry(_Card).State = EntityState.Modified;
                db.SaveChanges();
            }

        }

        private void DeleteCard_Click(object sender, RoutedEventArgs e)
        {
            db.Card.Remove(_Card);
            db.SaveChanges();
            this.Close();
                

        }


    }

}
