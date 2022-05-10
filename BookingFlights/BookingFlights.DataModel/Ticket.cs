using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingFlights.DataModel
{
    public class Ticket: EntityClass
    {
        public string Type { get; set; }

        public int Price { get; set; }
    }
}
