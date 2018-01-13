using System.Linq;
using System.Windows;

namespace GuziecYAIC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            StatusBar.Inicjalizuj(txtStatusBar);
            frameZawartosc.Navigate(new StronaGlowna());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Rozmowy.kartyRozmow.Any())
            {
                NasluchTCP.serwerUruchomiony = false;// rozpoczyna procedurę zamykania połączeń
                NasluchTCP.serverSocket.Stop();
                e.Cancel = true;
            }
        }
    }
}
