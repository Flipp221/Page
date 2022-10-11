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
        public static Model.ToiletPaperEntities db = new Model.ToiletPaperEntities();
        public List<Model.Product> products = new List<Model.Product>();
        public MainWindow()
        {
            InitializeComponent();
            RefreshComboBox();
            RefreshButtons();
            SortiriedCB();
            RefreshFilter();
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
        string sort;
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
                    button.Width = 50;
                    button.Height = 50;
                    button.FontSize = 20;
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
                    button.Width = 50;
                    button.Height = 50;
                    button.FontSize = 20;
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
            SortCB.Items.Add("Фильтрация");

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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (var flig in MainWindow.db.Product)
            {
                if (flig.Name == Poisk.Text.Trim())
                {
                    b++;
                    DGWrites.ItemsSource = MainWindow.db.Product.Where(c => c.Name.ToLower() == Poisk.Text.ToLower()).ToList();
                }
            }
            if (b == 0)
            {
                MessageBox.Show("Такого товара не найдено!!");
            }
        }

        private void Poisk_TextChanged(object sender, TextChangedEventArgs e)
        {

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
    }

}
