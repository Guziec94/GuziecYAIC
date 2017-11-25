using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GuziecYAIC
{
    public static class Rozmowy
    {
        static TabControl pasekRozmow;
        static List<TabItem> kartyRozmow;

        public static void Inicjalizuj(TabControl tabControl)
        {
            pasekRozmow = tabControl;
            DodajDomyslnaKarte();
        }

        public static void DodajDomyslnaKarte()
        {
            TabItem kartaNowejRozmowy = new TabItem();
            kartaNowejRozmowy.Header = "Rozpocznij nową rozmowę";
            Frame zawartoscKarty = new Frame();
            zawartoscKarty.Navigate(new RozpocznijNowaRozmowe());
            kartaNowejRozmowy.Content = zawartoscKarty;
            pasekRozmow.Items.Add(kartaNowejRozmowy);
        }

        public static void DodajKarteNowejRozmowy(string adresIP)
        {
            TabItem kartaNowejRozmowy = new TabItem();
            kartaNowejRozmowy.Header = adresIP;
            Frame zawartoscKarty = new Frame();
            zawartoscKarty.Navigate(new RozpocznijNowaRozmowe());
            kartaNowejRozmowy.Content = zawartoscKarty;
            pasekRozmow.Items.Add(kartaNowejRozmowy);

        }
    }
}
