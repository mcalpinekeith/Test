using System;
using Test.Mvvm.Models;
using Test.Mvvm.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test.Mvvm.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlaylistPage : ContentPage
	{
		public PlaylistPage ()
		{
            BindingContext = new PlaylistViewModel();

			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void OnAddPlaylist(object sender, EventArgs e)
        {
            (BindingContext as PlaylistViewModel).AddPlaylist();
        }

        void OnPlaylistSelected(object sender, SelectedItemChangedEventArgs e)
        {
            (BindingContext as PlaylistViewModel).SelectPlaylist(e.SelectedItem as Playlist);
        }
    }
}