using System;
using System.Collections.Generic;
using Test.Models;
using Xamarin.Forms;

namespace Test.MasterDetail
{
    public partial class MasterPage : MasterDetailPage
    {
        public MasterPage()
        {
            InitializeComponent();

            ContactListView.ItemsSource = new List<Contact>
            {
                new Contact { Name = "Mosh", Status = "Let's talk..", ImageUrl = "http://lorempixel.com/100/100/people/1" },
                new Contact { Name = "John", ImageUrl = "http://lorempixel.com/100/100/people/1" },
                new Contact { Name = "Bob", ImageUrl = "http://lorempixel.com/100/100/people/1" }
            };
        }

        void Handle_ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            //if (e.SelectedItem == null) return;

            var contact = e.SelectedItem as Contact;

            Detail = new NavigationPage(new DetailPage(contact));
            IsPresented = false; //Is master presented?

            //await Navigation.PushAsync(new DetailPage(contact));
            //ContactListView.SelectedItem = null;
        }
    }
}
