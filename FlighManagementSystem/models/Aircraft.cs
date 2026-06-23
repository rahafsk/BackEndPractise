using System;
using System.Collections.Generic;
using System.Text;

namespace FlighManagementSystem.Models
{
    internal class Aircraft
    {
        public int aircraftId { get; set; }
        public string aircraftModel { get; set; }
        public string aircraftCode { get; set; }
        public int capacity { get; set; }
        public bool isAvailable { get; set; }
    }

    
}
