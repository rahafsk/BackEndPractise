using FlighManagementSystem.models;
using FlighManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Linq;

namespace FlightManagementSystem.Models

{
    public class Program
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

        // ==========================================================
        // 1. Register Passenger
        // ==========================================================
        static void RegisterPassenger()
        {
            Console.Clear();
            Console.WriteLine("===== Register Passenger =====");

            int passengerId = context.Passengers.Count + 1;

            Console.Write("Enter passenger name: "); // set
            string name = Console.ReadLine(); // get

            Console.Write("Enter passenger phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter passenger email: ");
            string email = Console.ReadLine();

            Console.Write("Enter passport number: ");
            string passport = Console.ReadLine();

            Passenger passenger = new Passenger
            {
                passengerId = passengerId,
                passengerName = name,
                passengerPhone = phone,
                passengerEmail = email,
                passportNumber = passport
            };


            context.Passengers.Add(passenger);

            Console.WriteLine("Passenger registered successfully.");
        }
        // ==========================================================
        // 2. Add Pilot
        // ==========================================================
        static void AddPilot()
        {
                Console.Clear();
                Console.WriteLine("===== Add Pilot =====");

                int pilotId = context.Pilots.Count + 1; // This creates a new ID for the pilot. - context.Pilots.Count means how many pilots are already saved in the list.

                Console.Write("Enter pilot name: ");
                string name = Console.ReadLine();

                Console.Write("Enter license number: ");
                string license = Console.ReadLine();

                Console.Write("Enter phone number: ");
                string phone = Console.ReadLine();

                
                Pilot pilot = new Pilot  // This creates a new object from the Pilot class.
                {
                    pilotId = pilotId,
                    pilotName = name,
                    pilotPhone= phone,
                    licenseNumber = license,
                    
                    isAvailable = true
                };

                context.Pilots.Add(pilot); // This adds the new pilot object into the Pilots list.

                Console.WriteLine("Pilot added successfully.");

        }

        // ==========================================================
        // 3. Register Pilot
        // ==========================================================
        public static void RegisterPilot()
        {
            Console.Clear();
            Console.WriteLine("===== Register Pilot =====");

            int pilotId = context.Pilots.Count + 1;  // This generates a new pilot ID automatically.

            Console.Write("Enter pilot name: ");
            string name = Console.ReadLine();

            Console.Write("Enter pilot phone: ");
            string phone = Console.ReadLine();

            Console.Write("Enter license number: ");
            string license = Console.ReadLine();

            bool licenseExists = context.Pilots.Any(p => p.licenseNumber == license); // This line checks if the license number already exists.
            /*
             * Any() means:Is there any pilot that matches this condition? 
             * need to check if the license number is already used by another pilot.
             * true or false!!
             */
            if (licenseExists)
            {
                Console.WriteLine("This license number already exists.");
                return;
            }

            Console.Write("Enter total flight hours: ");
            int flightHours;

            if (!int.TryParse(Console.ReadLine(), out flightHours))
            /* 
             * Try to convert the user input into an int.
             * If it works, save the converted number inside flightHours.
             * Because TryParse needs to return two things:
             * 1. true or false: did conversion work?
             * 2. the converted number
             */
            {   
                Console.WriteLine("Invalid flight hours.");
                return;
            }

            if (flightHours < 0)  // This checks that flight hours are not negative. (-5 is not accepted, 0 is accepted, 100 is accepted)
{
                Console.WriteLine("Flight hours cannot be negative.");
                return;
            }

            Pilot pilot = new Pilot
            {
                pilotId = pilotId,
                pilotName = name,
                pilotPhone = phone,
                licenseNumber = license,
                FlightHours = flightHours,
                isAvailable = true
            };

            context.Pilots.Add(pilot);

            Console.WriteLine("Pilot registered successfully.");
            Console.WriteLine("Pilot ID: " + pilot.pilotId);
        }

        // ==========================================================
        // 4. View all Flights
        // ==========================================================
        public static void ViewAllFlights()
        {
            Console.Clear();
            Console.WriteLine("===== View All Flights =====");

            if (!context.Flights.Any()) 
            {
                Console.WriteLine("No flights found.");
                return;
            }

            foreach (Flight flight in context.Flights) // This loop goes through every flight inside the Flights list.
            {
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Flight Code: " + flight.flightCode);
                Console.WriteLine("Origin: " + flight.origin);
                Console.WriteLine("Destination: " + flight.destination);
                Console.WriteLine("Departure Date: " + flight.departureDate);
                Console.WriteLine("Departure Time: " + flight.departureTime);
                Console.WriteLine("Available Seats: " + flight.availableSeats);
                Console.WriteLine("Ticket Price: " + flight.ticketPrice);
                Console.WriteLine("Status: " + flight.status);
            }
        }

        // ==========================================================
        // 5. View Passengers
        // ==========================================================

