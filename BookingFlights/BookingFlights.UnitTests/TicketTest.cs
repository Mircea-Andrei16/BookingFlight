using BookingFlights.Abstractions.Repository;
using BookingFlights.AppLogic.Services;
using BookingFlights.DataAccess;
using BookingFlights.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Assert = NUnit.Framework.Assert;

namespace BookingFlights.UnitTests
{
    [TestClass]
    public class TicketTest
    {
        //connection string
        private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=BookingFlightDb;Trusted_Connection=True";

        //function for intialization of the service
        private TicketService init()
        {
            BookingFlightsDbContext dbContext = new BookingFlightsDbContext(new DbContextOptionsBuilder<BookingFlightsDbContext>()
                .UseSqlServer(ConnectionString)
                .Options);
            IRepositoryWrapper repositoryWrapper = new RepositoryWrapper(dbContext);
            return new TicketService(repositoryWrapper);
        }

        private FlightService init2()
        {
            BookingFlightsDbContext dbContext = new BookingFlightsDbContext(new DbContextOptionsBuilder<BookingFlightsDbContext>()
                .UseSqlServer(ConnectionString)
                .Options);
            IRepositoryWrapper repositoryWrapper = new RepositoryWrapper(dbContext);
            return new FlightService(repositoryWrapper);
        }

        [TestMethod]
        public void GetAllTicketsCreated()
        {
            //Arange
            TicketService _ticketService = init();

            //Act
            var ticketList = _ticketService.GetAllQueryable().ToList();

            //Assert
            Assert.AreEqual(9, ticketList.Count);
        }

        [TestMethod]
        public void FindSpecificFlight()
        {
            //Arrange
            TicketService _ticketService = init();
            Guid FlightId = Guid.Parse("42fdf6f2-0bff-4ee6-3ec7-08da3968162b");

            //Act
            var findTicket = _ticketService.findSpecificFlight(FlightId);

            //Assert
           foreach(var ticket in findTicket)
            {
                Assert.IsNotNull(ticket);
            }

            Assert.AreEqual(3, findTicket.Count());
        }

        [TestMethod]
        public void UpdateFromEntity_Works()
        {
            //Arrange
            TicketService ticketService = init();
            Guid TicketId = Guid.Parse("e2158fee-9da7-4b9b-904b-0b01f3dea174");
            Guid FlightId = Guid.Parse("2f5f24e1-5687-4dab-3ec8-08da3968162b");


            Ticket ticket = new Ticket()
            {
                Id = TicketId,
                Type = "Business",
                FlightId = FlightId,
                Price = 305
            };

            ticketService.UpdateFromEntity(ticket);

            var specificTicket = ticketService.GetByCondition(ticket => ticket.Id == TicketId);

            foreach (Ticket testTicket in specificTicket)
            {
                Assert.AreEqual(305, testTicket.Price);
            }
        }

       // [TestMethod]
        public void DeleteTicketsFlight()
        {

            TicketService ticketService = init();
            FlightService flightService = init2();

            string departureCity = "Barcelona";
            string arrivalCity = "Viena";
            DateTime departureDate = new DateTime(2022, 05, 26, 14, 07, 00);

            //Act
            var searchflight = flightService.SearchFlight(departureCity, arrivalCity, departureDate);

            foreach (Flight flight in searchflight)
            {
                flightService.DeleteFromEntity(flight);
            }

            Guid flightId = Guid.Parse("2f5f24e1-5687-4dab-3ec8-08da3968162b");
            var ticketsFlight = ticketService.GetByCondition(ticket => ticket.FlightId == flightId);

            foreach(Ticket tickets in ticketsFlight)
            {
                Assert.IsNull(tickets);
            }
        }

    }
}
