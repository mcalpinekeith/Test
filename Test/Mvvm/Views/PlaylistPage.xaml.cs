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
            _viewModel = new PlaylistCollectionViewModel(new PageService());

			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private PlaylistCollectionViewModel _viewModel
        {
            get => BindingContext as PlaylistCollectionViewModel;
            set => BindingContext = value;
        }

        void OnPlaylistSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _viewModel.SelectPlaylistCommand.Execute(e.SelectedItem);
        }
    }
}