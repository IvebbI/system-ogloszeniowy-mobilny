using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using systemogloszeniowyM.Tabele;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace systemogloszeniowyM.glowne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilUzytkownika : ContentPage
    {
        private readonly BazaDanych _dataAccess;
        private Sesja _sesja;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            WyswietlDaneUzytkownika();
            WyswietlDaneDoswiadczenia();
        }

        public ProfilUzytkownika()
        {
            InitializeComponent();
            _dataAccess = new BazaDanych(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BazaDanych.db3"));

        }

        private async void WyswietlDaneDoswiadczenia()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje =  _dataAccess.PobierzDaneDoswiadczenia(_sesja);
                    if (informacje != null)
                    {
                        DoswiadczenieListView.ItemsSource = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych doświadczenia: {ex.Message}", "OK");
            }
        }

        private void przyciskdoedytacjeD_Clicked(object sender, EventArgs e)
        {
            try
            {
                DoswiadczenieListView.IsVisible = false;
                DoswiadczenieFormularz.IsVisible = true;
                var informacje =  _dataAccess.PobierzDaneDoswiadczenia(_sesja);

                if (informacje != null)
                {
                    DoswiadczenieFormularz.BindingContext = informacje;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas edycji danych doświadczenia: {ex.Message}", "OK");
            }
        }


        private async void WyswietlDaneUzytkownika()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneUzytkownika(_sesja);

                    if (informacje != null)
                    {
                        DaneUzytkownika.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych użytkownika: {ex.Message}", "OK");
            }
        }

        private void przyciskdoedytacje_Clicked(object sender, EventArgs e)
        {
            try
            {
                DaneUzytkownika.IsVisible = false;
                DaneUzytkownikaFormularz.IsVisible = true;
                var informacje = _dataAccess.PobierzDaneUzytkownika(_sesja);

                if (informacje != null)
                {
                    DaneUzytkownikaFormularz.BindingContext = informacje;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
        public async void OdswiezDaneUzytkownika()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneUzytkownika(_sesja);

                    if (informacje != null)
                    {
                        DaneUzytkownika.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", $"Wystąpił błąd podczas odświeżania danych użytkownika: {ex.Message}", "OK");
            }
        }

        private async void PotwierdzEdytacje(object sender, EventArgs e)
        {
            try
            {
                var informacje = (Uzytkownik)DaneUzytkownikaFormularz.BindingContext;

                if (informacje != null)
                {
                    informacje.LinkDoZdjecia = LinkDoZdjeciaEntry.Text;
                    informacje.Email = EmailEntry.Text;
                    informacje.Imie = ImieEntry.Text;
                    informacje.Nazwisko = NazwiskoEntry.Text;
                    informacje.DataUrodzenia = DataUrodzeniaEntry.Text;
                    informacje.Telefon = TelefonEntry.Text;
                    informacje.Adres = AdresEntry.Text;
                    informacje.StanowiskoPracy = StanowiskoPracyEntry.Text;
                    informacje.OpisPracy = OpisPracyEntry.Text;
                    informacje.PodsumowanieZawodowe = PodsumowanieZawodoweEntry.Text;
                    informacje.GithubProfil = GithubProfilEntry.Text;

                    await _dataAccess.ZapiszDaneUzytkownika(informacje);
                    DaneUzytkownikaFormularz.IsVisible= false;
                    DaneUzytkownika.IsVisible = true;
                    OdswiezDaneUzytkownika();
                }

                await _dataAccess.ZapiszDaneUzytkownika(informacje);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas potwierdzania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }





        private void Anuluj(object sender, EventArgs e)
        {
            try
            {
                DaneUzytkownikaFormularz.IsVisible = false;
                DaneUzytkownika.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas anulowania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
    }
}
