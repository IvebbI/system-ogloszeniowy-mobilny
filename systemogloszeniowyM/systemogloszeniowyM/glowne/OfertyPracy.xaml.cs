using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using systemogloszeniowyM.Tabele;
using System.Windows.Input;
using System.Linq;
using System.Collections.ObjectModel;

namespace systemogloszeniowyM.glowne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfertyPracy : ContentPage
    {
        private readonly BazaDanych _dataAccess;

        public OfertyPracy()
        {
            InitializeComponent();
            List<WyswietlanieOgloszeniaIFirmy> WyswietlanieOfert=new List<WyswietlanieOgloszeniaIFirmy>();
            foreach (var item in App.DataAccess.PobierzFirme())
            {
                foreach(var itemm in App.DataAccess.PobierzOgloszneia())
                {
                    WyswietlanieOfert.Add(new WyswietlanieOgloszeniaIFirmy(itemm, item));
                }
            }
            ListaOgloszen.ItemsSource = WyswietlanieOfert;

        }
        private void ZobaczOgloszenie(object sender, EventArgs e)
        {
            WyswietlanieOgloszeniaIFirmy item = ((Button)sender).CommandParameter as WyswietlanieOgloszeniaIFirmy;
            Navigation.PushAsync(new SzczegolyOgloszenia(item));
        }
    }
}
