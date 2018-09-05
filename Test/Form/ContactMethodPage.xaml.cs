using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Test.Form
{
    public partial class ContactMethodPage : ContentPage
    {
        public ListView ContactMethods 
        {
            get
            {
                return this.ContactMethodListView;
            }
        }

        public ContactMethodPage()
        {
            InitializeComponent();

            ContactMethodListView.ItemsSource = new List<string>
            {
                "None",
                "Email",
                "SMS"
            };
        }
    }
}
