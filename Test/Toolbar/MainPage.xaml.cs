using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Test.Toolbar
{
    //MainPage = new NavigationPage(new Toolbar.MainPage());

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        void Handle_Activated(object sender, System.EventArgs e)
        {
            DisplayAlert("Activated", "Toolbar activated", "OK");
        }
    }
}
