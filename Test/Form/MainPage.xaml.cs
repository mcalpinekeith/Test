using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Form
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private List<ContactMethod> _contactMethods = new List<ContactMethod>();

        public MainPage()
        {
            InitializeComponent();

            _contactMethods = new List<ContactMethod>
            {
                new ContactMethod { Id = 1, Name = "SMS" },
                new ContactMethod { Id = 2, Name = "Email" }
            };

            foreach (var contactMethod in _contactMethods)
            {
                ContactMethodPicker.Items.Add(contactMethod.Name);
            }
        }

        void Handle_Toggled(object sender, Xamarin.Forms.ToggledEventArgs e)
        {
            //SwitchLabel.IsVisible = e.Value;
        }

        async void Handle_Completed(object sender, System.EventArgs e)
        {
            await DisplayAlert("Completed", "Completed", "OK");
        }

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            //e.NewTextValue;
            //e.OldTextValue;
            //..for each key press
        }

        void ContactMethodPicker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            //var selected = ContactMethodPicker.Items[ContactMethodPicker.SelectedIndex];

            var name = ContactMethodPicker.Items[ContactMethodPicker.SelectedIndex];

            var contactMethod = _contactMethods.Single(_ => _.Name == name);

            DisplayAlert("Contact Method", name, "OK");
        }

        void Handle_DateSelected(object sender, Xamarin.Forms.DateChangedEventArgs e)
        {

        }
    }

    public class ContactMethod
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
