using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace systemogloszeniowyM
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void NawigacjaUzytkownik(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());  
        }

        private void NawigacjaFirma(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPageF());
        }
    }
}
