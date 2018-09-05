using System;
using System.Collections.Generic;
using Test.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.MasterDetail
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailPage : ContentPage
    {
        public DetailPage(Contact contact)
        {
            if (contact == null) throw new ArgumentNullException();

            BindingContext = contact;

            InitializeComponent();
        }
    }
}
