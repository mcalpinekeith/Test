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
