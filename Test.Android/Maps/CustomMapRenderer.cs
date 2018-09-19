using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Test.Droid.Maps;
using Test.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Test.Droid.Maps
{
    public class CustomMapRenderer : MapRenderer
    {
        private List<Marker> _markers;
        private GoogleMap _googleMap;
        private Polyline _polyline;
        private List<Polyline> _polylines = new List<Polyline>();
        private bool _isReady;

        public CustomMapRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                // Unsubscribe  
            }

            if (e.NewElement != null)
            {
                Control.GetMapAsync(this);
            }
        }

        private async void Map_CameraChange(object sender, GoogleMap.CameraChangeEventArgs e)
        {
            _googleMap = sender as GoogleMap;

            var customMap = Element as CustomMap;

            if (customMap != null && _googleMap != null)
            {
                var visible = _googleMap.Projection.VisibleRegion;
                var center = visible.LatLngBounds.Center;

                if (center != null)
                {
                    App.CurrentLatitude = center.Latitude;
                    App.CurrentLongitude = center.Longitude;
                    App.CurrentZoom = e.Position.Zoom;
                }

                if (customMap.OnVisibleRegionChanged != null)
                {
                    var farLeftLatitude = Math.Round(visible.FarLeft.Latitude, 2).ToString();
                    var farLeftLongitude = Math.Round(visible.FarLeft.Longitude, 2).ToString();

                    var nearRightLatitude = Math.Round(visible.NearRight.Latitude, 2).ToString();
                    var nearRightLongitude = Math.Round(visible.NearRight.Longitude, 2).ToString();

                    var nearRight = nearRightLatitude + "," + nearRightLongitude;
                    var farLeft = farLeftLatitude + "," + farLeftLongitude;

                    await customMap.OnVisibleRegionChanged(nearRight, farLeft);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element == null || Control == null) return;

            if (e.PropertyName == CustomMap.RouteCoordinatesProperty.PropertyName)
            {
                var customMap = Element as CustomMap;

                if (customMap.RouteCoordinates.Count > 0)
                {
                    UpdateRouteCoordinates();
                }
                else
                {
                    RemoveRouteCoordinates();
                }
            }

            if (e.PropertyName == CustomMap.CustomPinsProperty.PropertyName)
            {
                if (_markers != null && _markers.Count > 0) _markers.ForEach(x => x.Remove());

                SetMarkers();
            }
        }

        private void SetMarkers()
        {
            var customMap = Element as CustomMap;

            _markers = new List<Marker>();

            if (customMap.CustomPins == null) return;

            foreach (var customPin in customMap.CustomPins)
            {
                var marker = new MarkerOptions();

                marker.SetPosition(new LatLng(customPin.Pin.Position.Latitude, customPin.Pin.Position.Longitude));
                marker.SetTitle(customPin.Pin.Label);
                marker.SetSnippet(customPin.Pin.Address);

                var resource = typeof(Resource.Drawable).GetField(customPin.Image);

                if (resource != null)
                {
                    var resourceId = (int)resource.GetValue(customPin.Image);
                    marker.SetIcon(BitmapDescriptorFactory.FromResource(resourceId));
                    //_googleMap.SetOnMarkerClickListener(this);
                    _markers.Add(_googleMap.AddMarker(marker));
                }
            }
        }

        //public bool OnMarkerClick(Marker marker)
        //{
        //    SignDetails _markerDetails = new SignDetails();
        //    foreach (var pin in ((CustomMap)this.Element).CustomPins)
        //    {
        //        if (pin.pin.Position.Latitude == marker.Position.Latitude)
        //        {
        //            if (pin.pin.Position.Longitude == marker.Position.Longitude)
        //            {
        //                if (!string.IsNullOrEmpty(pin.pin.Label))
        //                {
        //                    if (pin.pin.Label == marker.Title)
        //                    {
        //                        _markerDetails.SignLat = pin.pin.Position.Latitude.ToString();
        //                        _markerDetails.SignLong = pin.pin.Position.Longitude.ToString();
        //                        _markerDetails.SignImage = pin.pinImage;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //        ((CustomMap)this.Element).PinClicked(_markerDetails);
        //    return true;
        //}

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
        }

        private void UpdateRouteCoordinates()
        {
            var customMap = Element as CustomMap;

            RemoveRouteCoordinates();

            foreach (var routeCoordinates in customMap.RouteCoordinates)
            {
                foreach (var positionEstimate in routeCoordinates.PositionEstimates)
                {
                    if (routeCoordinates.Submitter == null) return;

                    var trackColor = GetColor(routeCoordinates.Color);
                    var polylineOptions = new PolylineOptions();

                    polylineOptions.InvokeColor(trackColor);

                    foreach (var actualPosition in positionEstimate.ActualPositions)
                    {
                        polylineOptions.Add(new LatLng(actualPosition.Latitude, actualPosition.Longitude));
                    }

                    _polyline = _googleMap.AddPolyline(polylineOptions);
                    _polylines.Add(_polyline);
                }
            }
        }

        private void RemoveRouteCoordinates()
        {
            if (_polylines != null && _polylines.Count > 0)
            { 
                foreach (Polyline polyline in _polylines)
                {
                    polyline.Remove();
                }

                _polylines.Clear();
            }
        }

        protected override async void OnMapReady(GoogleMap googleMap)
        {
            if (_isReady) return;

            _isReady = true;
            _googleMap = googleMap;

            var customMap = Element as CustomMap;
            var bounds = googleMap.Projection.VisibleRegion;

            if (App.CurrentLatitude == 0 && App.CurrentLongitude == 0)
            {
                var currentPosition = await customMap.GetCurrentPositionAsync();
                if (currentPosition != null)
                {
                    App.CurrentLatitude = currentPosition.Latitude;
                    App.CurrentLongitude = currentPosition.Longitude;
                    _googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(App.CurrentLatitude, App.CurrentLongitude), App.CurrentZoom));
                }
                else
                {
                    _googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(52.52, 13.40), App.CurrentZoom));
                }
            }
            else
            {
                _googleMap.MoveCamera(CameraUpdateFactory.NewLatLngZoom(new LatLng(App.CurrentLatitude, App.CurrentLongitude), App.CurrentZoom));
            }

            _googleMap.CameraChange += Map_CameraChange;

            await Task.Run(() =>
            {
                if (customMap.RouteCoordinates != null && customMap.RouteCoordinates.Count > 0) UpdateRouteCoordinates();
            });

            await Task.Run(() =>
            {
                if (customMap.CustomPins != null && customMap.CustomPins.Count > 0) SetMarkers();
            });
        }

        private Android.Graphics.Color GetColor(string color)
        {
            switch (color)
            {
                case "1":
                    return Android.Graphics.Color.Rgb(77, 123, 224);

                case "2":
                    return Android.Graphics.Color.Rgb(50, 193, 214);

                case "3":
                    return Android.Graphics.Color.Rgb(163, 178, 78);

                case "4":
                    return Android.Graphics.Color.Rgb(187, 93, 153);

                case "5":
                    return Android.Graphics.Color.Rgb(175, 98, 46);

                default:
                    return Android.Graphics.Color.Orange;
            }
        }
    }
}