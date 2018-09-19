using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace Test.Maps
{
    public class RouteCoordinate
    {
        public string Submitter { get; set; }

        public string Color { get; set; }

        public List<PositionList> PositionEstimates { get; set; }
    }

    public class PositionList
    {
        public PositionList()
        {
            ActualPositions = new List<Position>();
        }

        public List<Position> ActualPositions { get; set; }

    }
}