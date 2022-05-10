using BookingFlights.Abstractions.Repository;
using BookingFlights.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingFlights.DataAccess
{
    public class FlightRepository : BaseRepository<Flight>, IFlightsRepository
    {
        public FlightRepository(BookingFlightsDbContext dbContext) : base(dbContext)
        {
        }

        public override ICollection<Flight> GetAll()
        {
            return dbContext.Set<Flight>()
                            .Include(flight => flight.Seats)
                            .ToList();
        }
    }
}
