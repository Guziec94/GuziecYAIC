using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Navigation;

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
            StatusBar.SetStatusBarText("Niepodłączony");

            Application.Current.Properties["PortNasluchu"] = 32123;
            Application.Current.Properties["AdresIPNasluchu"] = IPAddress.Parse(cboAdresIP.SelectedItem as string);
        }

        private void InicjalizacjaWyboruAdresuIP()
        {
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily == AddressFamily.InterNetwork && ip.IsDnsEligible)
                    {
                        cboAdresIP.Items.Add(ip.Address.ToString());
                    }
                }
            }
            cboAdresIP.SelectedIndex = 0;
        }

        private void AdresIPcomboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IPAddress wybranyAdresIP = IPAddress.Parse(cboAdresIP.SelectedItem as string);
            Application.Current.Properties["AdresIPNasluchu"] = wybranyAdresIP;
        }

        private void btnOdswiezWyborIP_Click(object sender, RoutedEventArgs e)
        {
            cboAdresIP.Items.Clear();
            InicjalizacjaWyboruAdresuIP();
        }

        private void btnPrzejdzDalej_Click(object sender, RoutedEventArgs e)
        {
            if (txtPseudonim.Text!="")
            {
                Application.Current.Properties["Pseudonim"] = txtPseudonim.Text;
                if (Application.Current.Properties["AdresIPNasluchu"] != null)
                {
                    if (NasluchTCP.RozpocznijNasluch())
                    {
                        StatusBar.SetStatusBarText("Łączenie");
                        NavigationService.Navigate(new GlownyPanel());
                    }
                    else
                    {
                        MessageBox.Show("Nie udało się poprawnie utworzyć połączenia.");
                    }
                }
                else
                {
                    MessageBox.Show("Najpierw należy wybrać adres IP.");
                }
            }
            else
            {
                MessageBox.Show("Aby kontynuować najpierw wprowadź pseudonim.");
            }
        }

        private void txtPseudonim_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btnPrzejdzDalej.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
