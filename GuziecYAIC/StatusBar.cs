using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                txtStatusBar.Content = tekst;
            }
        }
    }
}
