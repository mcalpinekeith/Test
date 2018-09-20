using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Test
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        List<string> _quotes = new List<string>();

        int _quoteIndex = 0;

        public MainPage()
        {
            InitializeComponent();

            _quotes.Add("Together we can change the world, just one random act of kindness at a time.");
            _quotes.Add("My style is unique and random. But I think it's important that it still makes sense.");
            _quotes.Add("The past cannot be changed.The future is yet in your power.");

            //switch(Device.RuntimePlatform)
            //{
            //    case "iOS":
            //        Padding = new Thickness(0, 20, 0, 0);
            //        break;
            //}

            var bannerImageSource = new UriImageSource 
            { 
                Uri = new Uri("http://lorempixel.com/1920/1080/sports/7/"),
                CachingEnabled = false
                //CachingEnabled = true,
                //CacheValidity = new TimeSpan(2, 0, 0, 0) // 2 days
            };

            BannerImage.Source = bannerImageSource;

            //icon@2x = 50px by 50px
            //icon@3x = 75px by 75px

            //SmileImage.Source = ImageSource.FromResource("Test.Resources.Images.smile.jpeg");

            //path = Device.OnPlatForm(
            //  iOS: "clock.png",
            //  Android: "clock.png"
            //);
            //btn.Image = (FileImageSource) ImageSource.FromFile(path);
        }

        private void NextButton_Clicked(object sender, System.EventArgs e)
        {
            _quoteIndex++;

            if (_quoteIndex >= _quotes.Count) _quoteIndex = 0;

            QuoteLabel.Text = _quotes[_quoteIndex];
        }

        private void FontSlider_ValueChanged(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            var newStep = Math.Round(e.NewValue / 1.0);

            FontSlider.Value = newStep * 1.0;
        }
    }
}
