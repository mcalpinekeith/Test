using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Data
{
    // use PCL Storage for filesystems

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FileSystemPage : ContentPage
    {
        public FileSystemPage()
        {
            InitializeComponent();
        }
    }
}
