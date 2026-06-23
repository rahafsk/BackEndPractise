using System;
using System.Collections.Generic;
using System.Text;

namespace FlighManagementSystem.models
{
    public class Flight
    {
        public int flightId { get; set; }
        public int pilotId { get; set; }
        public int aircraftId { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public string departureDate { get; set; }
        public string departureTime { get; set; }
        public decimal ticketPrice { get; set; }
        public int totalSeats { get; set; }
        public int availableSeats { get; set; }
        public string status { get; set; }
        public string flightCode { get; set; }
    }
}
