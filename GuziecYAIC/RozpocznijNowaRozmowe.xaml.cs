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

namespace GuziecYAIC
{
    /// <summary>
    /// Interaction logic for RozpocznijNowaRozmowe.xaml
    /// </summary>
    public partial class RozpocznijNowaRozmowe : Page
    {
        public RozpocznijNowaRozmowe()
        {
            InitializeComponent();
        }

        private void btnRozpocznijRozmowe_Click(object sender, RoutedEventArgs e)
        {
            Rozmowy.DodajKarteNowejRozmowy(txtAdresIP.Text);
        }
    }
}
