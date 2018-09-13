using System;
using System.Collections.Generic;
using Test.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Messaging
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        //public event EventHandler<int> SliderValueChanged;

        public DetailPage(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException();

            BindingContext = contact;

            InitializeComponent();
        }

        void Handle_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            var value = (int)Math.Round(e.NewValue);

            //SliderValueChanged?.Invoke(this, value);

            MessagingCenter.Send(this, Events.SliderValueChanged, value);
        }
    }
}
