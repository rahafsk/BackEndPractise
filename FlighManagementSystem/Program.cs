using FlighManagementSystem.models;
using FlighManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.Models

{
    public class  Program
    {
        // Main storage for the whole flight system
        public static FlightContext context = new FlightContext
        {
            Passengers = new List<Passenger>(),
            Pilots = new List<Pilot>(),
            Aircrafts = new List<Aircraft>(),
            Flights = new List<Flight>(),
            Bookings = new List<Booking>()
        };
    }
}