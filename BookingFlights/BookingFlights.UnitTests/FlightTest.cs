using BookingFlights.Abstractions.Repository;
using BookingFlights.AppLogic.Services;
using BookingFlights.DataAccess;
using BookingFlights.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingFlights.UnitTests
{
    [TestClass]
    public class FlightTest
    {
        //    //connection string
        //    private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=BookingFlightDb;Trusted_Connection=True";

        //    //function for intialization of the service
        //    private BookingService init2()
        //    {
        //        BookingFlightsDbContext dbContext = new BookingFlightsDbContext(new DbContextOptionsBuilder<BookingFlightsDbContext>()
        //            .UseSqlServer(ConnectionString)
        //            .Options);
        //        IRepositoryWrapper repositoryWrapper = new RepositoryWrapper(dbContext);
        //        return new BookingService(repositoryWrapper);
        //    }

        //    private FlightService init()
        //    {
        //        BookingFlightsDbContext dbContext = new BookingFlightsDbContext(new DbContextOptionsBuilder<BookingFlightsDbContext>()
        //            .UseSqlServer(ConnectionString)
        //            .Options);
        //        IRepositoryWrapper repositoryWrapper = new RepositoryWrapper(dbContext);
        //        return new FlightService(repositoryWrapper);
        //    }

        //    [TestMethod]
        //    public void GetAllFlightsCreated()
        //    {
        //        //Arange
        //        FlightService _flightService = init();

        //        //Act
        //        var flightList = _flightService.GetAllQueryable().ToList();

        //        //Assert
        //        Assert.AreEqual(3, flightList.Count);
        //    }

        //    [TestMethod]
        //    public void SearchFlight_Works()
        //    {
        //        //Arange
        //        FlightService _flightService = init();

        //        string departureCity = "Barcelona";
        //        string arrivalCity = "Viena";
        //        DateTime departureDate = new DateTime(2022, 05, 26, 14, 07, 00);

        //        //Act
        //        var searchflight = _flightService.SearchFlight(departureCity, arrivalCity, departureDate);

        //        foreach(Flight flight in searchflight)
        //        {
        //            Assert.AreEqual("ASASD", flight.Name);
        //        }
        //    }

        //   // [TestMethod]
        //    public void deleteBookingFlight()
        //    {
        //        FlightService flightService = init();
        //        BookingService bookingService = init2();

        //        string departureCity = "Barcelona";
        //        string arrivalCity = "Viena";
        //        DateTime departureDate = new DateTime(2022, 05, 26, 14, 07, 00);

        //        //Act
        //        var searchflight = flightService.SearchFlight(departureCity, arrivalCity, departureDate);

        //        foreach(Flight flight in searchflight)
        //        {
        //            flightService.DeleteFromEntity(flight);
        //        }

        //        Guid flightId = Guid.Parse("2f5f24e1-5687-4dab-3ec8-08da3968162b");
        //        Booking SearchBook = bookingService.FindUser(flightId, "user@gmail.com");

        //        Assert.IsNull(SearchBook);

        //    }


        //    [TestMethod]
        //    public void UpdateSpecificSeat()
        //    {
        //        FlightService flightService = init();
        //        Guid flightId = Guid.Parse("d8af8f99-9e13-4f04-3fa9-08da3987d535");
        //        DateTime arrivalDate = new DateTime(2022, 05, 26, 14, 07, 0);
        //        DateTime departureDate = new DateTime(2022, 05, 26, 14, 07, 0); ;

        //        Flight flight = new Flight()
        //        {
        //            Id = flightId,
        //            Name = "WX17T3",
        //            ArrivalCity = "Munchen",
        //            DepartureCity = "Craiova",
        //            arrivalDate = arrivalDate,
        //            departureDate = departureDate
        //        };

        //        flightService.UpdateFromEntity(flight);

        //        var newFlight = flightService.SearchFlight("Craiova", "Munchen", departureDate);

        //        foreach(Flight testFlight in newFlight)
        //        {
        //            if (testFlight.Id == flightId)
        //            {
        //                Assert.AreEqual(departureDate, testFlight.departureDate);
        //            }
        //        }

        //    }

        private Mock<IRepositoryWrapper> flightRepoMock = new Mock<IRepositoryWrapper>();

        private Guid existingFlightId = Guid.Parse("42fdf6f2-0bff-4ee6-3ec7-08da3968162b");

        [TestInitialize]
        public void intializeTest()
        {
            var flight = new Flight()
            {
                Id = existingFlightId,
                Name = "AZ231",
                DepartureCity = "Craiova",
                ArrivalCity = "Cluj",
                departureDate = DateTime.Now,
                arrivalDate = DateTime.Now
            };

            flightRepoMock.Setup(flightRepo => flightRepo.FlightsRepository.GetAll()).Returns(new List<Flight> { flight }.AsQueryable());
            
        }

        [TestMethod]
        public void GetAllFlightsCreated()
        {
            var flightService = new FlightService(flightRepoMock.Object);
            //Act
            var flightList = flightService.GetAllQueryable();

            //Assert
            Assert.AreEqual(1, flightList.ToList().Count);
        }

        [TestMethod]
        public void SearchFlight_Works()
        {
            //Arange
            var flightService = new FlightService(flightRepoMock.Object);

            string departureCity = "Craiova";
            string arrivalCity = "Cluj";
            DateTime departureDate = DateTime.Now;

            //Act
            var searchflight = flightService.SearchFlight(departureCity, arrivalCity, departureDate);

            foreach (Flight flight in searchflight)
            {
                Assert.AreEqual("ASASD", flight.Name);
            }
        }

        [TestMethod]
        public void updateEntity()
        {
            var flightService = new FlightService(flightRepoMock.Object);

            Flight flight = new Flight()
            {
                Id = existingFlightId,
                Name = "aaaa",
                DepartureCity = "Craiova",
                ArrivalCity = "Cluj",
                departureDate = DateTime.Now,
                arrivalDate = DateTime.Now
            };

            flightService.UpdateFromEntity(flight);
            flightService.SaveAsync();

            var updateFlight = flightService.SearchFlight("Craiova", "Cluj", DateTime.Now);

            foreach(Flight updatedFlight in updateFlight)
            {
                Assert.AreEqual("aaaa", updatedFlight.Name);
            }
        }
    }
}
