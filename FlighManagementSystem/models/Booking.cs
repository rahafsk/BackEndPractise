using System;
using System.Collections.Generic;
using System.Text;

namespace FlighManagementSystem.models
{
    internal class Booking
    {
        public int bookingId { get; set; }
        public int passengerId { get; set; }
        public int flightId { get; set; }
        public string bookingDate { get; set; }
        public decimal ticketPrice { get; set; }
        public string status { get; set; }
    }
}
