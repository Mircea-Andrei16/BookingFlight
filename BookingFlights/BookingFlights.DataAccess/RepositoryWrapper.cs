﻿using BookingFlights.Abstractions.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingFlights.DataAccess
{
  public  class RepositoryWrapper: IRepositoryWrapper
    {
        private readonly BookingFlightsDbContext _bookingFLightsDbContext;
        private IFlightsRepository? _flightsRepository;
        private IPassengersRepository? _passengersRepository;
        private ISeatsRepository? _seatsRepository;
        private ITicketRepository? _ticketRepository;
        private IBookingRepository? _bookingRepository;

        public IFlightsRepository FlightsRepository
        {
            get
            {
                if (_flightsRepository == null)
                {
                    _flightsRepository = new FlightRepository(_bookingFLightsDbContext);
                }

                return _flightsRepository;
            }
        }

        public IPassengersRepository PassengersRepository
        {
            get
            {
                if (_passengersRepository == null)
                {
                    _passengersRepository = new PassengerRepository(_bookingFLightsDbContext);
                }

                return _passengersRepository;
            }
        }

        public ISeatsRepository SeatsRepository
        {
            get
            {
                if (_seatsRepository == null)
                {
                    _seatsRepository = new SeatRepository(_bookingFLightsDbContext);
                }

                return _seatsRepository;
            }
        }

        public ITicketRepository TicketRepository
        {
            get
            {
                if (_ticketRepository == null)
                {
                    _ticketRepository = new TicketRepository(_bookingFLightsDbContext);
                }

                return _ticketRepository;
            }
        }
        public IBookingRepository BookingRepository
        {
            get
            {
                if (_bookingRepository == null)
                {
                    _bookingRepository = new BookingRepository(_bookingFLightsDbContext);
                }

                return _bookingRepository;
            }
        }

        public RepositoryWrapper(BookingFlightsDbContext bookingFlightsDbContext)
        {
            _bookingFLightsDbContext = bookingFlightsDbContext;
        }

        public void Save()
        {
            _bookingFLightsDbContext.SaveChanges();
        }
    }
}
