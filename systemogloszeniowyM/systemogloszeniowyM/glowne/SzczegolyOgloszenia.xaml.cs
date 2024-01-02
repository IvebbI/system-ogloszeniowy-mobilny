using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using systemogloszeniowyM.Tabele;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace systemogloszeniowyM.glowne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SzczegolyOgloszenia : ContentPage
    {
        public SzczegolyOgloszenia(Ogloszenie ogloszenie)
        {
            InitializeComponent();
        }
    }
}