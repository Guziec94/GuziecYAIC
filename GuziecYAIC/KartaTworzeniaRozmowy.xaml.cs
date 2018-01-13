using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;

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
