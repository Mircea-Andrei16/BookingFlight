using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingFlights.DataAccess;
using BookingFlights.DataModel;

namespace BookingFlights
{
    public class FlightsUserController : Controller
    {
        private readonly BookingFlightsDbContext _context;

        public FlightsUserController(BookingFlightsDbContext context)
        {
            _context = context;
        }

        // GET: specific Flights for a specific date
        public async Task<IActionResult> Index(string departureCity, string arrivalCity, DateTime departureDate)
        {
            var specificFlight = _context.Flights.Where(flight => flight.DepartureCity == departureCity)
                                                  .Where(flight => flight.ArrivalCity == arrivalCity)
                                                   .Where(flight => flight.departureDate.Date == departureDate.Date);
            if(specificFlight == null)
            {
                return NotFound();
            }

            return View(specificFlight);
        }
    }
}
