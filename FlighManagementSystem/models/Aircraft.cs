using System;
using System.Collections.Generic;
using System.Text;

namespace FlighManagementSystem.Models
{
    public class Aircraft
    {
        public int aircraftId { get; set; }
        public string Model { get; set; }
        public string totalSeats { get; set; }
        public bool isOperational { get; set; }
        
    }

    
}
