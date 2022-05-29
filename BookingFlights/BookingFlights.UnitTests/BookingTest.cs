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
    public class BookingTest
    {
        private Mock<IRepositoryWrapper> bookingRepoMock = new Mock<IRepositoryWrapper>();

        private Guid existingBookingId = Guid.Parse("7299FFCC-435E-4A6D-99DF-57A4D6FBA712");
        private Guid existingFlightId = Guid.Parse("42fdf6f2-0bff-4ee6-3ec7-08da3968162b");
        private Guid existingSeatId = Guid.Parse("d28c138b-6715-4096-1ff9-08da3968163c");
        private Guid existingTicketId = Guid.Parse("e0350e18-62c4-413c-b09e-4eaecc0f46a9");

        [TestInitialize]
        public void intializeTest()
        {
            var booking = new Booking()
            {
                Id = existingBookingId,
                FlightId = existingFlightId,
                SeatId = existingSeatId,
                TicketId = existingTicketId,
                UserName = "user@gmail.com"
            };

            bookingRepoMock.Setup(bookingRepo => bookingRepo.BookingRepository.GetAll()).Returns(new List<Booking> { booking }.AsQueryable());
        }

        [TestMethod]
        public void GetAllBookingsCreated()
        {
            var bookingService = new BookingService(bookingRepoMock.Object);
            //Act
            var bookingList = bookingService.GetAllQueryable();

            //Assert
            Assert.AreEqual(1, bookingList.ToList().Count);
        }

        [TestMethod]
        public void FindBookingBySpecificFlightIdEmail()
        {
            //Arrange
            Guid FlightId = Guid.Parse("42fdf6f2-0bff-4ee6-3ec7-08da3968162b");
            string UserMail = "user@gmail.com";

            var bookingService = new BookingService(bookingRepoMock.Object);

            //Act
            var findBooking = bookingService.GetByCondition(booking => booking.FlightId == FlightId);
           
            foreach (Booking booking in findBooking)
            {
                Assert.AreEqual(existingBookingId, booking.Id);
            }

            
        }

        [TestMethod]
        public void updateEntity()
        {
            var bookingService = new BookingService(bookingRepoMock.Object);

            Booking booking = new Booking()
            {
                Id = existingBookingId,
                FlightId = existingFlightId,
                SeatId = existingSeatId,
                TicketId = existingTicketId,
                UserName = "new@gmail.com"
            };

            bookingService.UpdateFromEntity(booking);
            bookingService.SaveAsync();

            var updateBooking = bookingService.GetByCondition(booking => booking.Id == existingBookingId);

            foreach(Booking updatedBooking in updateBooking)
            {
                Assert.AreEqual("new@gmail.com", updatedBooking.UserName);
            }
            
        }

        [TestMethod]
        public void FindByEmail()
        {
            var booking = new Booking()
            {
                Id = existingBookingId,
                FlightId = existingFlightId,
                SeatId = existingSeatId,
                TicketId = existingTicketId,
                UserName = "user@gmail.com"
            };

            bookingRepoMock.Setup(bookingRepo => bookingRepo.BookingRepository.FindEmail("user@gmail.com")).Returns(booking);

            var bookingService = new BookingService(bookingRepoMock.Object);

            Booking findSpecificBooking = bookingService.FindEmail("user@gmail.com");

            Assert.AreEqual("user@gmail.com",findSpecificBooking.UserName);
        }
        
        [TestMethod]
        public void FindBy()
        {
            var booking = new Booking()
            {
                Id = existingBookingId,
                FlightId = existingFlightId,
                SeatId = existingSeatId,
                TicketId = existingTicketId,
                UserName = "user@gmail.com"
            };

            bookingRepoMock.Setup(bookingRepo => bookingRepo.BookingRepository.FindUser(existingFlightId, "user@gmail.com")).Returns(booking);

            var bookingService = new BookingService(bookingRepoMock.Object);

            Booking findSpecificBooking = bookingService.FindUser(existingFlightId,"user@gmail.com");

            Assert.AreEqual("user@gmail.com", findSpecificBooking.UserName);
            Assert.AreEqual(existingFlightId,findSpecificBooking.FlightId);
        }
    }
}
