using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using systemogloszeniowyM.Tabele;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace systemogloszeniowyM.glowne
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OfertyPracy : ContentPage
    {
        private readonly BazaDanych _dataAccess;
        private Sesja _sesja;

        public OfertyPracy ()
		{
			InitializeComponent ();
              _dataAccess = new BazaDanych(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BazaDanych.db3"));
            WyswietlWszystkieOgloszenia();
		}
        private async void WyswietlWszystkieOgloszenia()
        {
            List<Ogloszenie> ogloszenia = await _dataAccess.PobierzWszystkieOgloszeniaAsync();
            ListaOgloszen.ItemsSource= ogloszenia;
            
        }

    }
}