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
    /// Interaction logic for PagPersonenToevogen.xaml
    /// </summary>
    public partial class PagPersonenToevogen : Page
    {
        private byte[] profielFotoBytes;
        public PagPersonenToevogen()
        {
            InitializeComponent();
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

        private void BtnOpslaan_Click(object sender, RoutedEventArgs e)
        {
            // Haal de gegevens op uit de invoervelden
            string voornaam = txbVoornaam.Text;
            string achternaam = txbAchternaam.Text;
            string login = txbLogin.Text;
            string paswoord = txbPaswoord.Text;
            bool isAdmin = cbxAdmin.IsChecked ?? false;

            // Niet alle velden zijn ingevuld
            if (string.IsNullOrEmpty(voornaam) || string.IsNullOrEmpty(achternaam) || string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Vul alle verplichte velden in.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
         

            // Maak een nieuwe Persoon aanmaken
            Persoon nieuwePersoon = new Persoon
            {
                Voornaam = voornaam,
                Achternaam = achternaam,
                Login = login,
                Paswoord = paswoord,
                Regdatum = DateTime.Now,
                Isadmin = isAdmin,
                Profielfoto = profielFotoBytes
            };
            {
                nieuwePersoon.AddPersoon();
                MessageBox.Show("De persoon is succesvol opgeslagen.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                // Reset de invoervelden na succesvolle opslag
                txbVoornaam.Clear();
                txbAchternaam.Clear();
                txbLogin.Clear();
                txbPaswoord.Clear();
                cbxAdmin.IsChecked = false;
                imgProfielfoto.Source = null;
                profielFotoBytes = null;
            }

        }

        // Terug naar de vorige pagina gaan
        private void BtnAnnuleren_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("PagPersonen.xaml", UriKind.Relative));
        }
    }
}