        public static void ScheduleFlight()
        {
            Console.Clear();
            Console.WriteLine("===== Schedule Flight =====");

            var operationalAircrafts = context.Aircrafts
                .Where(a => a.isOperational == true)
                .ToList();
            /*
             * This gets all aircrafts that are operational.
             * context.Aircrafts is the list of all aircrafts.
             * .Where(a => a.isOperational == true) means: Take only aircrafts where isOperational is true.
             * .ToList() converts the result into a list.
             */

            if (!operationalAircrafts.Any())
            {
                Console.WriteLine("No operational aircraft available.");
                return;
            }

            var availablePilots = context.Pilots
                .Where(p => p.isAvailable == true)
                .ToList();
            /*
             * This gets all pilots who are available.
             * context.Pilots is the list of all pilots.
             * .Where(p => p.isAvailable == true) means: Take only pilots where isAvailable is true.
             */

            if (!availablePilots.Any())
            {
                Console.WriteLine("No available pilots found.");
                return;
            }

            // This prints all operational aircrafts for the user to choose from.
            Console.WriteLine("\nOperational Aircrafts:");
            foreach (Aircraft aircraft in operationalAircrafts)
            {
                Console.WriteLine("ID: " + aircraft.aircraftId +
                                  " | Model: " + aircraft.Model +
                                  " | Seats: " + aircraft.totalSeats);
            }

            Console.Write("Choose aircraft ID: ");
            int aircraftId;

            if (!int.TryParse(Console.ReadLine(), out aircraftId))
            {
                Console.WriteLine("Invalid aircraft ID.");
                return;
            }

            Aircraft selectedAircraft = operationalAircrafts // This searches for the aircraft with the ID entered by the user.
                .FirstOrDefault(a => a.aircraftId == aircraftId); // FirstOrDefault() means: Find the first aircraft that matches. If nothing is found, return null

            if (selectedAircraft == null)
            {
                Console.WriteLine("Aircraft not found or not operational.");
                return;
            }

            Console.WriteLine("\nAvailable Pilots:");
            foreach (Pilot pilot in availablePilots)
            {
                Console.WriteLine("ID: " + pilot.pilotId +
                                  " | Name: " + pilot.pilotName +
                                  " | Flight Hours: " + pilot.FlightHours);
            }

            Console.Write("Choose pilot ID: ");
            int pilotId;

            if (!int.TryParse(Console.ReadLine(), out pilotId))
            {
                Console.WriteLine("Invalid pilot ID.");
                return;
            }

            Pilot selectedPilot = availablePilots
                .FirstOrDefault(p => p.pilotId == pilotId);

            if (selectedPilot == null)
            {
                Console.WriteLine("Pilot not found or not available.");
                return;
            }

            Console.Write("Enter origin: ");
            string origin = Console.ReadLine();

            Console.Write("Enter destination: ");
            string destination = Console.ReadLine();

            Console.Write("Enter departure date: ");
            string date = Console.ReadLine();

            Console.Write("Enter departure time: ");
            string time = Console.ReadLine();

            Console.Write("Enter ticket price: ");
            decimal price; // decimal is used for money.

            if (!decimal.TryParse(Console.ReadLine(), out price))
            {
                Console.WriteLine("Invalid ticket price.");
                return;
            }

            if (price <= 0)
            {
                Console.WriteLine("Ticket price must be greater than 0.");
                return;
            }

            int flightId = context.Flights.Count + 1;

            //This creates a new flight and fills its data.
            Flight flight = new Flight
            {
                flightId = flightId,
                flightCode = "FMS-" + flightId.ToString("000"), // This generates a flight code automatically.
                aircraftId = selectedAircraft.aircraftId, // This connects the flight to the selected aircraft.
                pilotId = selectedPilot.pilotId,
                origin = origin,
                destination = destination,
                departureDate = date,
                departureTime = time,
                ticketPrice = price,
                totalSeats = selectedAircraft.totalSeats,
                availableSeats = selectedAircraft.totalSeats,
                status = "Scheduled"
            };

            context.Flights.Add(flight); //This adds the new flight to the system.

            selectedPilot.isAvailable = false; // After assigning the pilot to a flight, the pilot becomes unavailable.

            Console.WriteLine("Flight scheduled successfully.");
            Console.WriteLine("Flight Code: " + flight.flightCode);
        }

