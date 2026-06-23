using FlighManagementSystem.Models;
using FlightManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlighManagementSystem.models
{
    internal class FlightContext
    {
        public List<Passenger> Passengers { get; set; }
        public List<Pilot> Pilots { get; set; }
        public List<Aircraft> Aircrafts { get; set; }
        public List<Flight> Flights { get; set; }
        public List<Booking> Bookings { get; set; }
    }
}
