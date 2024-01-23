using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using WorkWithImage.Models;

namespace WorkWithImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            Refresh();

        }

        private void Refresh()
        {
            LVImages.ItemsSource = App.DB.Pictures.ToList();
        }

        private void BAdd_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog() { Filter = ".png, .jpg, .jpeg | *.png; *.jpg; *.jpeg" };
            if (dialog.ShowDialog().GetValueOrDefault())
            {
                var image = new Pictures();
                image.Img = File.ReadAllBytes(dialog.FileName);
                image.NumberOfLike = 0;
                image.CreateTime = DateTime.Now;
                App.DB.Pictures.Add(image);
                App.DB.SaveChanges();
                Refresh();
            }
        }

        private void BInstall_Click(object sender, RoutedEventArgs e)
        {
            if (LVImages.SelectedItem is Pictures image)
            {

                var saveFileDialog = new SaveFileDialog() { Filter = ".png, .jpg, .jpeg | *.png; *.jpg; *.jpeg" };
                if (saveFileDialog.ShowDialog().GetValueOrDefault())
                {
                    var file = File.Create(saveFileDialog.FileName);
                    file.Close();
                    File.WriteAllBytes(saveFileDialog.FileName, image.Img);
                }
            }
        }

        private void BRemove_Click(object sender, RoutedEventArgs e)
        {
            if (LVImages.SelectedItem is Pictures image)
            {
                App.DB.Pictures.Remove(image);
                App.DB.SaveChanges();
                Refresh();
            }
        }

        private void BPrint_Click(object sender, RoutedEventArgs e)
        {
            if (LVImages.SelectedItem is Pictures image)
            {
                var printDialog = new PrintDialog();
                if (printDialog.ShowDialog().GetValueOrDefault())
                    printDialog.PrintVisual(this, "HH");
            }
        }
    }
}
