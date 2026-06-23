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

            Console.Write("Enter passenger name: ");
            string name = Console.ReadLine();

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

                int pilotId = context.Pilots.Count + 1;

                Console.Write("Enter pilot name: ");
                string name = Console.ReadLine();

                Console.Write("Enter license number: ");
                string license = Console.ReadLine();

                Console.Write("Enter phone number: ");
                string phone = Console.ReadLine();

                
                Pilot pilot = new Pilot
                {
                    pilotId = pilotId,
                    pilotName = name,
                    pilotPhone= phone,
                    licenseNumber = license,
                    
                    isAvailable = true
                };

                context.Pilots.Add(pilot);

                Console.WriteLine("Pilot added successfully.");

        }

        // ==========================================================
        // 3. Add Aircraft
        // ==========================================================
        static void AddAircraft()
        {
            Console.Clear();
            Console.WriteLine("===== Add Aircraft =====");

            int aircraftId = context.Aircrafts.Count + 1;

            Console.Write("Enter aircraft model: ");
            string model = Console.ReadLine();

            Console.Write("Enter aircraft code: ");
            string code = Console.ReadLine();

            Console.Write("Enter aircraft capacity: ");
            int capacity;

            if (!int.TryParse(Console.ReadLine(), out capacity))
            {
                Console.WriteLine("Invalid capacity.");
                return;
            }

            Aircraft aircraft = new Aircraft
            {
                aircraftId = aircraftId,
                Model = model,
                totalSeats = totalSeats,
                isOperational = true
                
            };

            context.Aircrafts.Add(aircraft);

            Console.WriteLine("Aircraft added successfully.");
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

            foreach (Flight flight in context.Flights)
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
                        AddAircraft();
                        break;

                    case "4":
                        ViewAllFlights();
                        break;

                    case "5":
                        //ViewPassengers();
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
