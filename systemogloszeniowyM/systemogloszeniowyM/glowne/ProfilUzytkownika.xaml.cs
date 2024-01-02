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
     
          
        }

        public ProfilUzytkownika()
        {
            InitializeComponent();
            _dataAccess = new BazaDanych(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BazaDanych.db3"));
            Inicjalizacja();
        }
        private async void Inicjalizacja()
        {
            _sesja = await _dataAccess.PobierzSesje(); 

            if (_sesja != null)
            {
                if (_sesja.TypZalogowanego == "Uzytkownik")
                {
                    frameDaneuzytkownika.IsVisible = true;
                    FrameDoswiadczenie.IsVisible= true;
                    FrameWyksztalcenie.IsVisible= true;
                    frameKurs.IsVisible= true;
                    frameJezyk.IsVisible= true;
                    frameUmiejetnosci.IsVisible = true;
                    WyswietlDaneUzytkownika();
                    WyswietlDaneDoswiadczenia();
                    WyswietlDaneWyksztalcenia();
                    WyswietlDaneKursu();
                    WyswietlDaneJezyku();
                    WyswietlDaneUmiejetnosci();
                }
                else if (_sesja.TypZalogowanego == "Firma")
                {
                    FirmaDane.IsVisible = true;
                    WyswietlDaneFirma();
                }
            }
        }
        //Wyswietlanie 
        private async void WyswietlDaneFirma()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneFirmy(_sesja);

                    if (informacje != null)
                    {
                        FirmaDanePodstawowe.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych użytkownika: {ex.Message}", "OK");
            }
        }

        private async void WyswietlDaneDoswiadczenia()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneDoswiadczenia(_sesja);
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
        private async void WyswietlDaneWyksztalcenia()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneWyksztalcenia(_sesja);
                    if (informacje != null)
                    {
                        WyksztalcenieUzytkownika.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych doświadczenia: {ex.Message}", "OK");
            }
        }
        private async void WyswietlDaneKursu()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneKursu(_sesja);
                    if (informacje != null)
                    {
                        KursUzytkownika.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych doświadczenia: {ex.Message}", "OK");
            }
        }
        private async void WyswietlDaneJezyku()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneJezyku(_sesja);
                    if (informacje != null)
                    {
                        JezykUzytkownika.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych doświadczenia: {ex.Message}", "OK");
            }
        }
        private async void WyswietlDaneUmiejetnosci()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneUmiejetnosci(_sesja);
                    if (informacje != null)
                    {
                        UmiejetnoscUzytkownika.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych doświadczenia: {ex.Message}", "OK");
            }
        }

        //Edycja pierwsza

        private void przyciskdoedytacjeD_Clicked(object sender, EventArgs e)
        {
            try
            {
                DoswiadczenieUzytkownika.IsVisible = false;
                DoswiadczenieUzytkownikaFormularz.IsVisible = true;
                var informacje = _dataAccess.PobierzDaneDoswiadczenia(_sesja);

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

        private void przyciskdoedytacjeW_Clicked(object sender, EventArgs e)
        {
            try
            {
                WyksztalcenieUzytkownika.IsVisible = false;
                WyksztalcenieUzytkownikaFormularz.IsVisible = true;
                var informacje = _dataAccess.PobierzDaneWyksztalcenia(_sesja);

                if (informacje != null)
                {
                    WyksztalcenieUzytkownikaFormularz.BindingContext = informacje;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas edycji danych doświadczeniaaaa111: {ex.Message}", "OK");
            }
        }

        private void przyciskdoedytacjeFirma_Clicked(object sender, EventArgs e)
        {
            try
            {
                FirmaDanePodstawowe.IsVisible = false;
                FirmaDanePodstawoweFormularz.IsVisible = true;
                var informacje = _dataAccess.PobierzDaneFirmy(_sesja);

                if (informacje != null)
                {
                    FirmaDanePodstawoweFormularz.BindingContext = informacje;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas edycji danych użytkownika: {ex.Message}", "OK");
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
        private void przyciskdoedytacjeK_Clicked(object sender, EventArgs e)
        {
            try
            {
                KursUzytkownika.IsVisible = false;
                KursUzytkownikaFormularz.IsVisible = true;
                var informacje = _dataAccess.PobierzDaneKursu(_sesja);

                if (informacje != null)
                {
                    KursUzytkownikaFormularz.BindingContext = informacje;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
        private void przyciskdoedytacjeJ_Clicked(object sender, EventArgs e)
        {
            try
            {
                JezykUzytkownika.IsVisible = false;
                JezykUzytkownikaFormularz.IsVisible = true;
                var informacje = _dataAccess.PobierzDaneJezyku(_sesja);

                if (informacje != null)
                {
                    JezykUzytkownikaFormularz.BindingContext = informacje;
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
        private void przyciskdoedytacjeU_Clicked(object sender, EventArgs e)
        {
            try
            {
                UmiejetnoscUzytkownika.IsVisible = false;
                UmiejetnoscUzytkownikaFormularz.IsVisible = true;
                var informacje = _dataAccess.PobierzDaneUmiejetnosci(_sesja);

                if (informacje != null)
                {
                    UmiejetnoscUzytkownikaFormularz.BindingContext = informacje;
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
        public async void OdswiezDaneFirmy()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneFirmy(_sesja);

                    if (informacje != null)
                    {
                        FirmaDanePodstawowe.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Błąd", $"Wystąpił błąd podczas odświeżania danych użytkownika: {ex.Message}", "OK");
            }
        }
        //potwierdzenie edytacji

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
                    DaneUzytkownikaFormularz.IsVisible = false;
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
        private async void PotwierdzEdytacjeFirma(object sender, EventArgs e)
        {
            try
            {
                var informacje = (Firma)FirmaDanePodstawoweFormularz.BindingContext;

                if (informacje != null)
                {
                    informacje.Nazwa = NazwaFirmaEntry.Text;
                    informacje.Adres=AdresFirmyEntry.Text;
                    informacje.StronaInternetowa=StronaInternetowaEntry.Text;

                    await _dataAccess.ZapiszDaneFirmy(informacje);
                    FirmaDanePodstawoweFormularz.IsVisible = false;
                    FirmaDanePodstawowe.IsVisible = true;
                    OdswiezDaneFirmy();
                }

                await _dataAccess.ZapiszDaneFirmy(informacje);
                OdswiezDaneFirmy();
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
                var informacje = _dataAccess.PobierzDaneDoswiadczenia(_sesja);

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
                    informacje = new Doswiadczenie
                    {
                        Stanowisko = StanowiskoEntry.Text,
                        NazwaFirmy = NazwaFirmyEntry.Text,
                        Lokalizacja = LokalizacjaEntry.Text,
                        OkresZatrudnieniaOd = OkresZatrudnieniaOdEntry.Text,
                        OkresZatrudnieniaDo = OkresZatrudnieniaDoEntry.Text,
                        IdUzytkownika = _sesja.idUzytkownika,
                        Obowiazki = ObowiazkiEntry.Text?.ToString()
                    };

                    await _dataAccess.ZapiszDaneDoswiadczenia(informacje);
                    OdswiezDaneDoswiadczenia();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas potwierdzania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
        private async void PotwierdzEdytacjeW(object sender, EventArgs e)
        {
            try
            {
                var informacje = _dataAccess.PobierzDaneWyksztalcenia(_sesja);

                if (informacje != null)
                {
                    informacje.NazwaSzkoly = NazwaSzkolyEntry.Text;
                    informacje.Miejscowosc = MiejscowoscEntry.Text;
                    informacje.PoziomWyksztalcenia = PoziomWyksztalceniaEntry.Text;
                    informacje.Kierunek = KierunekEntry.Text;
                    informacje.UczestniczylOd = UczestniczyłODEntry.Text;
                    informacje.UczestniczylDo = UczestniczylDo.Text;
           


                    await _dataAccess.ZapiszDaneWyksztalcenia(informacje);
                    OdswiezDaneWyksztalcenia();
                }
                else
                {
                    informacje = new Wyksztalcenie
                    {
                        NazwaSzkoly = NazwaSzkolyEntry.Text,
                        Miejscowosc = MiejscowoscEntry.Text,
                        PoziomWyksztalcenia = PoziomWyksztalceniaEntry.Text,
                        Kierunek = KierunekEntry.Text,
                        UczestniczylOd = UczestniczyłODEntry.Text,
                        UczestniczylDo = UczestniczylDo.Text,
                        IdUzytkownika = _sesja.idUzytkownika,
                     
                    };

                    await _dataAccess.ZapiszDaneWyksztalcenia(informacje);
                    OdswiezDaneWyksztalcenia();
                }
            }   
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas potwierdzania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
        private async void PotwierdzEdytacjeK(object sender, EventArgs e)
        {
            try
            {
                var informacje = _dataAccess.PobierzDaneKursu(_sesja);

                if (informacje != null)
                {
                    informacje.NazwaSzkolenia=NazwaSzkoleniaEntry.Text;
                    informacje.Organizator=OrganizatorEntry.Text;
                    informacje.Data=DataKursuEntry.Text;



                    await _dataAccess.ZapiszDaneKursu(informacje);
                    OdswiezDaneKursu();
                }
                else
                {
                    informacje = new Kurs
                    {
                        NazwaSzkolenia = NazwaSzkoleniaEntry.Text,
                        Organizator = OrganizatorEntry.Text,
                        Data=DataKursuEntry.Text,
                        IdUzytkownika = _sesja.idUzytkownika,


                    };

                    await _dataAccess.ZapiszDaneKursu(informacje);
                    OdswiezDaneKursu();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas potwierdzania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
        private async void PotwierdzEdytacjeJ(object sender, EventArgs e)
        {
            try
            {
                var informacje = _dataAccess.PobierzDaneJezyku(_sesja);

                if (informacje != null)
                {
                   informacje.NazwaJezyka=NazwaJezykaEntry.Text;
                   informacje.Poziom=PoziomEntry.Text;



                    await _dataAccess.ZapiszDanejezyku(informacje);
                    OdswiezDaneJezyku();
                }
                else
                {
                    informacje = new Jezyk
                    {
                        NazwaJezyka = NazwaJezykaEntry.Text,
                        Poziom=PoziomEntry.Text,
                        IdUzytkownika = _sesja.idUzytkownika,

                    };

                    await _dataAccess.ZapiszDanejezyku(informacje);
                    OdswiezDaneJezyku();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas potwierdzania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
        private async void PotwierdzEdytacjeU(object sender, EventArgs e)
        {
            try
            {
                var informacje = _dataAccess.PobierzDaneUmiejetnosci(_sesja);

                if (informacje != null)
                {
                    informacje.NazwaUmiejetnosci=UmiejetnosciEntry.Text;
                   
            



                    await _dataAccess.ZapiszDaneUmiejetnosci(informacje);
                    OdswiezDaneUmiejetnosci();
                }
                else
                {
                    informacje = new Umiejetnosc
                    {
                        NazwaUmiejetnosci = UmiejetnosciEntry.Text,
                        IdUzytkownika = _sesja.idUzytkownika,

                    };

                    await _dataAccess.ZapiszDaneUmiejetnosci(informacje);
                    OdswiezDaneUmiejetnosci();
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas potwierdzania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
        //odswiezanie
        private async void OdswiezDaneDoswiadczenia()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneDoswiadczenia(_sesja);
                    if (informacje != null)
                    {
                        DoswiadczenieUzytkownika.IsVisible = true;
                        DoswiadczenieUzytkownikaFormularz.IsVisible = false;
                        DoswiadczenieUzytkownika.BindingContext = informacje;
                        DoswiadczenieUzytkownikaFormularz.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych doświadczenia: {ex.Message}", "OK");
            }
        }
        private async void OdswiezDaneWyksztalcenia()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneWyksztalcenia(_sesja);
                    if (informacje != null)
                    {
                        WyksztalcenieUzytkownika.IsVisible = true;
                        WyksztalcenieUzytkownikaFormularz.IsVisible = false;
                        WyksztalcenieUzytkownika.BindingContext = informacje;
                        WyksztalcenieUzytkownikaFormularz.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych doświadczenia: {ex.Message}", "OK");
            }
        }
        private async void OdswiezDaneKursu()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneKursu(_sesja);
                    if (informacje != null)
                    {
                        KursUzytkownika.IsVisible = true;
                        KursUzytkownikaFormularz.IsVisible = false;
                        KursUzytkownika.BindingContext = informacje;
                        KursUzytkownikaFormularz.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych doświadczenia: {ex.Message}", "OK");
            }
        }
        private async void OdswiezDaneJezyku()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneJezyku(_sesja);
                    if (informacje != null)
                    {
                        JezykUzytkownika.IsVisible = true;
                        JezykUzytkownikaFormularz.IsVisible = false;
                        JezykUzytkownika.BindingContext = informacje;
                        JezykUzytkownikaFormularz.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych doświadczenia: {ex.Message}", "OK");
            }
        }
        private async void OdswiezDaneUmiejetnosci()
        {
            try
            {
                _sesja = await _dataAccess.PobierzSesje();
                if (_sesja != null)
                {
                    var informacje = _dataAccess.PobierzDaneUmiejetnosci(_sesja);
                    if (informacje != null)
                    {
                        UmiejetnoscUzytkownika.IsVisible = true;
                        UmiejetnoscUzytkownikaFormularz.IsVisible = false;
                        UmiejetnoscUzytkownika.BindingContext = informacje;
                        UmiejetnoscUzytkownikaFormularz.BindingContext = informacje;
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas pobierania danych doświadczenia: {ex.Message}", "OK");
            }
        }



        //przyciski anuluj
        private void AnulujK(object sender, EventArgs e)
        {
            try
            {
                KursUzytkownikaFormularz.IsVisible = false;
                KursUzytkownika.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas anulowania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
        private void AnulujW(object sender, EventArgs e)
        {
            try
            {
                WyksztalcenieUzytkownikaFormularz.IsVisible = false;
                WyksztalcenieUzytkownika.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas anulowania edycji danych użytkownika: {ex.Message}", "OK");
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
        private void AnulujJ(object sender, EventArgs e)
        {
            try
            {
                JezykUzytkownikaFormularz.IsVisible = false;
                JezykUzytkownika.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas anulowania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
        private void AnulujU(object sender, EventArgs e)
        {
            try
            {
                UmiejetnoscUzytkownikaFormularz.IsVisible = false;
                UmiejetnoscUzytkownika.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas anulowania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
        private void AnulujF(object sender, EventArgs e)
        {
            try
            {
                FirmaDanePodstawoweFormularz.IsVisible = false;
                FirmaDanePodstawowe.IsVisible = true;
            }
            catch (Exception ex)
            {
                DisplayAlert("Błąd", $"Wystąpił błąd podczas anulowania edycji danych użytkownika: {ex.Message}", "OK");
            }
        }
    }
}
