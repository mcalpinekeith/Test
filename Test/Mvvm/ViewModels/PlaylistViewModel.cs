using System;
using System.Drawing;

namespace Test.Mvvm.ViewModels
{
    public class PlaylistViewModel : BaseViewModel
    {
        private bool _isFavorite;

        public string Title { get; set; }

        public bool IsFavorite
        {
            get => _isFavorite;
            set
            {
                Set(ref _isFavorite, value);
                OnPropertyChanged(nameof(Color));
            }
        }

        public Color Color
        {
            get
            {
                return IsFavorite ? Color.Pink : Color.Black;
            }
        }
    }
}
