﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Test.Navigation
{
    public partial class IntroductionPage : ContentPage
    {
        public IntroductionPage()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, false);
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            //await Navigation.PopAsync();
            await Navigation.PopModalAsync();
        }

        protected override bool OnBackButtonPressed()
        {
            return true; // Disable Back button functionality
            //return base.OnBackButtonPressed();
        }
    }
}
