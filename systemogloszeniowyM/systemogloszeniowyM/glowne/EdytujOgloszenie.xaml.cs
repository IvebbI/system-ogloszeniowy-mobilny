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
    public partial class EdytujOgloszenie : ContentPage
    {
        private readonly BazaDanych _dataAccess;
        Ogloszenie ogloszenia;
        public EdytujOgloszenie(Ogloszenie ogloszenie)
        {
            _dataAccess = new BazaDanych(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BazaDanych.db3"));
            InitializeComponent();
            ogloszenia= ogloszenie;
            NazwaOfertyEntry.Text = ogloszenie.Nazwa;
            PoziomStanowiskaEntry.Text = ogloszenie.PoziomStanowiska;
            RodzajUmowyEntry.Text = ogloszenie.RodzajUmowy;
            WymiarEtatuEntry.Text = ogloszenie.WymiarEtatu;
            RodzajPracyEntry.Text = ogloszenie.RodzajPracy;
            WynagrodzenieEntry.Text = ogloszenie.Wynagrodzenie;
            DniPracyEntry.Text = ogloszenie.DniPracy;
            GodzinyPracyEntry.Text = ogloszenie.Godzinypracy;
            DataWaznosciEntry.Text = ogloszenie.DataWaznosci;
            KategoriaEntry.Text = ogloszenie.Kategoria;
            ZakresObowiazkowEntry.Text = ogloszenie.ZakresObowiazkow;
            OferowaneBenefityEntry.Text = ogloszenie.OferowaneBenefity;
            WymaganiaEntry.Text = ogloszenie.Wymagania;
            InformacjeEntry.Text = ogloszenie.Informacje;

        }

        private void Edytuj_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(NazwaOfertyEntry.Text) ||
                string.IsNullOrEmpty(PoziomStanowiskaEntry.Text) ||
                string.IsNullOrEmpty(RodzajUmowyEntry.Text) ||
                string.IsNullOrEmpty(WymiarEtatuEntry.Text) ||
                string.IsNullOrEmpty(RodzajPracyEntry.Text) ||
                string.IsNullOrEmpty(WynagrodzenieEntry.Text) ||
                string.IsNullOrEmpty(DniPracyEntry.Text) ||
                string.IsNullOrEmpty(GodzinyPracyEntry.Text) ||
                string.IsNullOrEmpty(DataWaznosciEntry.Text) ||
                string.IsNullOrEmpty(KategoriaEntry.Text) ||
                string.IsNullOrEmpty(ZakresObowiazkowEntry.Text) ||
                string.IsNullOrEmpty(OferowaneBenefityEntry.Text) ||
                string.IsNullOrEmpty(WymaganiaEntry.Text) ||
                string.IsNullOrEmpty(InformacjeEntry.Text))
            {
                DisplayAlert("Błąd", "Wszystkie pola muszą być wypełnione", "OK");
                return;
            }
            ogloszenia.Nazwa = NazwaOfertyEntry.Text;
            ogloszenia.PoziomStanowiska = PoziomStanowiskaEntry.Text;
            ogloszenia.RodzajUmowy = RodzajUmowyEntry.Text;
            ogloszenia.WymiarEtatu = WymiarEtatuEntry.Text;
            ogloszenia.RodzajPracy = RodzajPracyEntry.Text;
            ogloszenia.Wynagrodzenie = WynagrodzenieEntry.Text;
            ogloszenia.DniPracy = DniPracyEntry.Text;
            ogloszenia.Godzinypracy = GodzinyPracyEntry.Text;
            ogloszenia.DataWaznosci = DataWaznosciEntry.Text;
            ogloszenia.Kategoria = KategoriaEntry.Text;
            ogloszenia.ZakresObowiazkow = ZakresObowiazkowEntry.Text;
            ogloszenia.OferowaneBenefity = OferowaneBenefityEntry.Text;
            ogloszenia.Wymagania = WymaganiaEntry.Text;
            ogloszenia.Informacje = InformacjeEntry.Text;
            _dataAccess.EdytujOgloszenie(ogloszenia);
            Navigation.PopAsync();
        }


        private void Anuluj_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}