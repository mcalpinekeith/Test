using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Mvvm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Mvvm.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlaylistDetailPage : ContentPage
	{
        public PlaylistDetailPage (PlaylistViewModel playlist)
		{
            BindingContext = playlist;

            InitializeComponent ();
        }
	}
}