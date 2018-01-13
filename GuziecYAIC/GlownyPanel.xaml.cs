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
    /// Interaction logic for GlownyPanel.xaml
    /// </summary>
    public partial class GlownyPanel : Page
    {
        public GlownyPanel()
        {
            InitializeComponent();
            // Inicjalizuje passek rozmów oraz dodaje kartę umożliwiającą rozpoczęcie pierwszej rozmowy
            Rozmowy.Inicjalizuj(tabRozmowy);
            StatusBar.SetStatusBarText("Gotowy do połączenia");
        }
    }
}
