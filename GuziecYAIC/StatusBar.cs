using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GuziecYAIC
{
    public static class StatusBar
    {
        public static Label txtStatusBar;

        public static void Inicjalizuj(Label label)
        {
            txtStatusBar = label;
        }

        public static void SetStatusBarText(string tekst)
        {
            if (txtStatusBar != null)
            {
                string przedrostek = "GuziecYAIC: ";
                if (NasluchTCP.serwerUruchomiony)
                {
                    przedrostek += "(" + (Application.Current.Properties["AdresIPNasluchu"] as IPAddress).ToString() + "): ";
                }
                txtStatusBar.Content = przedrostek + tekst;
            }
        }
    }
}
