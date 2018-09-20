using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.Foursquare;
using Xamarin.Forms;

namespace Test.Maps.ViewModels
{
    public class VenueCollectionViewModel : BaseViewModel
    {
        //events
        //private fields
        //public properties
        //public methods
        //private methods

        private VenueViewModel _selectedVenue;

        public ObservableCollection<VenueViewModel> Venues { get; private set; } = new ObservableCollection<VenueViewModel>();

        public ICommand SelectVenueCommand { get; private set; }

        public VenueCollectionViewModel()
        {
            SelectVenueCommand = new Command<VenueViewModel>(vm => SelectVenue(vm));
        }

        public VenueViewModel SelectedVenue
        {
            get => _selectedVenue;
            set
            {
                Set(ref _selectedVenue, value);
            }
        }

        private void SelectVenue(VenueViewModel venue)
        {
            if (venue == null) return;

            SelectedVenue = null;
        }

        public void SetVenues(List<Venue> venues)
        {
            Venues.Clear();

            foreach (var venue in venues)
            {
                Venues.Add(new VenueViewModel
                {
                    Name = venue.name,
                    Distance = venue.location.distance
                });
            }
        }
    }
}
