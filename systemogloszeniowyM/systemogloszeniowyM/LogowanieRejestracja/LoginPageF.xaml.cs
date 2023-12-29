using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using systemogloszeniowyM.Tabele;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using systemogloszeniowyM.glowne;

namespace systemogloszeniowyM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPageF : ContentPage
    {
        public LoginPageF()
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

            Firma zalogowanyFirma = await App.DataAccess.ZalogujFirme(email, haslo);

            if (zalogowanyFirma != null)
            {
                Sesja sesja = new Sesja
                {
                    idUzytkownika = zalogowanyFirma.Id,
                    TypZalogowanego = "Firma"
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
            Navigation.PushAsync(new RegisterPageF());
        }
    }
}