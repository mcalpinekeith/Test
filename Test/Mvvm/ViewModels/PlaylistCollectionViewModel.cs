using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.Mvvm.Models;
using Test.Mvvm.Views;
using Xamarin.Forms;

namespace Test.Mvvm.ViewModels
{
    public class PlaylistCollectionViewModel : BaseViewModel
    {
        //events
        //private fields
        //public properties
        //public methods
        //private methods

        private PlaylistViewModel _selectedPlaylist;

        public ObservableCollection<PlaylistViewModel> Playlists { get; private set; } = new ObservableCollection<PlaylistViewModel>();

        public ICommand AddPlaylistCommand { get; private set; }
        public ICommand SelectPlaylistCommand { get; private set; }

        private readonly IPageService _pageService;
        public PlaylistCollectionViewModel(IPageService pageService)
        {
            _pageService = pageService;

            AddPlaylistCommand = new Command(AddPlaylist);
            SelectPlaylistCommand = new Command<PlaylistViewModel>(async vm => await SelectPlaylist(vm));
        }

        public PlaylistViewModel SelectedPlaylist
        {
            get => _selectedPlaylist;
            set
            {
                Set(ref _selectedPlaylist, value);
            }
        }

        private void AddPlaylist()
        {
            Playlists.Add(new PlaylistViewModel { Title = "Playlist " + DateTime.Now.Ticks });
        }

        private async Task SelectPlaylist(PlaylistViewModel playlist)
        {
            if (playlist == null) return;

            playlist.IsFavorite = !playlist.IsFavorite;

            SelectedPlaylist = null;

            await _pageService.PushAsync(new PlaylistDetailPage(playlist));
        }
    }
}
