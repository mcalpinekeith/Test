using System;
using System.Collections.Generic;
using Test.Models;
using Xamarin.Forms;

namespace Test.MasterDetail
{
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
