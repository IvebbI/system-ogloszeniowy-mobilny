using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using systemogloszeniowyM.Tabele;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace systemogloszeniowyM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {

        public RegisterPage()
        {
            InitializeComponent();

        }
        private void Zarejestruj(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string haslo = hasloEntry.Text;
            string powtorzhaslo = powtorzhasloEntry.Text;
            if (haslo != powtorzhaslo)
            {
                DisplayAlert("Błąd!", "Hasła nie są takie same!", "Ok");
                return;
            }
            if (WalidacjaDanych.CzyUzupelnione(email) || WalidacjaDanych.CzyUzupelnione(haslo) || WalidacjaDanych.CzyUzupelnione(powtorzhaslo))
            {
                DisplayAlert("Błąd!", "Nie uzupełniłeś wszystkich danych!", "Ok");
            }
            Uzytkownik uzytkownik = new Uzytkownik();
            uzytkownik.Email = email;
            uzytkownik.Haslo = haslo;


            App.DataAccess.StworzUzytkownika(uzytkownik);
            Navigation.PushAsync(new StronaGlowna());
            
        }
    }
}