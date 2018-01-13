using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace GuziecYAIC
{
    static class NasluchTCP
    {
        public static IPAddress adresDoNasluchu;
        public static int portDoNasluchu;
        public static TcpListener serverSocket;
        static TcpClient clientSocket;
        public static bool serwerUruchomiony;
        static Task watekOczekujacyNaPolaczenia;
        static public bool RozpocznijNasluch()
        {
            try
            {
                portDoNasluchu = (int) Application.Current.Properties["PortNasluchu"];
                adresDoNasluchu = Application.Current.Properties["AdresIPNasluchu"] as IPAddress;

                serverSocket = new TcpListener(new IPEndPoint(adresDoNasluchu, portDoNasluchu));
                //clientSocket = new TcpClient();// default(TcpClient);

                serverSocket.Start();
                serwerUruchomiony = true;
                Console.WriteLine(" >> " + "Server Started");

                watekOczekujacyNaPolaczenia = new Task(()=>
                {
                    while (serwerUruchomiony)
                    {
                        try
                        {
                            clientSocket = serverSocket.AcceptTcpClient();
                            Console.WriteLine("new Client started!");
                            handleClinet client = new handleClinet();
                            client.UruchomKlientaPrzychodzacego(clientSocket);
                        }
                        catch (Exception ex)
                        { }
                    }
                    while(Rozmowy.kartyRozmow.Any())
                    {
                        Rozmowy.kartyRozmow.FirstOrDefault().ZakonczRozmowe();
                    }
                    // Zamnknięcie aplikacji po rozłączęniu wszystkich użytkowników
                    Application.Current.Dispatcher.Invoke(()=> 
                    {
                        Application.Current.Shutdown();
                    });
                });
                watekOczekujacyNaPolaczenia.Start();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        static public bool ZakonczNasluch()
        {
            try
            {
                clientSocket.Close();
                serverSocket.Stop();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }

    //Class to handle each client request separatly
    public class handleClinet
    {
        public void UruchomKlientaPrzychodzacego(TcpClient inClientSocket)
        {
            string adresIP = ((IPEndPoint)inClientSocket.Client.RemoteEndPoint).Address.ToString();
            if (Rozmowy.kartyRozmow.Any(x => x.adresIPRozmowcy.ToString() == adresIP))
            {
                MessageBox.Show("Nie można nawiązać rozmowy z tym adresem IP (prawdopodobnie taka konwersacja już trwa).");
                return;
            }
            NetworkStream networkStream = inClientSocket.GetStream();
            BinaryReader binaryReader = new BinaryReader(networkStream);
            string pseudonimNawiazujacego = binaryReader.ReadString();
            BinaryWriter binaryWriter = new BinaryWriter(networkStream);
            binaryWriter.Write(Application.Current.Properties["Pseudonim"] as String);
            Application.Current.Dispatcher.Invoke(() =>
            {
                Rozmowy.DodajKarteNowejRozmowy(pseudonimNawiazujacego, adresIP, networkStream);
            });
        }

        public void UruchomKlientaWychodzacego(IPAddress adresIP)
        {
            if (Rozmowy.kartyRozmow.Any(x => x.adresIPRozmowcy.ToString() == adresIP.ToString()))
            {
                MessageBox.Show("Nie można nawiązać rozmowy z tym adresem IP (prawdopodobnie taka konwersacja już trwa).");
                return;
            }
            if (Rozmowy.kartyRozmow.Count >= 8)
            {
                MessageBox.Show("Zbyt wiele otwartych kart, nie można otworzyć kolejnego okna rozmowy.");
                return;
            }
            var task = new Task(() =>
            {
                try
                {
                    TcpClient outClientSocket = new TcpClient(adresIP.ToString(), NasluchTCP.portDoNasluchu);
                    NetworkStream networkStream = outClientSocket.GetStream();
                    BinaryWriter binaryWriter = new BinaryWriter(networkStream);
                    binaryWriter.Write(Application.Current.Properties["Pseudonim"] as String);
                    BinaryReader binaryReader = new BinaryReader(networkStream);
                    string pseudonimOdbierajacego = binaryReader.ReadString();
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Rozmowy.DodajKarteNowejRozmowy(pseudonimOdbierajacego, adresIP.ToString(), networkStream);
                    });
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Nie udało się nawiązać połączenia z adresem: " + adresIP.ToString() + ".\n" + ex.Message);
                }
            });
            task.Start();
        }
    }
}
