using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace GuziecYAIC
{
    /// <summary>
    /// Interaction logic for KartaRozmowy.xaml
    /// </summary>
    public partial class KartaRozmowy : TabItem
    {
        public Guid guid;
        public ObservableCollection<Wiadomosc> listaWiadomosci;
        public string pseudonimRozmowcy;
        public IPAddress adresIPRozmowcy;
        NetworkStream strumienSieciowy;
        BinaryWriter binaryWriter;
        BinaryReader binaryReader;
        Task odbieranieWiadomosci;

        public KartaRozmowy(string pseudonim, IPAddress adresIP, NetworkStream strumienSieciowy)
        {
            InitializeComponent();
            adresIPRozmowcy = adresIP;
            guid = new Guid();
            listaWiadomosci = new ObservableCollection<Wiadomosc>();
            listaWiadomosci.CollectionChanged += ListaWiadomosci_CollectionChanged;

            CloseableHeader nowyHeader = new CloseableHeader();
            nowyHeader.btnClose.Click += new RoutedEventHandler(btnClose_Click);
            Header = nowyHeader;

            pseudonimRozmowcy = pseudonim;
            Title = pseudonim + " (" + adresIP + ")";
            txtTytulRozmowy.Text = $"Rozmowa z {pseudonimRozmowcy} ({adresIPRozmowcy})";

            this.strumienSieciowy = strumienSieciowy;
            binaryWriter = new BinaryWriter(strumienSieciowy);
            binaryReader = new BinaryReader(strumienSieciowy);
            odbieranieWiadomosci = new Task(() => {
                try
                {
                    while (binaryReader != null)
                    {
                        if (strumienSieciowy.DataAvailable)
                        {
                            ZróbPik();
                            string odebranaWiadomosc = binaryReader.ReadString();
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                if (odebranaWiadomosc != "*KONIEC*")
                                {
                                    listaWiadomosci.Add(new Wiadomosc(TypWiadomosci.doMnie, odebranaWiadomosc));
                                }
                                else
                                {
                                    listaWiadomosci.Add(new Wiadomosc(TypWiadomosci.inny, "Rozmówca zakończył konwersację."));
                                    btnWyslij.IsEnabled = false;
                                }
                            });
                        }
                    }
                }
                catch(Exception ex)
                {}
            });
            odbieranieWiadomosci.Start();
        }

        private void ZróbPik()//najlepsza funkcjonalność
        {
            new Task(()=>Console.Beep(5000, 250)).Start();
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


        public void ZakonczRozmowe()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                (Header as CloseableHeader).btnClose.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
            });
        }

        // Button Close Click - Remove the Tab - (or raise an event indicating a "CloseTab" event has occurred)
        void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                binaryWriter.Write("*KONIEC*");
                binaryReader.Close();
                binaryWriter.Close();
                strumienSieciowy.Close();

                binaryReader.Dispose();
                binaryWriter.Dispose();
                strumienSieciowy.Dispose();
                binaryReader = null;
            }
            catch (Exception ex)
            { }
            Rozmowy.ZakonczRozmowe(guid);
            ((TabControl)Parent).Items.Remove(this);
        }

        private void btnWyslij_Click(object sender, RoutedEventArgs e)
        {
            if (txtTrescWiadomosci.Text.Length > 0 && btnWyslij.IsEnabled)
            {
                binaryWriter.Write(txtTrescWiadomosci.Text);
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
