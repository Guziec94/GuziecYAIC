using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GuziecYAIC
{
    static class NasluchTCP
    {
        static TcpListener tcpListener;
        static IPAddress adresDoNasluchu;
        static int portDoNasluchu;
        public static Task nasluchiwanie;

        static public bool RozpocznijNasluch()
        {
            try
            {
                portDoNasluchu = (int) Application.Current.Properties["PortNasluchu"];
                adresDoNasluchu = Application.Current.Properties["AdresIPNasluchu"] as IPAddress;
                tcpListener = new TcpListener(adresDoNasluchu, portDoNasluchu);
                nasluchiwanie = new Task(() =>
                {
                    tcpListener.Start();
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        static public bool ZakonczNasluch()
        {
            try
            {
                nasluchiwanie.Dispose();
                nasluchiwanie = null;
                tcpListener.Stop();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
