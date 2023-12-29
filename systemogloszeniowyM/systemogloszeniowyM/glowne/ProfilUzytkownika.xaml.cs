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
                        DoswiadczenieUzytkownika.BindingContext = informacje;
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
                DoswiadczenieUzytkownika.IsVisible = false;
                DoswiadczenieUzytkownikaFormularz.IsVisible = true;
                var informacje =  _dataAccess.PobierzDaneDoswiadczenia(_sesja);

                if (informacje != null)
                {
                    DoswiadczenieUzytkownikaFormularz.BindingContext = informacje;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas edycji danych doświadczeniaaaa111: {ex.Message}", "OK");
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
        private async void PotwierdzEdytacjeD(object sender, EventArgs e)
        {
            try
            {
                var informacje = (Doswiadczenie)DaneUzytkownikaFormularz.BindingContext;

                if (informacje != null)
                {
                    informacje.Stanowisko = StanowiskoEntry.Text;
                    informacje.NazwaFirmy = NazwaFirmyEntry.Text;
                    informacje.Lokalizacja = LokalizacjaEntry.Text;
                    informacje.OkresZatrudnieniaOd = OkresZatrudnieniaOdEntry.Text;
                    informacje.OkresZatrudnieniaDo = OkresZatrudnieniaDoEntry.Text;
                    informacje.Obowiazki = ObowiazkiEntry.Text;

                    await _dataAccess.ZapiszDaneDoswiadczenia(informacje);
                    OdswiezDaneDoswiadczenia(); 
                }
                else
                {
                    var noweDoswiadczenie = new Doswiadczenie
                    {
                        Stanowisko = StanowiskoEntry.Text,
                        NazwaFirmy = NazwaFirmyEntry.Text,
                        Lokalizacja = LokalizacjaEntry.Text,
                        OkresZatrudnieniaOd = OkresZatrudnieniaOdEntry.Text,
                        OkresZatrudnieniaDo = OkresZatrudnieniaDoEntry.Text,
                        Obowiazki = ObowiazkiEntry.Text?.ToString()
                    };

                    await _dataAccess.ZapiszDaneDoswiadczenia(noweDoswiadczenie);
                    OdswiezDaneDoswiadczenia();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas potwierdzania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }

        private async void OdswiezDaneDoswiadczenia()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje =  _dataAccess.PobierzDaneDoswiadczenia(_sesja);
                    if (informacje != null)
                    {
                        DoswiadczenieUzytkownikaFormularz.IsVisible = false;
                        DoswiadczenieUzytkownika.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych doświadczenia: {ex.Message}", "OK");
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
        private void AnulujD(object sender, EventArgs e)
        {
            try
            {
                DoswiadczenieUzytkownikaFormularz.IsVisible = false;
                DoswiadczenieUzytkownika.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas anulowania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
    }
}
