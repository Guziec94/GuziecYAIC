using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for KartaRozmowy.xaml
    /// </summary>
    public partial class KartaRozmowy : TabItem
    {
        public Guid guid;
        string pseudonimRozmowcy;
        IPAddress adresIPRozmowcy;
        ObservableCollection<Wiadomosc> listaWiadomosci;

        public KartaRozmowy(IPAddress adresIP, string pseudonim)
        {
            InitializeComponent();
            adresIPRozmowcy = adresIP;
            pseudonimRozmowcy = pseudonim;
            guid = new Guid();
            listaWiadomosci = new ObservableCollection<Wiadomosc>();
            listaWiadomosci.CollectionChanged += ListaWiadomosci_CollectionChanged;

            CloseableHeader nowyHeader = new CloseableHeader();
            nowyHeader.btnClose.Click += new RoutedEventHandler(btnClose_Click);
            Header = nowyHeader;

            Title = pseudonim + adresIP;
            txtTytulRozmowy.Text = String.Format("Rozmowa z {0} ({1})", pseudonimRozmowcy, adresIPRozmowcy);
        }

        private void ListaWiadomosci_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            stpaWiadomosci.Items.Clear();
            foreach (Wiadomosc w in listaWiadomosci)
            {
                stpaWiadomosci.Items.Add(w.GenerujObiektUI());
            }
        }

        public string Title
        {
            set => ((CloseableHeader)Header).lblTabTitle.Content = value;
            get => ((CloseableHeader)Header).lblTabTitle.Content.ToString();
        }


        // Button Close Click - Remove the Tab - (or raise an event indicating a "CloseTab" event has occurred)
        void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Rozmowy.ZakonczRozmowe(guid);
            ((TabControl)Parent).Items.Remove(this);
        }

        private void btnWyslij_Click(object sender, RoutedEventArgs e)
        {
            if (txtTrescWiadomosci.Text.Length > 0)
            {
                listaWiadomosci.Add(new Wiadomosc(TypWiadomosci.odeMnie, txtTrescWiadomosci.Text));
                txtTrescWiadomosci.Clear();
                var scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(stpaWiadomosci, 0);
                scrollViewer.ScrollToBottom();
            }
            else
            {
                //nie można wysłać pustej wiadomości
            }
        }

        private void txtTrescWiadomosci_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                btnWyslij.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            }
        }
    }
}
