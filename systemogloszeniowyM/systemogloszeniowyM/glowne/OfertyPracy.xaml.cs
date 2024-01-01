using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using systemogloszeniowyM.Tabele;

namespace systemogloszeniowyM.glowne
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OfertyPracy : ContentPage
    {
        private readonly BazaDanych _dataAccess;
        private Sesja _sesja;

        public OfertyPracy()
        {
            InitializeComponent();
            _dataAccess = new BazaDanych(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BazaDanych.db3"));
            WyswietlOfertyPracy();
        }

        private void WyswietlOfertyPracy()
        {
            List<(Ogloszenie, Firma)> oferty = _dataAccess.PobierzOgloszeniaIFirmy();

            Grid mainGrid = new Grid();

            StackLayout mainStackLayout = FindByName("MainStackLayout") as StackLayout;


            mainStackLayout.Children.Add(mainGrid);

            for (int i = 0; i < oferty.Count; i++)
            {
                Ogloszenie ogloszenie = oferty[i].Item1;
                Firma firma = oferty[i].Item2;

                if (i % 3 == 0)
                {
                    mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                }
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                Frame frame = CreateOgloszenieFrame(ogloszenie, firma);

                mainGrid.Children.Add(frame);
                Grid.SetRow(frame, i / 3);
                Grid.SetColumn(frame, i % 3);
            }
        }

        private Frame CreateOgloszenieFrame(Ogloszenie ogloszenie, Firma firma)
        {
            Frame frame = new Frame
            {
                BorderColor = Color.Black,
                CornerRadius = 5,
                Margin = new Thickness(10),
                HeightRequest = 200,
                BackgroundColor = Color.Transparent,
            };

            frame.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    PrzejdzDoStronySzczegolowUzytkownika(ogloszenie, firma);
                })
            });

            Image image = CreateFirmaLogoImage(firma);

            Label nazwaFirmyLabel = new Label
            {
                Text = firma.Nazwa,
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Start,
                VerticalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(5),
            };

            Label widełkiWynagrodzeniaLabel = new Label
            {
                Text = ogloszenie.Wynagrodzenie,
                Margin = new Thickness(5),
            };

            Label rodzajPracyLabel = new Label
            {
                Text = ogloszenie.RodzajPracy,
                Margin = new Thickness(5),
            };

            Label adresFirmyLabel = new Label
            {
                Text = "Adres: " + firma.Adres,
                Margin = new Thickness(5),
            };

            StackLayout infoStackLayout = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
            };

            infoStackLayout.Children.Add(nazwaFirmyLabel);
            infoStackLayout.Children.Add(widełkiWynagrodzeniaLabel);
            infoStackLayout.Children.Add(rodzajPracyLabel);
            infoStackLayout.Children.Add(adresFirmyLabel);

            StackLayout stackLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };

            if (image != null)
            {
                stackLayout.Children.Add(image);
            }
            stackLayout.Children.Add(infoStackLayout);

            frame.Content = stackLayout;

            return frame;
        }

        private Image CreateFirmaLogoImage(Firma firma)
        {
            Image image = null;
            if (!string.IsNullOrEmpty(firma.LinkDoZdjecia))
            {
                image = new Image
                {
                    HeightRequest = 70,
                    Source = ImageSource.FromUri(new Uri(firma.LinkDoZdjecia)),
                    Margin = new Thickness(5),
                };
            }

            return image;
        }

        private void PrzejdzDoStronySzczegolowUzytkownika(Ogloszenie ogloszenie, Firma firma)
        {
            // Handle the navigation to the details page here
        }
    }
}
