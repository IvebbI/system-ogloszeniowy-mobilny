using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using systemogloszeniowyM.Tabele;

namespace systemogloszeniowyM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private string generatedVerificationCode;
        private Uzytkownik uzytkownik; 

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void Zarejestruj(object sender, EventArgs e)
        {
            string email = emailEntry.Text;
            string haslo = hasloEntry.Text;
            string powtorzhaslo = powtorzhasloEntry.Text;

            if (haslo != powtorzhaslo)
            {
                await DisplayAlert("Błąd!", "Hasła nie są takie same!", "Ok");
                return;
            }

            if (WalidacjaDanych.CzyUzupelnione(email) || WalidacjaDanych.CzyUzupelnione(haslo) || WalidacjaDanych.CzyUzupelnione(powtorzhaslo))
            {
                await DisplayAlert("Błąd!", "Nie uzupełniłeś wszystkich danych!", "Ok");
                return;
            }

            uzytkownik = new Uzytkownik();
            uzytkownik.Email = email;
            uzytkownik.Haslo = haslo;

            if (await App.DataAccess.CzyEmailJuzIstnieje(email))
            {
                await DisplayAlert("Błąd!", "Użytkownik o podanym adresie e-mail już istnieje!", "OK");
                return;
            }
            else if (await App.DataAccess.CzyEmailJuzIstniejeF(email))
            {
                await DisplayAlert("Błąd!", "Użytkownik o podanym adresie e-mail już istnieje!", "OK");
                return;
            }
            else
            {
                generatedVerificationCode = GenerujKodWeryfikacyjny();
                bool emailSent = await SendVerificationCode(email, generatedVerificationCode);

                if (emailSent)
                {
                    DodajEntryKodWeryfikacyjny(); 
                }
                else
                {
                    await DisplayAlert("Błąd!", "Wystąpił problem podczas wysyłania e-maila weryfikacyjnego.", "OK");
                }
            }
        }

        private string GenerujKodWeryfikacyjny()
        {
            byte[] data = new byte[4];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }
            int code = BitConverter.ToInt32(data, 0);
            return code.ToString("X"); 
        }

        public static async Task<bool> SendVerificationCode(string email, string verificationCode)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.office365.com");

                mail.From = new MailAddress("projektofertypracy123545@outlook.com");
                mail.To.Add(email);
                mail.Subject = "Weryfikacja Użytkownika - kod";
                mail.Body = $"Twój kod do rejestracji to: {verificationCode}";

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new NetworkCredential("projektofertypracy123545@outlook.com", "Haslo12345");
                SmtpServer.EnableSsl = true;

                await SmtpServer.SendMailAsync(mail);

                return true;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Błąd", $"Wystąpił błąd podczas wysyłania e-maila: {ex.Message}", "OK");
                return false;
            }
        }

        private void DodajEntryKodWeryfikacyjny()
        {
            Entry entryKodWeryfikacyjny = new Entry
            {
                Placeholder = "Podaj kod weryfikacyjny",
                Keyboard = Keyboard.Text,
                Margin = new Thickness(10, 0, 10, 0)
            };

            Button sprawdzButton = new Button
            {
                Text = "Sprawdź kod",
                Command = new Command(async () => await SprawdzKodWeryfikacyjny(entryKodWeryfikacyjny.Text))
            };

            StackLayout stackLayout = new StackLayout
            {
                Children = { entryKodWeryfikacyjny, sprawdzButton },
                Margin = new Thickness(10, 10, 10, 0)
            };

            Content = stackLayout;
        }

        private async Task SprawdzKodWeryfikacyjny(string wprowadzonyKod)
        {
            if (wprowadzonyKod == generatedVerificationCode)
            {
                await DisplayAlert("Sukces!", "Kod weryfikacyjny poprawny. Konto zostało zarejestrowane.", "OK");
                await App.DataAccess.StworzUzytkownika(uzytkownik);
                await Navigation.PushAsync(new StronaGlowna());
            }
            else
            {
                await DisplayAlert("Błąd!", "Kod weryfikacyjny niepoprawny.", "OK");
            }
        }

        private void Powrot(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }
    }
}
