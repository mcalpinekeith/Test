using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Test.Navigation
{
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
            //NavigationPage.SetHasNavigationBar(this, false);
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            //await Navigation.PushAsync(new IntroductionPage());
            await Navigation.PushModalAsync(new IntroductionPage());
        }
    }
}
