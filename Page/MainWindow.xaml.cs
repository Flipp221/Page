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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Page
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int b;
        public static Model.ToiletPaperEntities1 db = new Model.ToiletPaperEntities1();
        public List<Model.Product> products = new List<Model.Product>();
        public MainWindow()
        {
            InitializeComponent();
            RefreshComboBox();
            RefreshFilter();
            RefreshButtons();
            SortiriedCB();
            UpdateTovar();

        }

        private void CBNumberWrite_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pageSize = Convert.ToInt32(CBNumberWrite.SelectedItem.ToString());
            RefreshPagination();
            RefreshButtons();
        }

        private void BLeft_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 0)
                return;
            pageNumber--;
            RefreshPagination();
        }

        private void BRight_Click(object sender, RoutedEventArgs e)
        {
            if (users.Count % pageSize == 0)
            {
                if (pageNumber == (users.Count / pageSize) - 1)
                    return;
            }
            else
            {

                if (pageNumber == (users.Count / pageSize))
                    return;
            }
            pageNumber++;
            RefreshPagination();
        }
        int pageSize;
        int pageNumber;
        List<Model.Product> users = db.Product.ToList();
        private void RefreshPagination()
        {
            DGWrites.ItemsSource = null;
            DGWrites.ItemsSource = users.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }
        private void RefreshComboBox()
        {
            CBNumberWrite.Items.Add("20");
        }
        private void SortiriedCB()
        {
            CBSortiried.Items.Add("Цена");
            CBSortiried.Items.Add("По алфавиту");
        }
        private void RefreshButtons()
        {
            WPButtons.Children.Clear();
            if (users.Count % pageSize == 0)
            {
                for (int i = 0; i < users.Count / pageSize; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(5);
                    button.Width = 20;
                    button.Height = 20;
                    button.FontSize = 12;
                    button.Background = null;
                    button.BorderBrush = null;
                    WPButtons.Children.Add(button);
                }
            }
            else
            {
                for (int i = 0; i < users.Count / pageSize + 1; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(5);
                    button.Width = 20;
                    button.Height = 20;
                    button.FontSize = 12;
                    button.Background = null;
                    button.BorderBrush = null;
                    WPButtons.Children.Add(button);
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pageNumber = Convert.ToInt32(button.Content) - 1;
            RefreshPagination();
        }

        private void CBSortiried_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void RefreshFilter()
        {
            foreach (var item in db.TypeProd)
                SortCB.Items.Add(item.NameType);
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox combobox = (ComboBox)sender;
            string item = Convert.ToString(combobox.SelectedItem);

            if (item == "Фильтрация")
            {
                DGWrites.ItemsSource = users;
                return;
            }

            products = db.Product.Where(z => z.TypeProd.NameType == item).ToList();
            DGWrites.ItemsSource = products;
        }

        private void Poisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTovar();
        }
        private void UpdateTovar()
        {
            var current = Model.ToiletPaperEntities1.GetContext().Product.ToList();

            current = current.Where(p => p.Name.ToLower().Contains(Poisk.Text.ToLower())).ToList();

            DGWrites.ItemsSource = current.OrderBy(p => p.Name).ToList();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (CBSortiried.Text == "По алфавиту")
            {
                var query = (from word in db.Product
                             orderby word.Name
                             select word).ToList();
                DGWrites.ItemsSource = query;
            }
            if (CBSortiried.Text == "Цена")
            {
                var f = (from word in db.Product
                         orderby word.MinCostForAgent
                         select word).ToList();
                DGWrites.ItemsSource = f;
            }

        }

        private void Kol_Click(object sender, RoutedEventArgs e)
        {
            AddPictyre ad = new AddPictyre();
            this.Close();
            ad.Show();
        }
    }

}
