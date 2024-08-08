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

namespace WpfAdmin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int gebruikerId;
        private Persoon IngelogdeGebruiker;
        public MainWindow(int gebruikerId)
        {
            InitializeComponent();
            this.gebruikerId = gebruikerId;
            this.IngelogdeGebruiker = Persoon.GetById(gebruikerId);
            // Ingelogde gebuiker weergeven
            ToonGebruiker();
            // bij het openen van het MainWindow wordt de pagina PagPersoenen standaard getoont.
            FrInhoud.Content = new PagPersonen();
        }

        // De ingelogde gebuiker weergeven
        private void ToonGebruiker()
        {
            if (IngelogdeGebruiker != null)
            {
                lblTop.Content = $"WPFAdmin: welkom {IngelogdeGebruiker.Voornaam} {IngelogdeGebruiker.Achternaam}";
                imgProfielfoto.Source = IngelogdeGebruiker.Profielfoto != null ? ByteArrayToImage(IngelogdeGebruiker.Profielfoto) : null;
            }
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

        // Mainwindow sluiten en terug naar het login venster gaan
        private void BtnUitloggen_Click(object sender, RoutedEventArgs e)
        {
            new WinLogin().Show();
            this.Close();
        }

        // De pagina Activiteiten openen
        private void BtnActiviteiten_Click(object sender, RoutedEventArgs e)
        {
            FrInhoud.Content = new PagActiviteiten();
        }

        // Da pagina Personen openen
        private void BtnPersonen_Click(object sender, RoutedEventArgs e)
        {
            FrInhoud.Content = new PagPersonen();
        }
    }
    
}
