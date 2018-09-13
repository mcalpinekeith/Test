using System;
using System.Collections.Generic;
using Test.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Messaging
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
            var detailPage = new DetailPage(contact);

            //detailPage.SliderValueChanged += DetailPage_SliderValueChanged;
            MessagingCenter.Subscribe<DetailPage, int>(this, Events.SliderValueChanged, DetailPage_SliderValueChanged);

            //MessagingCenter.Unsubscribe<DetailPage>(this, "SliderValueChanged");

            Detail = new NavigationPage(detailPage);
            IsPresented = false; //Is master presented?

            //await Navigation.PushAsync(new DetailPage(contact));
            //ContactListView.SelectedItem = null;
        }

        void DetailPage_SliderValueChanged(DetailPage sender, int e)
        {
            SliderLabel.Text = "Slider value: " + e;
        }

    }
}
