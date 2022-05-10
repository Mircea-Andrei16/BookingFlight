using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingFlights.DataModel
{
    public class Passenger: EntityClass
    {
        public Passenger()
        {
            this.Flights = new HashSet<Flight>();  
        }

        public string PassengerName { get; set; }

        public string PassengerSurname { get; set; }

        public string Email { get; set; }

        public string Telephone { get; set; }

        public Seat Seat { get; set; }

        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<Flight> Flights { get; set; } 
    }
}
