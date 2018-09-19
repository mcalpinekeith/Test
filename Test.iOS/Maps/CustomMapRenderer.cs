using System;
using System.ComponentModel;
using System.Collections.Generic;
using MapKit;
using Test.iOS.Maps;
using Test.Maps;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using System.Threading.Tasks;
using UIKit;
using CoreLocation;
using Plugin.Geolocator;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Test.iOS.Maps
{
    public class CustomMapRenderer : MapRenderer
    {
        private readonly List<MKPolyline> _polyLines = new List<MKPolyline>();
        private MKPolylineRenderer polylineRenderer;
        private MKPolyline routeOverlay;
        private CustomMap customMap;
        private MKMapView mapView;

        public string TopLeft { get; set; }
        public string BottomRight { get; set; }
        public bool IsRegionChange = true;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            customMap = sender as CustomMap;

            if (mapView == null)
            {
                mapView = Control as MKMapView;
                mapView.ZoomEnabled = true;
                mapView.ScrollEnabled = true;
            }

            if (mapView != null && IsRegionChange)
            {
                //mapView.RegionChanged += NativeMap_RegionChanged;

                Redraw();
                IsRegionChange = false;
            }

            if (Element == null || Control == null) return;

            if (e.PropertyName == CustomMap.RouteCoordinatesProperty.PropertyName)
            {
                if (mapView == null) return;

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
                customMap.Pins.Clear();

                if (customMap.CustomPins != null)
                {
                    foreach (var customPin in customMap.CustomPins)
                    {
                        customMap.Pins.Add(customPin.Pin);
                    }
                }

                PlotPins();
            }
        }

        private void RemoveRouteCoordinates()
        {
            if (_polyLines != null && _polyLines.Count > 0)
            {
                mapView.RemoveOverlays(mapView.Overlays);
                _polyLines.Clear();
            }
        }

        private void Redraw()
        {
            if (customMap == null) return;

            if (customMap.RouteCoordinates != null && customMap.RouteCoordinates.Count > 0) UpdateRouteCoordinates();

            if (customMap.CustomPins != null && customMap.CustomPins.Count > 0) PlotPins();
        }

        //private async void NativeMap_RegionChanged(object sender, MKMapViewChangeEventArgs e)
        //{
        //    if (Element == null) return;
        //
        //    CalculateBounds(mapView);
        //
        //    mapView.ZoomEnabled = true;
        //    mapView.ScrollEnabled = true;
        //    mapView.RegionChanged -= NativeMap_RegionChanged;
        //
        //    //This solves the problem of MKMAPVIEW to Reset its view to FirstLocation  
        //    customMap.MoveToRegion(new MapSpan(new Position(mapView.Region.Center.Latitude, mapView.Region.Center.Longitude), mapView.Region.Span.LatitudeDelta, mapView.Region.Span.LongitudeDelta));
        //
        //    mapView.RegionChanged += NativeMap_RegionChanged;
        //
        //    if (BottomRight != null && TopLeft != null && customMap.OnVisibleRegionChanged != null)
        //    {
        //        await customMap.OnVisibleRegionChanged(BottomRight, TopLeft);
        //    }
        //}

        //public void CalculateBounds(MKMapView map)
        //{
        //    var center = map.Region.Center;
        //    var halfheightDegrees = map.Region.Span.LatitudeDelta / 2;
        //    var halfwidthDegrees = map.Region.Span.LongitudeDelta / 2;
        //    var left = Math.Round(center.Longitude - halfwidthDegrees, 2);
        //    var right = Math.Round(center.Longitude + halfwidthDegrees, 2);
        //    var top = Math.Round(center.Latitude + halfheightDegrees, 2);
        //    var bottom = Math.Round(center.Latitude - halfheightDegrees, 2);
        //
        //    if (left < -180) left = 180 + (180 + left);
        //    if (right > 180) right = (right - 180) - 180;
        //
        //    TopLeft = top.ToString() + "," + left.ToString();
        //    BottomRight = bottom.ToString() + "," + right.ToString();
        //}

        private void PlotPins()
        {
            if (mapView == null) return;

            mapView.GetViewForAnnotation = GetViewForAnnotation;
        }

        private static MKAnnotationView GetViewForAnnotation(CustomMapRenderer instance, MKMapView view, IMKAnnotation annotation)
        {
            if (annotation is MKUserLocation) return null;

            if (!(annotation is MKPointAnnotation pointAnnotation)) return null;

            var customPin = instance.GetCustomPin(pointAnnotation);

            if (customPin == null) return null;

            var annotationView = new MKAnnotationView(annotation, customPin.Image)
            {
                //CurrentPin = customPin,
                //PinTouch = customPin.PinClicked
            };

            annotationView.Image = UIImage.FromFile(customPin.Image + ".png");

            return annotationView;
        }

        private CustomPin GetCustomPin(MKPointAnnotation pointAnnotation)
        {
            var position = new Position(pointAnnotation.Coordinate.Latitude, pointAnnotation.Coordinate.Longitude);

            foreach (var customPin in customMap.CustomPins)
            {
                if (customPin.Pin.Position != position) continue;
                if (string.IsNullOrEmpty(customPin.Pin.Label)) continue;
                if (customPin.Pin.Label != pointAnnotation.Title) continue;

                return customPin;
            }

            return null;
        }

        [Foundation.Export("mapView:rendererForOverlay:")]
        private MKPolylineRenderer GetOverlayRenderer(MKMapView view, IMKOverlay overlay)
        {
            if (polylineRenderer == null)
            {
                var o = ObjCRuntime.Runtime.GetNSObject(overlay.Handle) as MKPolyline;

                polylineRenderer = new MKPolylineRenderer(o)
                {
                    FillColor = UIColor.Blue,
                    StrokeColor = UIColor.Blue,
                    LineWidth = 2,
                    Alpha = 0.4f
                };
            }

            return polylineRenderer;
        }

        private void UpdateRouteCoordinates()
        {
            if (mapView == null) return;

            RemoveRouteCoordinates();
            mapView.OverlayRenderer = GetOverlayRenderer;

            foreach (var routeCoordinates in customMap.RouteCoordinates)
            {
                foreach (var positionEstimate in routeCoordinates.PositionEstimates)
                {
                    int index = 0;
                    var coordinates = new CLLocationCoordinate2D[positionEstimate.ActualPositions.Count];

                    foreach (var actualPosition in positionEstimate.ActualPositions)
                    {
                        coordinates[index] = new CLLocationCoordinate2D(actualPosition.Latitude, actualPosition.Longitude);
                        index++;
                    }

                    routeOverlay = MKPolyline.FromCoordinates(coordinates);

                    var trackColor = GetColor(routeCoordinates.Color);
                    var o = ObjCRuntime.Runtime.GetNSObject(routeOverlay.Handle) as MKPolyline;

                    polylineRenderer = new MKPolylineRenderer(o)
                    {
                        StrokeColor = trackColor,
                        LineWidth = 1
                    };

                    mapView.AddOverlay(routeOverlay);
                    _polyLines.Add(routeOverlay);
                }
            }
        }

        private UIColor GetColor(string color)
        {
            switch (color)
            {
                case "1":
                    return UIColor.FromRGB(77, 123, 224);

                case "2":
                    return UIColor.FromRGB(50, 193, 214);

                case "3":
                    return UIColor.FromRGB(163, 178, 78);

                case "4":
                    return UIColor.FromRGB(187, 93, 153);

                case "5":
                    return UIColor.FromRGB(175, 98, 46);
                default:
                    return UIColor.Orange;
            }
        }
    }
}
