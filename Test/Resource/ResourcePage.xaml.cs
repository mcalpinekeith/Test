using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Resource
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResourcePage : ContentPage
    {
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Resources["buttonBackgroundColor"] = Color.Pink;
        }

        public ResourcePage()
        {
            InitializeComponent();
        }
    }
}
