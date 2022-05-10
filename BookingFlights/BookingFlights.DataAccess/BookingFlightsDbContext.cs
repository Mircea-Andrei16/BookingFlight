using BookingFlights.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingFlights.DataAccess
{
    public class BookingFlightsDbContext: DbContext
    {
        public BookingFlightsDbContext(DbContextOptions<BookingFlightsDbContext> options): base(options)
        { }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Passenger> Passengers { get; set; }
        
        public DbSet<Seat> Seats { get; set; }

        public DbSet<Ticket> Tickets { get; set; }
    }
}
