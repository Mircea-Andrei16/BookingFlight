using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingFlights.DataModel
{
    public class Seat: EntityClass
    { 
        public int Number { get; set; }

        public bool isAvailable { get; set; }


    }
}
