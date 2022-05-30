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
    public class SeatTest
    {

        //private const string ConnectionString = @"Server=(localdb)\mssqllocaldb;Database=BookingFlightDb;Trusted_Connection=True";

        ////function for intialization of the service
        //private SeatService init()
        //{
        //    BookingFlightsDbContext dbContext = new BookingFlightsDbContext(new DbContextOptionsBuilder<BookingFlightsDbContext>()
        //        .UseSqlServer(ConnectionString)
        //        .Options);
        //    IRepositoryWrapper repositoryWrapper = new RepositoryWrapper(dbContext);
        //    return new SeatService(repositoryWrapper);
        //}


        private Mock<IRepositoryWrapper> seatRepoMock = new Mock<IRepositoryWrapper>();

        private Guid existingSeatId = Guid.Parse("ca8ef5a9-79a8-4a8a-57bc-08da3984ad00");
        private Guid existingFlightId = Guid.Parse("bdc9b0c2-247d-4701-ead9-08da3984acee");
       

        [TestInitialize]
        public void intializeTest()
        {
            var seat = new Seat()
            {
                Id = existingSeatId,
                FlightId = existingFlightId,
               
               
            };

            seatRepoMock.Setup(seatRepo => seatRepo.SeatsRepository.GetAll()).Returns(new List<Seat> { seat }.AsQueryable());
        }

        [TestMethod]
        public void GetAllSeatsCreated()
        {
            var seatService = new SeatService(seatRepoMock.Object);
            //Act
            var seatList = seatService.GetAllQueryable();

            //Assert
            Assert.AreEqual(1, seatList.ToList().Count);
        }


        [TestMethod]
        public void FindSpecificSeatForFlight()
        {
            //Arrange
            Guid FlightId = Guid.Parse("bdc9b0c2-247d-4701-ead9-08da3984acee");
            

            var seatService = new SeatService(seatRepoMock.Object);

            //Act
            var findSeat = seatService.GetByCondition(seat => seat.FlightId == FlightId);

            foreach (Seat seat in findSeat)
            {
                Assert.AreEqual(existingSeatId, seat.Id);
            }
        }


        [TestMethod]
        public void updateEntity()
        {
            var seatService = new SeatService(seatRepoMock.Object);

            Seat seat = new Seat()
            {
                Id = existingSeatId,
                FlightId = existingFlightId,
            };

            seatService.UpdateFromEntity(seat);
            seatService.SaveAsync();

            var updateSeat = seatService.GetByCondition(seat => seat.Id == existingSeatId);

            foreach (Seat updatedSeat in updateSeat)
            {
                Assert.AreEqual(existingSeatId, seat.Id);
            }

        }


        //[TestMethod]
        //public void UpdateSpecificSeat()
        //{
        //    SeatService seatService = init();

        //    Guid seatGuid = Guid.Parse("668e7f08-260a-4d8f-1fd9-08da3968163c");
        //    Guid flightGuid = Guid.Parse("42fdf6f2-0bff-4ee6-3ec7-08da3968162b");

        //    Seat seat = new Seat()
        //    {
        //        Id = Guid.Parse("668e7f08-260a-4d8f-1fd9-08da3968163c"),
        //        Number = 1,
        //        isAvailable = true,
        //        FlightId = Guid.Parse("42fdf6f2-0bff-4ee6-3ec7-08da3968162b")
        //    };

        //    seatService.UpdateFromEntity(seat);

        //    var specificSeat = seatService.GetByCondition(seat => seat.Id == seatGuid && seat.FlightId == flightGuid);

        //    foreach(Seat updatedSeat in specificSeat)
        //    {
        //        Assert.IsTrue(updatedSeat.isAvailable);
        //    }
        //}
    }
}
