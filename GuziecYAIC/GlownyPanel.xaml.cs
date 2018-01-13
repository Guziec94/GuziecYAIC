using System.Windows.Controls;

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
