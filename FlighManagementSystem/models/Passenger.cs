using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManagementSystem.Models

    {
    public class Passenger
    {
            public int passengerId { get; set; }  // set = put value / change value - get = read value / take value
            public string passengerName { get; set; }
            public string passengerPhone { get; set; }
            public string passengerEmail { get; set; }
            public string passportNumber { get; set; }
        }
    }


