using CLActiBuddy;
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
// voor de afbeeldingen
using System.IO;

namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for PagPersonenVerwijderen.xaml
    /// </summary>
    public partial class PagPersonenVerwijderen : Page
    {
        private byte[] profielFotoBytes;
        private Persoon selectedPersoon;
        public PagPersonenVerwijderen(Persoon persoon)
        {
            InitializeComponent();
            this.selectedPersoon = persoon;
            LaadtPersoondetails();
        }

       

        //de fiche in de labels weergeven
        private void LaadtPersoondetails()
        {
            LblVoornaam.Content = selectedPersoon.Voornaam;
            LblAchternaam.Content = selectedPersoon.Achternaam;
            imgProfielfoto.Source = selectedPersoon.Profielfoto != null ? ByteArrayToImage(selectedPersoon.Profielfoto) : null;
        }

        //voor het laden van de foto
        private BitmapImage ByteArrayToImage(byte[] byteArray)
        {
            using (var stream = new MemoryStream(byteArray))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();
                return image;
            }
        }

        private void BtnVerwijderen_Click(object sender, RoutedEventArgs e)
        {
            selectedPersoon.DeleteFromDB();
            MessageBox.Show("Persoon succesvol verwijderd.");
            NavigationService.Navigate(new PagPersonen());
        }

        private void BtnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PagPersonen());
        }
    }
}
