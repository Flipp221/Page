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
using Microsoft.Win32;
using System.IO;


namespace Page
{
    /// <summary>
    /// Логика взаимодействия для AddPictyre.xaml
    /// </summary>
    public partial class AddPictyre : Window
    {
        byte[] pic;
        public AddPictyre()
        {
            InitializeComponent();
        }

        private void BChangePicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
               pic = File.ReadAllBytes(openFileDialog.FileName);
            }
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.db.User.Add(user);
            foreach(var flig in MainWindow.db.Product)
            {
                if(flig.Id_Prod.ToString() == CodeNum.Text)
                {
                    flig.Picture = pic;
                }
            }
            MainWindow.db.SaveChanges();
            MessageBox.Show("Success");
        }
    }
}
