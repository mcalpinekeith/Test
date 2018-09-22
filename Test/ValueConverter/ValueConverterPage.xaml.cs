using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Test.ValueConverter
{
    public partial class ValueConverterPage : ContentPage
    {
        public ValueConverterPage()
        {
            InitializeComponent();

            DateListView.ItemsSource = new List<DateModel>
            {
                new DateModel { Date = DateTime.Today.AddMonths(-3) },
                new DateModel { Date = DateTime.Today.AddMonths(-2) },
                new DateModel { Date = DateTime.Today.AddMonths(-1) },
                new DateModel { Date = DateTime.Today.AddMonths(1) },
                new DateModel { Date = DateTime.Today.AddMonths(2) },
                new DateModel { Date = DateTime.Today.AddMonths(3) },
            };
        }
    }
}
