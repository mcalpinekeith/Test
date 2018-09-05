using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Popup
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            // Confirmation box
            //var response = await DisplayAlert("Warning", "Are you sure?", "Yes", "No");

            //if (response)
            //{
            //    await DisplayAlert("Done", "Your response will be saved.", "OK");
            //}

            var response = await DisplayActionSheet("Title", "Cancel", "Delete", "Copy", "Message", "Email");

            await DisplayAlert("Response", response, "OK");
        }
    }
}
