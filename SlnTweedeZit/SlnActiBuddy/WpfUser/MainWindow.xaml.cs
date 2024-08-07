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

namespace WpfUser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        //Pagina activitiet openen
        private void BtnActiviteiten_Click(object sender, RoutedEventArgs e)
        {
            FrInhoud.Content = new PagActiviteit();
        }

        //Pagina Organiseren openen
        private void BtnOrganiseren_Click(object sender, RoutedEventArgs e)
        {
                FrInhoud.Content = new PagOrganiseren();   
        }
        
        //uitloggen en terug naar het login venster gaan
        private void BtnUitloggen_Click(object sender, RoutedEventArgs e)
        {
            new WinLogin().Show();
            this.Close();
        }

    }
}