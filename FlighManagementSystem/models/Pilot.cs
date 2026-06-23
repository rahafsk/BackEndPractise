using System;
using System.Collections.Generic;
using System.Text;

namespace Task2_FlightManagementSystem.Models
{
    public class Pilot
    {
        public int pilotId { get; set; }
        public string pilotName { get; set; }
        public string licenseNumber { get; set; }
        public int experienceYears { get; set; }
        public bool isAvailable { get; set; }

    }
}

