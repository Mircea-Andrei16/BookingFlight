using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingFlights.DataModel
{
    public class Flight: EntityClass
    {
        public Flight()
        {
            this.Passengers = new HashSet<Passenger>();
        }

        public string Name { get; set; }

        public string DepartureCity { get; set; }

        public string ArrivalCity { get; set; }

        public DateTime departureDate { get; set; }

        public DateTime arrivalDate { get; set; }

        public ICollection<Seat> Seats { get; set; } = new List<Seat>(30);

        public ICollection<Passenger> Passengers { get; set; }
    }
}
