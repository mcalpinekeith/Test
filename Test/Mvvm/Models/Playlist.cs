using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Mvvm.Models
{
    public class Playlist
    {
        public string Title { get; set; }

        public bool IsFavorite { get; set; }

        //TextColor="{Binding Color}"
        //public Xamarin.Forms.Color Color { get; set; } = new Xamarin.Forms.Color(0,0,0);
    }
}
