using CLActiBuddy;
using Microsoft.Win32;
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
using System.IO;

namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for PagPersonenWijzigen.xaml
    /// </summary>
    public partial class PagPersonenWijzigen : Page
    {
        private byte[] profielFotoBytes;
        private Persoon selectedPersoon;
        public PagPersonenWijzigen(Persoon persoon)
        {
            InitializeComponent();
            this.selectedPersoon = persoon;
            LaadtPersoonDetails();
        }
        private void LaadtPersoonDetails()
        {

                
            txbVoornaam.Text = selectedPersoon.Voornaam;
            txbAchternaam.Text = selectedPersoon.Achternaam;
            txbLogin.Text = selectedPersoon.Login;
            txbPaswoord.Text = selectedPersoon.Paswoord;
            cbxAdmin.IsChecked = selectedPersoon.Isadmin;
            imgProfielfoto.Source = selectedPersoon.Profielfoto != null? ByteArrayToImage(selectedPersoon.Profielfoto) : null;
            


        }
        private BitmapImage ByteArrayToImage(byte[] byteArray)
        {
            using (var stream = new System.IO.MemoryStream(byteArray))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
        }

        private void BtnUpload_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                imgProfielfoto.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                profielFotoBytes = File.ReadAllBytes(openFileDialog.FileName);

            }
        }

        private void BtnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("PagPersonen.xaml", UriKind.Relative));
        }

        private void BtnOpslaan_Click(object sender, RoutedEventArgs e)
        {

            selectedPersoon.Voornaam = txbVoornaam.Text;
            selectedPersoon.Achternaam = txbAchternaam.Text;
            selectedPersoon.Login = txbLogin.Text;
            selectedPersoon.Paswoord = txbPaswoord.Text;
            selectedPersoon.Isadmin = cbxAdmin.IsChecked ?? false;
            selectedPersoon.Profielfoto = profielFotoBytes;


            selectedPersoon.UpdatePersoon();
        }
    }
}
