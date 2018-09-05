using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Form
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
