using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Data
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppPropertiesPage : ContentPage
    {
        private App _app = Application.Current as App;

        public AppPropertiesPage()
        {
            InitializeComponent();

            BindingContext = _app;

            //TitleEntryCell.Text = _app.Title;
            //IsNotificationsEnabledSwitch.On = _app.IsNotificationsEnabled;
        }

        void OnChange(object sender, System.EventArgs e)
        {
            //_app.Title = TitleEntryCell.Text;
            //_app.IsNotificationsEnabled = (bool)IsNotificationsEnabledSwitch.On;

            //Application.Current.SavePropertiesAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            //OnChange
        }
    }
}
