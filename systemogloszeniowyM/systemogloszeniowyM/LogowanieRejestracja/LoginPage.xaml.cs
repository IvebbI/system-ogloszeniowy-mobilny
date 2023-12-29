using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using systemogloszeniowyM.glowne;
using systemogloszeniowyM.Tabele;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace systemogloszeniowyM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private async void Zaloguj(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string haslo = hasloEntry.Text;

            if (WalidacjaDanych.CzyUzupelnione(email) || WalidacjaDanych.CzyUzupelnione(haslo))
            {
                await DisplayAlert("Błąd!", "Nie uzupełniłeś wszystkich danych!", "Ok");
                return;
            }

            Uzytkownik zalogowanyUzytkownik = await App.DataAccess.ZalogujUzytkownika(email, haslo);

            if (zalogowanyUzytkownik != null)
            {
                Sesja sesja = new Sesja
                {
                    idUzytkownika = zalogowanyUzytkownik.Id,
                    TypZalogowanego = "Uzytkownik"
                };
                App.DataAccess.ZapiszSesje(sesja);
                await Navigation.PushAsync(new Nawigacja());
            }
            else
            {   
                await DisplayAlert("Błąd!", "Błędny adres e-mail lub hasło.", "Ok");
            }
        }

        private void NawigacjaRejestracja(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}