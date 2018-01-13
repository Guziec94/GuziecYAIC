using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
    /// Interaction logic for RozpocznijNowaRozmowe.xaml
    /// </summary>
    public partial class KartaTworzeniaRozmowy : TabItem
    {
        public KartaTworzeniaRozmowy()
        {
            InitializeComponent();
            Header = "Rozpocznij nową rozmowę";
        }

        private void btnRozpocznijRozmowe_Click(object sender, RoutedEventArgs e)
        {
            IPAddress ip;
            if (IPAddress.TryParse(txtAdresIP.Text, out ip) && ip.AddressFamily == AddressFamily.InterNetwork)
            {
                try
                {
                    handleClinet client = new handleClinet();
                    client.UruchomKlientaWychodzacego(ip);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Nie udało się nawiązać połączenia z adresem: " + ip.ToString() + ".\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Wprowadź poprawny adres IPv4.");
            }
        }
    }
}
