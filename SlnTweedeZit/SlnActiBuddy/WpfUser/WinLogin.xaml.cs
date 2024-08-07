namespace WpfUser
{
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
    using CLActiBuddy;

    /// <summary>
    /// Interaction logic for WinLogin.xaml
    /// </summary>
    public partial class WinLogin : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WinLogin"/> class.
        /// inlog ventser.
        /// </summary>
        public WinLogin()
        {
            InitializeComponent();
        }

        //naar mainwindow gaan na het klikken
        //hier wordt niet gechecked op admin aangezien administators ook het recht hebben om als gebuiker in te loggen
        private void BtnAanmelden_Click(object sender, RoutedEventArgs e)
        {
            string gebruikersnaam = TxbGebruikersnaam.Text;
            string password = PwbWachtwoord.Password;

            //gebruikersnaam en wachtwoord mogen niet nul zijn
            if (string.IsNullOrEmpty(gebruikersnaam) || string.IsNullOrEmpty(password))
            {
                ErrorMessage.Text = "Gebruikersnaam en Wachtwoord mogen niet leeg zijn.";
                return;
            }
            //nakijken of de gebruiker en het wachtwoord juist zijn via de methode JuisteLogin
            Persoon gebruiker = JuisteLogin(gebruikersnaam, password);
            if (gebruiker == null)
            {
                ErrorMessage.Text = "Ongeldige gebruikersnaam of wachtwoord. Probeer het opnieuw.";
                return;
            }



            // Open het MainWindow en sluit het LoginWindow
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        //DIT NOG IN EEN APARTE CLASSE PLAATSEN
        private Persoon JuisteLogin(string gebruikersnaam, string passwoord)
        {
            List<Persoon> alleGebruikers = Persoon.GetAll();
            foreach (Persoon gebruiker in alleGebruikers)
            {
                if (gebruiker.Login == gebruikersnaam && gebruiker.Paswoord == passwoord)
                {
                    return gebruiker;
                }
            }
            return null;
        }
    }

}
