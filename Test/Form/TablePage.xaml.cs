using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Test.Form
{
    public partial class TablePage : ContentPage
    {
        public TablePage()
        {
            InitializeComponent();
        }

        void Handle_Tapped(object sender, System.EventArgs e)
        {
            var page = new ContactMethodPage();

            page.ContactMethods.ItemSelected += ContactMethods_ItemSelected;

            Navigation.PushAsync(page);
        }

        void ContactMethods_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ContactMethodLabel.Text = e.SelectedItem.ToString();
            Navigation.PopAsync();
        }

    }
}