        // ==========================================================
        // 6. Book Flight
        // ==========================================================
        public static void BookFlight()
        {
            Console.Clear();
            Console.WriteLine("===== Book Flight =====");

            if (!context.Passengers.Any())
            {
                Console.WriteLine("No passengers found. Register passenger first.");
                return;
            }

            if (!context.Flights.Any(f => f.status == "Scheduled" && f.availableSeats > 0))
            {
                Console.WriteLine("No scheduled flights with available seats.");
                return;
            }

            Console.WriteLine("\nPassengers:");
            foreach (Passenger passenger in context.Passengers)
            {
                Console.WriteLine("ID: " + passenger.passengerId +
                                  " | Name: " + passenger.passengerName +
                                  " | Passport: " + passenger.passportNumber);
            }

            Console.Write("Enter passenger ID: ");
            int passengerId;

            if (!int.TryParse(Console.ReadLine(), out passengerId))
            {
                Console.WriteLine("Invalid passenger ID.");
                return;
            }

            Passenger selectedPassenger = context.Passengers
                .FirstOrDefault(p => p.passengerId == passengerId);

            if (selectedPassenger == null)
            {
                Console.WriteLine("Passenger not found.");
                return;
            }

            Console.Write("Enter destination: ");
            string destination = Console.ReadLine();

            var availableFlights = context.Flights
                .Where(f => f.status == "Scheduled"
                         && f.destination.ToLower() == destination.ToLower()
                         && f.availableSeats > 0)
                .ToList();

            if (!availableFlights.Any())
            {
                Console.WriteLine("No scheduled flights found for this destination.");
                return;
            }

            Console.WriteLine("\nAvailable Flights:");
            foreach (Flight flight in availableFlights)
            {
                Console.WriteLine("ID: " + flight.flightId +
                                  " | Code: " + flight.flightCode +
                                  " | From: " + flight.origin +
                                  " | To: " + flight.destination +
                                  " | Date: " + flight.departureDate +
                                  " | Seats: " + flight.availableSeats +
                                  " | Price: " + flight.ticketPrice);
            }

            Console.Write("Choose flight ID: ");
            int flightId;

            if (!int.TryParse(Console.ReadLine(), out flightId))
            {
                Console.WriteLine("Invalid flight ID.");
                return;
            }

            Flight selectedFlight = availableFlights
                .FirstOrDefault(f => f.flightId == flightId);

            if (selectedFlight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            bool alreadyBooked = context.Bookings.Any(b =>
                b.passengerId == passengerId &&
                b.flightId == flightId &&
                b.status == "Confirmed");

            if (alreadyBooked)
            {
                Console.WriteLine("This passenger already booked this flight.");
                return;
            }

            int bookingId = context.Bookings.Count + 1;
            string seatNumber = (selectedFlight.totalSeats - selectedFlight.availableSeats + 1) + "A";

            /* 
             * or i can do 
             * string seatNumber = GenerateSeatNumber(selectedFlight); 
             * but i need to do a fungtion up for the GenerateSeatNumber
             * 
             * static string GenerateSeatNumber(Flight flight)
             * {
             * int seatIndex = flight.totalSeats - flight.availableSeats + 1;
             * return seatIndex + "A";
             * }
             */

            Booking booking = new Booking
            {
                bookingId = bookingId,
                passengerId = selectedPassenger.passengerId,
                flightId = selectedFlight.flightId,
                seatNumber = seatNumber,
                bookingDate = DateTime.Now.ToString("yyyy-MM-dd"),
                TotalPrice = selectedFlight.ticketPrice,
                status = "Confirmed"
            };

            context.Bookings.Add(booking);

            selectedFlight.availableSeats--;

            Console.WriteLine("Booking created successfully.");
            Console.WriteLine("Booking ID: " + booking.bookingId);
            Console.WriteLine("Seat Number: " + booking.seatNumber);
            Console.WriteLine("Total Price: " + booking.TotalPrice);
        }



        // ==========================================================
        // Main Menu
        // ==========================================================
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();

                Console.WriteLine("====================================");
                Console.WriteLine("     Flight Management System");
                Console.WriteLine("====================================");
                Console.WriteLine("1. Register Passenger");
                Console.WriteLine("2. Add Pilot");
                Console.WriteLine("3. Add Aircraft");
                Console.WriteLine("4. Create Flight");
                Console.WriteLine("5. View Passengers");
                Console.WriteLine("6. View Pilots");
                Console.WriteLine("7. View Aircrafts");
                Console.WriteLine("8. View Flights");
                Console.WriteLine("9. Book Flight");
                Console.WriteLine("10. Cancel Booking");
                Console.WriteLine("11. Cancel Flight");
                Console.WriteLine("12. Flight Revenue Report");
                Console.WriteLine("0. Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterPassenger();
                        break;

                    case "2":
                        AddPilot();
                        break;

                    case "3":
                        RegisterPilot();
                        break;

                    case "4":
                        ViewAllFlights();
                        break;

                    case "5":
                        ScheduleFlight();
                        break;

                    case "6":
                        //ViewPilots();
                        break;

                    case "7":
                        //ViewAircrafts();
                        break;

                    case "8":
                        //ViewFlights();
                        break;

                    case "9":
                        //BookFlight();
                        break;

                    case "10":
                        //CancelBooking();
                        break;

                    case "11":
                        //CancelFlight();
                        break;

                    case "12":
                        //FlightRevenueReport();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Goodbye.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }
   
    }
}
