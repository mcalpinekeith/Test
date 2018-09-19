using System;
using Test.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

//https://github.com/xamarin/XamarinComponents for plugins
//Xam.Plugin or Xam.Plugins

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Test
{
    public partial class App : Application
    {
        const string TitleKey = "Title";
        const string IsNotificationsEnabledKey = "IsNotificationsEnabled";

        public string Title
        {
            get
            {
                if (Properties.ContainsKey(TitleKey))
                {
                     return Properties[TitleKey].ToString();
                }

                return string.Empty;
            }

            set
            {
                Properties[TitleKey] = value;
            }
        }

        public bool IsNotificationsEnabled
        {
            get
            {
                if (Properties.ContainsKey(IsNotificationsEnabledKey))
                {
                    return (bool)Properties[IsNotificationsEnabledKey];
                }

                return false;
            }

            set
            {
                Properties[IsNotificationsEnabledKey] = value;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Maps.MapPage());

            Properties["Username"] = "John";
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
