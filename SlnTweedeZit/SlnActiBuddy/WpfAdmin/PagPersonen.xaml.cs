using CLActiBuddy;
using System;
using System.Collections.Generic;
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

namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for PagPersonen.xaml
    /// </summary>
    public partial class PagPersonen : Page
    {
        public PagPersonen()
        {
            InitializeComponent();
            LaadtPersoon();
        }
        
        //alle gebuikers in de listbox laden
        private void LaadtPersoon()
        {
            var personen = Persoon.GetAll();
            LbxPersonen.ItemsSource = personen;
        }

        private void LbxPersonen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LbxPersonen.SelectedItem is Persoon selectedPersoon)
            {
                TxbLogin.Text = selectedPersoon.Login;
                TxbRegDatum.Text = selectedPersoon.Regdatum.ToString("d MMMM yyyy");
                TxbIsAdmin.Text = selectedPersoon.Isadmin ? "ja" : "nee";
                ImgProfielfoto.Source = selectedPersoon.Profielfoto != null ? ByteArrayToImage(selectedPersoon.Profielfoto) : null;
            }
        }
        private void btnVerwijder_Click(object sender, RoutedEventArgs e)
        {
            if (LbxPersonen.SelectedItem is Persoon selectedPersoon)
            {
                NavigationService.Navigate(new PagPersonenVerwijderen(selectedPersoon));
            }
        }

       

        private void ClearDetails()
        {
            TxbLogin.Text = string.Empty;
            TxbRegDatum.Text = string.Empty;
            TxbIsAdmin.Text = string.Empty;
            ImgProfielfoto.Source = null;
        }
        private BitmapImage ByteArrayToImage(byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private void btnNieuw_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PagPersonenToevogen());
        }

        private void btnWijzigen_Click(object sender, RoutedEventArgs e)
        {
            if (LbxPersonen.SelectedItem is Persoon selectedPersoon)
            {

            NavigationService.Navigate(new PagPersonenWijzigen(selectedPersoon));
            }   
        }
    }
}
