using SpeakersMongoGUI.Entities;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SpeakersMongoGUI
{
    /// <summary>
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {

        #region Variables

        public int total = 0;

        public List<Speaker> chosenSpeakers = new List<Speaker>();

        #endregion

        #region Initialization

        public CartWindow()
        {
            InitializeComponent();

            InitializeProductsList();
        }

        public void InitializeProductsList()
        {
            ClearProductList();

            foreach (Speaker speaker in chosenSpeakers.Select(cs => cs).Distinct())
            {
                AddProductToList(speaker);
            }
        }

        #endregion

        #region Helpers

        public void AddProductToList(Speaker speaker)
        {
            Border productBorder = new Border()
            {
                CornerRadius = new CornerRadius(6),
                BorderBrush = Brushes.Green,
                BorderThickness = new Thickness(1),
                Margin = new Thickness(5, 5, 5, 5),
            };

            StackPanel productSP = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(5, 5, 5, 5),
            };

            StackPanel mainInfoSP = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 110
            };

            StackPanel subInfoSP = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 90
            };

            StackPanel buttonsSP = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 50
            };

            TextBlock manufacturerTB = new TextBlock()
            {
                Text = speaker.Manufacturer,
            };

            TextBlock nameTB = new TextBlock()
            {
                Text = speaker.Name,
            };

            TextBlock countTB = new TextBlock()
            {
                Text = chosenSpeakers.Count(s => s.Id == speaker.Id) + " шт.",
            };

            TextBlock priceTB = new TextBlock()
            {
                Text = (chosenSpeakers.Count(s => s.Id == speaker.Id) * chosenSpeakers.Find(s => s.Id == speaker.Id).Price).ToString() + " ₽",
            };

            Image addImage = new Image()
            {
                Source = new BitmapImage(new Uri("/Pictures/add.png", UriKind.Relative))
            };

            Image removeImage = new Image()
            {
                Source = new BitmapImage(new Uri("/Pictures/remove.png", UriKind.Relative))
            };

            Button addButton = new Button()
            {
                Tag = speaker.Id,
                Height = 20,
                Width = 20,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,

            };

            Button removeButton = new Button()
            {
                Tag = speaker.Id,
                Height = 20,
                Width = 20,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,

            };

            mainInfoSP.Children.Add(manufacturerTB);
            mainInfoSP.Children.Add(nameTB);

            subInfoSP.Children.Add(priceTB);
            subInfoSP.Children.Add(countTB);

            addButton.Content = addImage;
            addButton.Click += AddBtnClick;

            removeButton.Content = removeImage;
            removeButton.Click += RemoveBtnClick;

            buttonsSP.Children.Add(addButton);
            buttonsSP.Children.Add(removeButton);

            productSP.Children.Add(mainInfoSP);
            productSP.Children.Add(subInfoSP);
            productSP.Children.Add(buttonsSP);

            productBorder.Child = productSP;

            products_sp.Children.Add(productBorder);
        }

        public void ClearProductList()
        {
            products_sp.Children.Clear();
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Clicks

        private void ClearBtnClick(object sender, RoutedEventArgs e)
        {
            total = 0;
            total_lbl.Content = total.ToString();

            chosenSpeakers.Clear();

            ClearProductList();

            InitializeProductsList();
        }

        private void AddBtnClick(object sender, RoutedEventArgs e)
        {
            chosenSpeakers.Add(chosenSpeakers.Find(s => s.Id == ((Button)sender).Tag.ToString()));

            total += chosenSpeakers.Find(s => s.Id == ((Button)sender).Tag.ToString()).Price;

            total_lbl.Content = total.ToString();

            InitializeProductsList();
        }

        private void RemoveBtnClick(object sender, RoutedEventArgs e)
        {
            total -= chosenSpeakers.Find(s => s.Id == ((Button)sender).Tag.ToString()).Price;

            chosenSpeakers.Remove(chosenSpeakers.Find(s => s.Id == ((Button)sender).Tag.ToString()));

            total_lbl.Content = total.ToString();

            InitializeProductsList();
        }

        #endregion
    }
}
