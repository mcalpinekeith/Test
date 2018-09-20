using System;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Test.Foursquare;
using Test.Maps.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Test.Maps
{
    //https://www.c-sharpcorner.com/blogs/xamarin-forms-map-renderer-for-ios-and-android
    public partial class MapPage : ContentPage
    {
        private IGeolocator _locator;

        public MapPage()
        {
            _viewModel = new VenueCollectionViewModel();

            InitializeComponent();

            _locator = CrossGeolocator.Current;

            _locator.PositionChanged += Locator_PositionChanged;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //RouteCoordinates = "{Binding RouteCoordinates}"
            //CustomPins = "{Binding CustomPins}"

            if (!_locator.IsListening)
            {
                await _locator.StartListeningAsync(new TimeSpan(), 100); // User must move at least 100 meters before event fires
            }

            //var position = await _locator.GetPositionAsync();

            //await MoveMapToRegion(position);

            //http://maps.googleapis.com/maps/api/directions/json?origin=34.7304,-86.5861&destination=1314+College+Ave+Davenport+IA&key=AIzaSyAVC3Izu_adI7wYdmhEBDyqvK1A8r-iFDQ
        }

        private async void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            await MoveMapToRegion(e.Position);
        }

        private async Task MoveMapToRegion(Plugin.Geolocator.Abstractions.Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var distance = Distance.FromMiles(1.0);
            var mapSpan = MapSpan.FromCenterAndRadius(center, distance);

            var pin = new Pin
            {
                Type = PinType.SavedPin,
                Position = center,
                Label = "Home"
            };

            map.Pins.Clear();
            map.Pins.Add(pin);

            map.MoveToRegion(mapSpan);

            var venues = await FoursquareService.GetVenues(position.Latitude, position.Longitude);

            _viewModel.SetVenues(venues);
        }

        private async void HuntsvilleButton_Clicked(object sender, System.EventArgs e)
        {
            //34.7304° N, 86.5861° W
            await MoveMapToRegion(new Plugin.Geolocator.Abstractions.Position(34.7304, -86.5861));
        }

        private VenueCollectionViewModel _viewModel
        {
            get => BindingContext as VenueCollectionViewModel;
            set => BindingContext = value;
        }

        void OnVenueSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _viewModel.SelectVenueCommand.Execute(e.SelectedItem);
        }
    }
}
