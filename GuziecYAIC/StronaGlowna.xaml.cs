using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

namespace GuziecYAIC
{
    /// <summary>
    /// Interaction logic for StronaGlowna.xaml
    /// </summary>
    public partial class StronaGlowna : Page
    {
        public StronaGlowna()
        {
            StatusBar.SetStatusBarText("Inicjalizacja");
            InitializeComponent();
            InicjalizacjaWyboruAdresuIP();
            StatusBar.SetStatusBarText("GuziecYAIC - offline");
        }

        private void InicjalizacjaWyboruAdresuIP()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    string adres_ip = ip.Address.ToString();
                    if (ip.IsDnsEligible && adres_ip.Length <= 15)
                    {
                        cboAdresIP.Items.Add(adres_ip);
                    }
                }
            }
            cboAdresIP.SelectedIndex = 0;
        }

        private void AdresIPcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string wybraneIP = cboAdresIP.SelectedItem as string;
            System.Windows.Application.Current.Properties["AdresIP"] = wybraneIP;
        }

        private void btnOdswiezWyborIP_Click(object sender, RoutedEventArgs e)
        {
            cboAdresIP.Items.Clear();
            InicjalizacjaWyboruAdresuIP();
        }

        private void btnPrzejdzDalej_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GlownyPanel());
        }
    }
}
