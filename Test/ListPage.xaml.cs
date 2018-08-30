using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Test.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListPage : ContentPage
    {
        private List<ContactGroup> _contactGroups = new List<ContactGroup>();
        private ObservableCollection<Contact> _contacts = new ObservableCollection<Contact>();

        public ListPage()
        {
            InitializeComponent();

            _contactGroups = new List<ContactGroup>
            {
                new ContactGroup("M", "M")
                {
                    new Contact { Name = "Mosh", Status="Let's talk..", ImageUrl = "http://lorempixel.com/100/100/people/1" }
                },
                new ContactGroup("J", "J")
                {
                    new Contact { Name = "John", ImageUrl = "http://lorempixel.com/100/100/people/1" }
                },
                new ContactGroup("B", "B")
                {
                    new Contact { Name = "Bob", ImageUrl = "http://lorempixel.com/100/100/people/1" }
                }
            };

            _contacts = GetContacts();
            NameListView.ItemsSource = _contacts;

            GroupNameListView.ItemsSource = _contactGroups;
        }

        private void NameListView_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            _contacts = GetContacts(e.NewTextValue);
            NameListView.ItemsSource = _contacts;
        }

        private ObservableCollection<Contact> GetContacts(string searchText = null)
        {
            var contacts = new List<Contact>
            {
                new Contact { Name = "Mosh", Status = "Let's talk..", ImageUrl = "http://lorempixel.com/100/100/people/1" },
                new Contact { Name = "John", ImageUrl = "http://lorempixel.com/100/100/people/1" },
                new Contact { Name = "Bob", ImageUrl = "http://lorempixel.com/100/100/people/1" }
            };

            if (string.IsNullOrWhiteSpace(searchText)) return new ObservableCollection<Contact>(contacts);

            return new ObservableCollection<Contact>(contacts.Where(_ => _.Name.ToLower().Contains(searchText.ToLower())).ToList());
        }

        private void NameListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var contact = e.Item as Contact;

            DisplayAlert("Tapped", contact.Name, "OK");
        }

        private void NameListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            NameListView.SelectedItem = null; // This disables Selected event

            //var contact = e.SelectedItem as Contact;

            //DisplayAlert("Selected", contact.Name, "OK");
        }

        private void Call_Clicked(object sender, System.EventArgs e)
        {
            var contact = (sender as MenuItem).CommandParameter as Contact;

            DisplayAlert("Call", contact.Name, "OK");
        }

        private void Delete_Clicked(object sender, System.EventArgs e)
        {
            var contact = (sender as MenuItem).CommandParameter as Contact;

            DisplayAlert("Delete", contact.Name, "OK");

            _contacts.Remove(contact);
        }

        private void NameListView_Refreshing(object sender, System.EventArgs e)
        {
            _contacts = GetContacts();
            NameListView.ItemsSource = _contacts;
            NameListView.EndRefresh();
        }
    }
}
