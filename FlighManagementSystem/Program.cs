using FlighManagementSystem.models;
using FlighManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Channels;

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
        // 2. Add Aircraft
        // ==========================================================
        static void AddAircraft()
        {
            Console.Clear();
            Console.WriteLine("===== Add Aircraft =====");

            // Generate aircraft ID automatically
            int aircraftId = context.Aircrafts.Count + 1; // context.Pilots.Count means how many pilots are already saved in the list.

            Console.Write("Enter aircraft model: ");
            string model = Console.ReadLine();

            Console.Write("Enter total seats: ");
            int totalSeats;

            // Check if total seats is a valid number
            if (!int.TryParse(Console.ReadLine(), out totalSeats))
            {
                Console.WriteLine("Invalid total seats.");
                return;
            }

            // Total seats must be more than 0
            if (totalSeats <= 0)
            {
                Console.WriteLine("Total seats must be greater than 0.");
                return;
            }

            // Create new Aircraft object
            Aircraft aircraft = new Aircraft
            {
                aircraftId = aircraftId,
                Model = model,
                totalSeats = totalSeats,

                // The aircraft starts as operational
                isOperational = true
            };

            // Add aircraft to the Aircrafts list
            context.Aircrafts.Add(aircraft);

            Console.WriteLine("Aircraft added successfully.");
            Console.WriteLine("Aircraft ID: " + aircraft.aircraftId);
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
        // 5. Schedule Flight
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
             * var - displays the type of the variable automatically. In this case, it will be List<Aircraft>.
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
            foreach (Passenger passenger in context.Passengers) // Go through each passenger inside context.Passengers
            {
                Console.WriteLine("ID: " + passenger.passengerId +
                                  " | Name: " + passenger.passengerName +
                                  " | Passport: " + passenger.passportNumber);
            }
            

            Console.Write("Enter passenger ID: ");
            int passengerId;

            //Validate passenger ID
            if (!int.TryParse(Console.ReadLine(), out passengerId))
            {
                Console.WriteLine("Invalid passenger ID.");
                return;
            }
            //Find the selected passenger
            Passenger selectedPassenger = context.Passengers
                .FirstOrDefault(p => p.passengerId == passengerId);

            // Check if passenger exists
            if (selectedPassenger == null)
            {
                Console.WriteLine("Passenger not found.");
                return;
            }

            // Ask for destination
            Console.Write("Enter destination: ");
            string destination = Console.ReadLine();

            // Find available flights to that destination
            var availableFlights = context.Flights
                .Where(f => f.status == "Scheduled" // .Where(...) filters the list.
                         && f.destination.ToLower() == destination.ToLower()
                         && f.availableSeats > 0)
                .ToList();

            // Check if no flights found
            if (!availableFlights.Any())
            {
                Console.WriteLine("No scheduled flights found for this destination.");
                return;
            }

            // Display available flights
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

            // Ask for flight ID
            Console.Write("Choose flight ID: ");
            int flightId;

            // Validate flight ID
            if (!int.TryParse(Console.ReadLine(), out flightId))
            {
                Console.WriteLine("Invalid flight ID.");
                return;
            }

            // Find the selected flight
            Flight selectedFlight = availableFlights
                .FirstOrDefault(f => f.flightId == flightId);

            // Check if flight exists
            if (selectedFlight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            // Check if passenger already booked this flight
            bool alreadyBooked = context.Bookings.Any(b =>
                b.passengerId == passengerId &&
                b.flightId == flightId &&
                b.status == "Confirmed");

            // If already booked, inform the user
            if (alreadyBooked)
            {
                Console.WriteLine("This passenger already booked this flight.");
                return;
            }

            // Create a new booking
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

            // Create a new booking and fill its data
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

            selectedFlight.availableSeats--; // Decrease the available seats of the flight by 1 after booking.

            Console.WriteLine("Booking created successfully.");
            Console.WriteLine("Booking ID: " + booking.bookingId);
            Console.WriteLine("Seat Number: " + booking.seatNumber);
            Console.WriteLine("Total Price: " + booking.TotalPrice);
        }

        // ==========================================================
        // 7. Cancel Booking
        // ==========================================================
        public static void CancelBooking()
        {
            Console.Clear();
            Console.WriteLine("===== Cancel Booking =====");

            var confirmedBookings = context.Bookings // So confirmedBookings contains only bookings that can be cancelled.
                                                     // .Where(...) filters the list.
                .Where(b => b.status == "Confirmed") // This line gets all bookings that have status "Confirmed".
                .ToList();

            // Check if there are no confirmed bookings
            if (!confirmedBookings.Any())
            {
                Console.WriteLine("No confirmed bookings found.");
                return;
            }

            Console.WriteLine("\nConfirmed Bookings:");
            foreach (Booking booking in confirmedBookings) //This loop goes through every booking inside the confirmedBookings list.
            {
                // This loop goes through every booking inside the confirmedBookings list.
                Flight flight = context.Flights.FirstOrDefault(f => f.flightId == booking.flightId); // Find the flight whose ID is the same as the booking flightId.
                Passenger passenger = context.Passengers.FirstOrDefault(p => p.passengerId == booking.passengerId);
                /*
                 * This line searches inside context.Passengers to find the passenger connected to this booking.
                 * The condition: p.passengerId == booking.passengerId
                 * Find the passenger whose ID is the same as the booking passengerId.
                 */


                // Print booking details
                Console.WriteLine("Booking ID: " + booking.bookingId +
                                  " | Passenger: " + passenger?.passengerName + // If passenger is not null, get passengerName. If passenger is null, do not crash.
                                  " | Flight: " + flight?.flightCode + //If flight exists, show flightCode
                                  " | Seat: " + booking.seatNumber);
                // This is called the null conditional operator.
            }

            // Ask for booking ID to cancel
            Console.Write("Enter booking ID to cancel: ");
            int bookingId;

            // Validate booking ID
            if (!int.TryParse(Console.ReadLine(), out bookingId))
            {
                Console.WriteLine("Invalid booking ID.");
                return;
            }

            // Find the booking to cancel
            Booking selectedBooking = context.Bookings
                .FirstOrDefault(b => b.bookingId == bookingId && b.status == "Confirmed");

            // Check if booking exists
            if (selectedBooking == null)
            {
                Console.WriteLine("Booking not found.");
                return;
            }

            // Find the flight associated with the booking
            Flight selectedFlight = context.Flights
                .FirstOrDefault(f => f.flightId == selectedBooking.flightId);

            // Cancel the booking
            if (selectedFlight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            // Update the booking status to "Cancelled"
            if (selectedFlight.status == "Departed")
            {
                Console.WriteLine("Cannot cancel booking because flight already departed.");
                return;
            }

            // Update the booking status to "Cancelled"
            if (selectedFlight.status == "Cancelled")
            {
                Console.WriteLine("Cannot cancel booking because flight is cancelled.");
                return;
            }

            
            selectedBooking.status = "Cancelled";
            selectedFlight.availableSeats++;

            Console.WriteLine("Booking cancelled successfully.");
            Console.WriteLine("Seat returned to flight.");
        }


        // ==========================================================
        // 8. Depart Flight
        // ==========================================================
        public static void DepartFlight()
        {
            Console.Clear();
            Console.WriteLine("===== Depart Flight =====");

            // Check if no scheduled flights exist
            var scheduledFlights = context.Flights
                .Where(f => f.status == "Scheduled")
                .ToList();

            // Display scheduled flights
            if (!scheduledFlights.Any())
            {
                Console.WriteLine("No scheduled flights found.");
                return;
            }

            // Display scheduled flights
            Console.WriteLine("\nScheduled Flights:");
            foreach (Flight flight in scheduledFlights)
            {
                Console.WriteLine("ID: " + flight.flightId +
                                  " | Code: " + flight.flightCode +
                                  " | From: " + flight.origin +
                                  " | To: " + flight.destination);
            }

            // Ask for flight ID to depart
            Console.Write("Enter flight ID to depart: ");
            int flightId;

            // Validate flight ID
            if (!int.TryParse(Console.ReadLine(), out flightId))
            {
                Console.WriteLine("Invalid flight ID.");
                return;
            }

            // Find the flight to depart
            Flight selectedFlight = scheduledFlights
                .FirstOrDefault(f => f.flightId == flightId);

            // Check if flight exists
            if (selectedFlight == null)
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            // Update flight status to "Departed"
            // This asks the user how many hours the flight took
            Console.Write("Enter flight duration in hours: ");
            int duration;

            if (!int.TryParse(Console.ReadLine(), out duration))
            {
                Console.WriteLine("Invalid duration.");
                return;
            }

            // Update flight status to "Departed"
            if (duration <= 0) // This asks the user how many hours the flight took
            {
                Console.WriteLine("Duration must be greater than 0.");
                return;
            }

            
            selectedFlight.status = "Departed";

            // This line searches inside the Pilots list to find the pilot assigned to this flight.
            Pilot pilot = context.Pilots
                .FirstOrDefault(p => p.pilotId == selectedFlight.pilotId);

            if (pilot != null)
            {
                pilot.FlightHours += duration; //This adds the flight duration to the pilot’s total flight hours.
                pilot.isAvailable = true;
            }

            Console.WriteLine("Flight departed successfully.");
            Console.WriteLine("Pilot flight hours updated.");
        }
        // ==========================================================
        // 9. Cancel Flight
        // ==========================================================
        public static void CancelFlight()
        {
            Console.Clear();
            Console.WriteLine("===== Cancel Flight =====");

            var scheduledFlights = context.Flights
                .Where(f => f.status == "Scheduled")
                .ToList();

            if (!scheduledFlights.Any())
            {
                Console.WriteLine("No scheduled flights found.");
                return;
            }

            Console.WriteLine("\nScheduled Flights:");
            foreach (Flight flight in scheduledFlights)
            {
                Console.WriteLine("ID: " + flight.flightId +
                                  " | Code: " + flight.flightCode +
                                  " | From: " + flight.origin +
                                  " | To: " + flight.destination);
            }

            Console.Write("Enter flight ID to cancel: ");
            int flightId;

            if (!int.TryParse(Console.ReadLine(), out flightId))
            {
                Console.WriteLine("Invalid flight ID.");
                return;
            }
            Flight selectedFlight = scheduledFlights
                .FirstOrDefault(f => f.flightId == flightId); // Find the flight whose flightId is the same as the ID entered by the user.

            if (selectedFlight == null) // If selectedFlight is null, it means the flight was not found.
            {
                Console.WriteLine("Flight not found.");
                return;
            }

            selectedFlight.status = "Cancelled";

            var affectedBookings = context.Bookings
                .Where(b => b.flightId == selectedFlight.flightId && b.status == "Confirmed")
                .ToList();

            foreach (Booking booking in affectedBookings)
            {
                booking.status = "Cancelled";
            }

            selectedFlight.availableSeats = selectedFlight.totalSeats; // This resets the available seats back to the full number of seats.
            /*
             * Because the flight is cancelled, so all seats are logically free again.
             * The confirmed bookings were cancelled, so no seats are occupied anymore.
             */

            Pilot pilot = context.Pilots
                .FirstOrDefault(p => p.pilotId == selectedFlight.pilotId);

            if (pilot != null)
            {
                pilot.isAvailable = true;
            }

            Console.WriteLine("Flight cancelled successfully.");
            Console.WriteLine("Affected bookings: " + affectedBookings.Count);
            Console.WriteLine("Pilot is available again.");
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
                Console.WriteLine("2. Add Aircraft ");
                Console.WriteLine("3. Register Pilot");
                Console.WriteLine("4. View all Flights");
                Console.WriteLine("5. Schedule Flight");
                Console.WriteLine("6. Book Flight");
                Console.WriteLine("7. Cancel Booking");
                Console.WriteLine("8. Depart Flight");
                Console.WriteLine("9. Cancel Flight");
                Console.WriteLine("10. Passenger Booking History");
                Console.WriteLine("11. Flight Revenue & Load Factor Report");
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
                        AddAircraft();
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
                        BookFlight();
                        break;

                    case "7":
                        CancelBooking();
                        break;

                    case "8":
                        DepartFlight();
                        break;

                    case "9":
                        CancelFlight();
                        break;

                    case "10":
                        //PassengerBookingHistory();
                        break;

                    case "11":
                        //FlightRevenueLoadFactorReport();
                        break;

                    case "12":
                        //Flight01
                        //RevenueReport();
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
