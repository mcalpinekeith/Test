using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using Test.Mvvm.ViewModels;

namespace Test.Mvvm.Models
{
    public class Playlist
    {
        public string Title { get; set; }

        public bool IsFavorite { get; set; }
    }
}
