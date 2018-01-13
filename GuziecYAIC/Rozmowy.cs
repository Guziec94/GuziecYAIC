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
    public static class Rozmowy
    {
        static TabControl pasekRozmow;
        public static List<KartaRozmowy> kartyRozmow;

        public static void Inicjalizuj(TabControl tabControl)
        {
            pasekRozmow = tabControl;
            DodajDomyslnaKarte();
        }

        /// <summary>
        /// Funkcja tworzy nową kartę, w której można rozpocząć nową rozmowę.
        /// </summary>
        public static void DodajDomyslnaKarte()
        {
            pasekRozmow.Items.Add(new KartaTworzeniaRozmowy());
        }

        public static void DodajKarteNowejRozmowy(string adresIP)
        {
            kartyRozmow = kartyRozmow ?? new List<KartaRozmowy>();
            if (kartyRozmow.Count < 8) //3 rzędy po 3 karty
            {
                KartaRozmowy kartaNowejRozmowy = new KartaRozmowy(IPAddress.Parse("123.123.123.123"), "pseudo");//zamienić kiedyś na pseudonim pobrany podczas połączenia
                pasekRozmow.Items.Add(kartaNowejRozmowy);
                kartyRozmow.Add(kartaNowejRozmowy);
            }
            else
            {
                MessageBox.Show("Osiągnięto limit otwartych rozmów.");
            }
        }

        public static void ZakonczRozmowe(Guid guid)
        {
            kartyRozmow.RemoveAt(kartyRozmow.FindIndex(x => x.guid == guid));
            
            //wysłać jakiś komunikat o zamknięciu rozmowy
        }
    }
}
