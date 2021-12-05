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
using Microsoft.Win32;
using System.IO;
using Microsoft.Toolkit.Uwp.Notifications;

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
                ImagesPanelCard();
                _Card.open = true;
                this.DataContext = _Card;
                this.Show();
            }
        }


        private CardModel GenerateCard()
        {
            ImagesPanelCard();
            CardModel newCard = new CardModel()
            {
                cod_color = 1
            };

            db.Card.Add(newCard);
            db.SaveChanges();
            newCard = db.Card.Include(i => i.FK_Color).Where(s => s.cod_card == newCard.cod_card).FirstOrDefault();
            newCard = db.Card.Include(i => i.IImages).Where(s => s.cod_card == newCard.cod_card).FirstOrDefault();
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
                if (checkCardExist > 0)
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

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            List<CardModel> listCard = db.Card.ToList();

            ((MainWindow)Application.Current.MainWindow).cardListView.ItemsSource = listCard;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog.Filter = "JPGE (*.jpg,*.jpeg,*.jpe) | *.png; *.jpg;*.jpeg;*.jpe |PNG (*.png) | *.png|Todos os arquivos (*.*) | *.*";
            openFileDialog.Multiselect = true;


            string directoryPath = @"C:\stickyNotes";
            List<string> filePaths = new List<string>();


            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            try
            {
                if (openFileDialog.ShowDialog() == true)
                {
                    string filePath;
                    foreach (string fileNames in openFileDialog.FileNames)
                    {
                        long ticks = new DateTime().Millisecond;
                        string fileName = System.IO.Path.GetFileName(fileNames);
                        filePath = directoryPath + @$"\{System.IO.Path.GetRandomFileName() + fileName  }";





                        FileStream file = File.OpenRead(openFileDialog.FileName);
                        File.Copy(file.Name, filePath);

                        filePaths.Add(filePath);

                        file.Dispose();



                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu o seguinte erro durante a execução do programa: " + ex.Message);
            }




            foreach (string paths in filePaths)
            {

                ImageModel image = new ImageModel();
                image.cod_card = _Card.cod_card;
                image.path = paths;



                db.Add(image);
                db.SaveChanges();

                InsertImages();
                ImagesPanelCard();


            }


        }

        private void Button_Click_Notification(object sender, RoutedEventArgs e)
        {
            ImageModel image = db.Image.Where(s => s.cod_card == _Card.cod_card).FirstOrDefault<ImageModel>();
            ToastContentBuilder notification = new ToastContentBuilder();

            if (DateTime.Parse(_Card.get_reminder_hour) < DateTime.Now)
            {
                new ToastContentBuilder()
               .AddArgument("action", "viewConversation")
               .AddText("Revise o agendamento da sua tarefa!!!")
               .Show();
                return;
            }


            

            new  ToastContentBuilder()
             .AddArgument("action", "viewConversation")
             .AddText("Atividade Agendada para o dia " + _Card.get_reminder_date + " às " + _Card.get_reminder_hour)
             .Show();


            notification.AddArgument("action", "viewConversation");
            notification.AddArgument("conversationId", 9813);
            notification.AddText(_Card.text);


            if (image != null)
            {
                notification.AddAppLogoOverride(new Uri(@image.path), ToastGenericAppLogoCrop.Circle);
                notification.AddHeroImage(new Uri(@image.path));
            }

            

            notification.Schedule(DateTime.Parse(_Card.get_reminder_hour));

            


        }

        private void InsertImages()
        {

            List<ImageModel> images = db.Image.Where(s => s.cod_card == _Card.cod_card).ToList();
            ListViewImages.ItemsSource = images;
        }


        private void ImagesPanelCard()
        {


            List<ImageModel> images = db.Image.Where(s => s.cod_card == _Card.cod_card).ToList();
            if (images.Count > 0)
            {
                ListViewImages.ItemsSource = images;

                var scrollViewer = (ScrollViewer)this.FindName("ScrollViewerImages");
                scrollViewer.Visibility = Visibility.Visible;

                var textCard = (TextBox)this.FindName("CardTextBox");

                Thickness margin = textCard.Margin;
                margin.Top = 0;
                textCard.Margin = margin;
                Grid.SetRow(textCard, 2);
            }
            else
            {

                var scrollViewer = (ScrollViewer)this.FindName("ScrollViewerImages");
                scrollViewer.Visibility = Visibility.Collapsed;

                var textCard = (TextBox)this.FindName("CardTextBox");

                Thickness margin = textCard.Margin;
                margin.Top = 40;
                textCard.Margin = margin;
                Grid.SetRow(textCard, 0);


            }
        }


    }
}





