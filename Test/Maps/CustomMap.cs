using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Test.Maps
{
    public class CustomMap : Map
    {
        //This bindable property will provide Track information for map   
        //Will be accessible from Xaml view for binding  
        public static readonly BindableProperty RouteCoordinatesProperty =
            BindableProperty.Create("RouteCoordinates", typeof(List<RouteCoordinate>), typeof(CustomMap), null, BindingMode.TwoWay);

        public List<RouteCoordinate> RouteCoordinates
        {
            get { return (List<RouteCoordinate>)GetValue(RouteCoordinatesProperty); }
            set { SetValue(RouteCoordinatesProperty, value); }
        }

        //This bindable property will contain collection of pins  
        //CustomPin Model will have Pin Metadata  
        public static readonly BindableProperty CustomPinsProperty =
            BindableProperty.Create("CustomPins", typeof(List<CustomPin>), typeof(CustomMap), null, BindingMode.TwoWay);

        public List<CustomPin> CustomPins
        {
            get { return (List<CustomPin>)GetValue(CustomPinsProperty); }
            set { SetValue(CustomPinsProperty, value); }
        }

        //public Func<string, string, Task> OnVisibleRegionChanged;
    }
}
