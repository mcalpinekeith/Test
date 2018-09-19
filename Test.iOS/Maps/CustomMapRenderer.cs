using CoreLocation;
using MapKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Test.iOS.Maps;
using Test.Maps;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Test.iOS.Maps
{
    public class CustomMapRenderer : MapRenderer
    {
        private readonly List<MKPolyline> _polylines = new List<MKPolyline>();
        private MKPolylineRenderer _polylineRenderer;
        private MKPolyline _routeOverlay;
        private CustomMap _customMap;
        private MKMapView _mapView;

        public bool IsRegionChange = true;

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (_mapView == null)
            {
                _mapView = Control as MKMapView;
                _mapView.ZoomEnabled = true;
                _mapView.ScrollEnabled = true;
                _customMap = sender as CustomMap;
            }

            if (_mapView != null && IsRegionChange)
            {
                _mapView.RegionChanged += MapView_RegionChanged;

                Redraw();
                IsRegionChange = false;
            }

            if (Element == null || Control == null) return;

            if (e.PropertyName == CustomMap.RouteCoordinatesProperty.PropertyName)
            {
                _customMap = sender as CustomMap;

                if (_mapView == null) return;

                if (_customMap.RouteCoordinates.Count > 0)
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
                _customMap = sender as CustomMap;
                _customMap.Pins.Clear();

                if (_customMap.CustomPins != null)
                {
                    foreach (var customPin in _customMap.CustomPins)
                    {
                        _customMap.Pins.Add(customPin.Pin);
                    }
                }

                PlotPins();
            }
        }

        private void Redraw()
        {
            if (_customMap == null) return;

            if (_customMap.RouteCoordinates != null && _customMap.RouteCoordinates.Count > 0) UpdateRouteCoordinates();

            if (_customMap.CustomPins != null && _customMap.CustomPins.Count > 0) PlotPins();
        }

        private async void MapView_RegionChanged(object sender, MKMapViewChangeEventArgs e)
        {
            if (Element == null) return;

            _mapView.ZoomEnabled = true;
            _mapView.ScrollEnabled = true;
            _mapView.RegionChanged -= MapView_RegionChanged;

            var center = _mapView.Region.Center;

            //This solves the problem of MKMAPVIEW to Reset its view to FirstLocation  
            _customMap.MoveToRegion(new MapSpan(new Position(center.Latitude, center.Longitude), _mapView.Region.Span.LatitudeDelta, _mapView.Region.Span.LongitudeDelta));

            _mapView.RegionChanged += MapView_RegionChanged;

            if (_customMap.OnVisibleRegionChanged != null)
            {
                var halfheightDegrees = _mapView.Region.Span.LatitudeDelta / 2;
                var halfwidthDegrees = _mapView.Region.Span.LongitudeDelta / 2;

                var left = Math.Round(center.Longitude - halfwidthDegrees, 2);
                var right = Math.Round(center.Longitude + halfwidthDegrees, 2);

                var top = Math.Round(center.Latitude + halfheightDegrees, 2);
                var bottom = Math.Round(center.Latitude - halfheightDegrees, 2);

                if (left < -180) left = 180 + (180 + left);
                if (right > 180) right = (right - 180) - 180;

                var topLeft = top.ToString() + "," + left.ToString();
                var bottomRight = bottom.ToString() + "," + right.ToString();

                await _customMap.OnVisibleRegionChanged(bottomRight, topLeft);
            }
        }

        private void PlotPins()
        {
            if (_mapView == null) return;

            _mapView.GetViewForAnnotation = GetViewForAnnotation;
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

            foreach (var customPin in _customMap.CustomPins)
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
            if (_polylineRenderer == null)
            {
                var o = ObjCRuntime.Runtime.GetNSObject(overlay.Handle) as MKPolyline;

                _polylineRenderer = new MKPolylineRenderer(o)
                {
                    FillColor = UIColor.Blue,
                    StrokeColor = UIColor.Blue,
                    LineWidth = 2,
                    Alpha = 0.4f
                };
            }

            return _polylineRenderer;
        }

        private void UpdateRouteCoordinates()
        {
            if (_mapView == null) return;

            RemoveRouteCoordinates();
            _mapView.OverlayRenderer = GetOverlayRenderer;

            foreach (var routeCoordinates in _customMap.RouteCoordinates)
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

                    _routeOverlay = MKPolyline.FromCoordinates(coordinates);

                    var trackColor = GetColor(routeCoordinates.Color);
                    var o = ObjCRuntime.Runtime.GetNSObject(_routeOverlay.Handle) as MKPolyline;

                    _polylineRenderer = new MKPolylineRenderer(o)
                    {
                        StrokeColor = trackColor,
                        LineWidth = 1
                    };

                    _mapView.AddOverlay(_routeOverlay);
                    _polylines.Add(_routeOverlay);
                }
            }
        }

        private void RemoveRouteCoordinates()
        {
            if (_polylines != null && _polylines.Count > 0)
            {
                _mapView.RemoveOverlays(_mapView.Overlays);
                _polylines.Clear();
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
