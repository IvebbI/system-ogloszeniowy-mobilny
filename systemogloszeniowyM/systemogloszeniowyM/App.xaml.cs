﻿using System;
using System.IO;
using systemogloszeniowyM.glowne;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace systemogloszeniowyM
{
    public partial class App : Application
    {
        static BazaDanych Database;

        public static BazaDanych DataAccess
        {
            get
            {
                if (Database == null)
                {
                    Database = new BazaDanych(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"BazaDanych.db3"));
                }
                return Database;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
