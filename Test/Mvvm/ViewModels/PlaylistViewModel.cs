using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Test.Mvvm.Models;

namespace Test.Mvvm.ViewModels
{
    public class PlaylistViewModel
    {
        public ObservableCollection<Playlist> Playlists { get; private set; } = new ObservableCollection<Playlist>();
        public Playlist SelectedPlaylist { get; set; }

        public void AddPlaylist()
        {
            Playlists.Add(new Playlist { Title = "Playlist " + DateTime.Now.Ticks });
        }

        public void SelectPlaylist(Playlist playlist)
        {
            if (playlist == null) return;

            playlist.IsFavorite = !playlist.IsFavorite;

            SelectedPlaylist = null;
        }
    }
}
