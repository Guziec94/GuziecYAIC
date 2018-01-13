using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace GuziecYAIC
{
    public enum TypWiadomosci
    {
        inny = 0,
        doMnie = 1,
        odeMnie = 2,
    }

    class Wiadomosc
    {
        string tresc;
        TypWiadomosci typWiadomosci;
        DateTime czasNadejscia;
        ItemsControl obiektUI;

        public Wiadomosc(TypWiadomosci typWiadomosci, string tresc)
        {
            this.typWiadomosci = typWiadomosci;
            this.tresc = tresc;
            czasNadejscia = DateTime.Now;
        }
        
        public ItemsControl GenerujObiektUI()
        {
            if (obiektUI != null)
            {
                return obiektUI;
            }

            ItemsControl stpaWiadomosc = new ItemsControl();
            switch (typWiadomosci)
            {
                case TypWiadomosci.inny:
                    stpaWiadomosc.HorizontalAlignment = HorizontalAlignment.Stretch;
                    stpaWiadomosc.Background = new SolidColorBrush(Colors.OrangeRed);
                    stpaWiadomosc.Margin = new Thickness(25, 5, 25, 5);
                    break;
                case TypWiadomosci.doMnie:
                    stpaWiadomosc.HorizontalAlignment = HorizontalAlignment.Left;
                    stpaWiadomosc.Background = new SolidColorBrush(Colors.LimeGreen);
                    stpaWiadomosc.Margin = new Thickness(10, 5, 25, 5);
                    break;
                case TypWiadomosci.odeMnie:
                    stpaWiadomosc.HorizontalAlignment = HorizontalAlignment.Right;
                    stpaWiadomosc.Background = new SolidColorBrush(Colors.AliceBlue);
                    stpaWiadomosc.Margin = new Thickness(25, 5, 10, 5);
                    break;
            }
            TextBlock txtTresc = new TextBlock()
            {
                Text = tresc,
                FontSize = 12,
                Margin = new Thickness(5),
                TextWrapping = TextWrapping.Wrap
            };
            TextBlock txtCzasNadejscia = new TextBlock()
            {
                Text = czasNadejscia.ToString(),
                FontSize = 10,
                Margin = new Thickness(5),
                HorizontalAlignment = HorizontalAlignment.Right
            };
            stpaWiadomosc.Items.Add(txtTresc);
            stpaWiadomosc.Items.Add(txtCzasNadejscia);
            obiektUI = stpaWiadomosc;
            return stpaWiadomosc;
        }
    }
}
