using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace systemogloszeniowyM.glowne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfertyPracy : ContentPage
    {
        private readonly BazaDanych _dataAccess;
        private List<WyswietlanieOgloszeniaIFirmy> allOgloszenia;
        private ObservableCollection<WyswietlanieOgloszeniaIFirmy> filteredOgloszenia;

        public OfertyPracy()
        {
            InitializeComponent();
            allOgloszenia = new List<WyswietlanieOgloszeniaIFirmy>();
            filteredOgloszenia = new ObservableCollection<WyswietlanieOgloszeniaIFirmy>();
            LoadOgloszenia();

            ListaOgloszen.ItemsSource = allOgloszenia; 
            SearchBars.TextChanged += OnSearchBarsTextChanged;
        }

        private void OnSearchBarsTextChanged(object sender, TextChangedEventArgs e)
        {
            string keyword = e.NewTextValue.ToLower();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                ListaOgloszen.ItemsSource = allOgloszenia;
            }
            else
            {
                filteredOgloszenia.Clear();
                foreach (var ogloszenie in allOgloszenia.Where(o => o.Nazwa.ToLower().Contains(keyword)))
                {
                    filteredOgloszenia.Add(ogloszenie);
                }
                ListaOgloszen.ItemsSource = filteredOgloszenia;
            }
        }

        private void LoadOgloszenia()
        {
            foreach (var firma in App.DataAccess.PobierzFirme())
            {
                foreach (var ogloszenie in App.DataAccess.PobierzOgloszneia())
                {
                    allOgloszenia.Add(new WyswietlanieOgloszeniaIFirmy(ogloszenie, firma));
                }
            }
        }

        private void ZobaczOgloszenie(object sender, EventArgs e)
        {
            WyswietlanieOgloszeniaIFirmy item = ((Button)sender).CommandParameter as WyswietlanieOgloszeniaIFirmy;
            Navigation.PushAsync(new SzczegolyOgloszenia(item));
        }
    }
}
