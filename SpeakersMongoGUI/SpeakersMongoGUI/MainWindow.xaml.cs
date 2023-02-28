using System;
using System.Collections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MongoDB.Bson;
using MongoDB.Driver;
using SpeakersMongoGUI.Entities;

namespace SpeakersMongoGUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables

        int lowerPrice;
        int higherPrice;

        public List<Speaker> speakers = new List<Speaker>();
        public List<Category> categories = new List<Category>();

        CartWindow cart = new CartWindow();

        #endregion

        #region Initialization

        public MainWindow()
        {
            InitializeComponent();

            GetProducts("{}");
            InitializeProductsList();

            GetCategories();
            InitializeCategoriesList();
        }

        public void GetProducts(string query)
        {
            speakers = new List<Speaker>();

            MongoClient client = new MongoClient("mongodb://localhost:27017/speakers");

            var collection = client.GetDatabase("speakers").GetCollection<BsonDocument>("speaker");

            foreach (var item in collection.Find(query).ToList()) 
            {
                string id = item.GetElement(0).Value.ToString();
                string name = item.GetElement(1).Value.ToString();
                int count = item.GetElement(2).Value.ToInt32();
                int price = item.GetElement(3).Value.ToInt32();
                string manufacturer = item.GetElement(4).Value.ToString();
                bool bluetooth = item.GetElement(5).Value.ToBoolean();
                bool aux = item.GetElement(6).Value.ToBoolean();
                string categoryId = item.GetElement(7).Value.ToString();

                speakers.Add(new Speaker(id, name, count, price, manufacturer, bluetooth, aux, categoryId));
            }
        }

        public void InitializeProductsList()
        {
            foreach (Speaker speaker in speakers)
            {
                AddProductToList(speaker);
            }
        }

        public void GetCategories()
        {
            MongoClient client = new MongoClient("mongodb://localhost:27017/speakers");

            var collection = client.GetDatabase("speakers").GetCollection<BsonDocument>("category");

            foreach (var item in collection.Find(new BsonDocument()).ToList())
            {
                string id = item.GetElement(0).Value.ToString();
                string name = item.GetElement(1).Value.ToString();
                categories.Add(new Category(id, name));
            }
        }

        public void InitializeCategoriesList()
        {
            foreach (var item in categories)
            {
                categories_cb.Items.Add(item.Name);
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
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200
            };

            StackPanel subInfoSP = new StackPanel()
            {
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Right,
                Width = 450
            };

            StackPanel buttonsSP = new StackPanel()
            {
                Orientation = Orientation.Horizontal,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                Width = 200
            };

            TextBlock manufacturerTB = new TextBlock()
            {
                Text = speaker.Manufacturer,
                Margin = new Thickness(5, 0, 0, 0)
            };

            TextBlock nameTB = new TextBlock()
            {
                Text = speaker.Name,
                Margin = new Thickness(5, 0, 5, 0)
            };

            TextBlock priceTB = new TextBlock()
            {
                Text = "цена: " + speaker.Price.ToString() + " ₽",
                Margin = new Thickness(30, 0, 30, 0)
            };

            TextBlock countTB = new TextBlock()
            {
                Text = "в наличии: " + speaker.Count.ToString() + " шт.",
                Margin = new Thickness(30, 0, 30, 0)
            };

            Image addImage = new Image()
            {
                Source = new BitmapImage(new Uri("/Pictures/add.png", UriKind.Relative))
            };

            Button addButton = new Button()
            {
                Tag = speaker.Id,
                Height = 40,
                Width = 40,
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Transparent,
                
            };
            
            mainInfoSP.Children.Add(manufacturerTB);
            mainInfoSP.Children.Add(nameTB);

            subInfoSP.Children.Add(priceTB);
            subInfoSP.Children.Add(countTB);

            addButton.Content = addImage;
            addButton.Click += AddToCartClick;

            buttonsSP.Children.Add(addButton);

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

        #endregion

        #region Clicks

        private void SearchBtnClick(object sender, RoutedEventArgs e)
        {
            ClearProductList();

            string categoryId;
            string query = $"{{";

            if (search_tb.Text != "")
            {
                query += $"$text: {{$search: \"{search_tb.Text}\"}}";
            }

            if (Int32.TryParse(lower_price_tb.Text, out lowerPrice) && Int32.TryParse(higher_price_tb.Text, out higherPrice))
            {
                query += $"speaker_price: {{$gte: {lowerPrice}, $lte: {higherPrice}}}";
            }
            else if (Int32.TryParse(lower_price_tb.Text, out lowerPrice))
            {
                query += $"speaker_price: {{$gte: {lowerPrice}}}";
            }
            else if (Int32.TryParse(higher_price_tb.Text, out higherPrice))
            {
                query += $"speaker_price: {{$lte: {higherPrice}}}";
            }

            if (categories_cb.Text != "-")
            {
                categoryId = categories.Find(
                    delegate (Category category)
                    {
                        return category.Name == categories_cb.Text;
                    }
                    ).Id;

                query += $"speaker_category_id: ObjectId('{categoryId}')";
            }

            query += $"}}";

            GetProducts(query);

            InitializeProductsList();
        }

        private void ClearBtnClick(object sender, RoutedEventArgs e)
        {
            search_tb.Text = "";
            lower_price_tb.Text = "";
            higher_price_tb.Text = "";
            categories_cb.Text = "-";

            ClearProductList();

            GetProducts("{}");

            InitializeProductsList();
        }

        private void AddToCartClick(object sender, RoutedEventArgs e)
        {
            cart.chosenSpeakers.Add(speakers.Find(s => s.Id == ((Button)sender).Tag.ToString()));

            cart.total += speakers.Find(s => s.Id == ((Button)sender).Tag.ToString()).Price;

            cart.total_lbl.Content = cart.total.ToString();
        }

        private void ShowCartWindow(object sender, RoutedEventArgs e)
        {
            cart.InitializeProductsList();

            cart.Show();
        }

        #endregion
    }
}
