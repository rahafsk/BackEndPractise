using FlighManagementSystem.models;
using FlighManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Text;

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

                Console.Write("Enter experience years: ");
                int experienceYears;

                if (!int.TryParse(Console.ReadLine(), out experienceYears))
                {
                    Console.WriteLine("Invalid number.");
                    return;
                }

                Pilot pilot = new Pilot
                {
                    pilotId = pilotId,
                    pilotName = name,
                    licenseNumber = license,
                    experienceYears = experienceYears,
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
                aircraftModel = model,
                aircraftCode = code,
                capacity = capacity,
                isAvailable = true
            };

            context.Aircrafts.Add(aircraft);

            Console.WriteLine("Aircraft added successfully.");
        }

        // ==========================================================
        // 4. Create Flight
        // ==========================================================
        static void CreateFlight()
        {
            Console.Clear();
            Console.WriteLine("===== Create Flight =====");

            if (!context.Pilots.Any(p => p.isAvailable == true))
            {
                Console.WriteLine("No available pilots.");
                return;
            }

            if (!context.Aircrafts.Any(a => a.isAvailable == true))
            {
                Console.WriteLine("No available aircrafts.");
                return;
            }

            int flightId = context.Flights.Count + 1;

            Console.Write("Enter flight number: ");
            string flightNumber = Console.ReadLine();

            Console.WriteLine("\nAvailable Pilots:");
            foreach (Pilot pilot in context.Pilots.Where(p => p.isAvailable == true))
            {
                Console.WriteLine($"ID: {pilot.pilotId} | Name: {pilot.pilotName}");
            }

            Console.Write("Choose pilot ID: ");
            int pilotId;

            if (!int.TryParse(Console.ReadLine(), out pilotId))
            {
                Console.WriteLine("Invalid pilot ID.");
                return;
            }

            Pilot selectedPilot = context.Pilots.FirstOrDefault(p => p.pilotId == pilotId && p.isAvailable == true);

            if (selectedPilot == null)
            {
                Console.WriteLine("Pilot not found or not available.");
                return;
            }

            Console.WriteLine("\nAvailable Aircrafts:");
            foreach (Aircraft aircraft in context.Aircrafts.Where(a => a.isAvailable == true))
            {
                Console.WriteLine($"ID: {aircraft.aircraftId} | Model: {aircraft.aircraftModel} | Capacity: {aircraft.capacity}");
            }

            Console.Write("Choose aircraft ID: ");
            int aircraftId;

            if (!int.TryParse(Console.ReadLine(), out aircraftId))
            {
                Console.WriteLine("Invalid aircraft ID.");
                return;
            }

            Aircraft selectedAircraft = context.Aircrafts.FirstOrDefault(a => a.aircraftId == aircraftId && a.isAvailable == true);

            if (selectedAircraft == null)
            {
                Console.WriteLine("Aircraft not found or not available.");
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
            decimal ticketPrice;

            if (!decimal.TryParse(Console.ReadLine(), out ticketPrice))
            {
                Console.WriteLine("Invalid ticket price.");
                return;
            }

            Flight flight = new Flight
            {
                flightId = flightId,
                flightNumber = flightNumber,
                pilotId = selectedPilot.pilotId,
                aircraftId = selectedAircraft.aircraftId,
                origin = origin,
                destination = destination,
                departureDate = date,
                departureTime = time,
                ticketPrice = ticketPrice,
                totalSeats = selectedAircraft.capacity,
                availableSeats = selectedAircraft.capacity,
                status = "Scheduled"
            };

            context.Flights.Add(flight);

            selectedPilot.isAvailable = false;
            selectedAircraft.isAvailable = false;

            Console.WriteLine("Flight created successfully.");
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
                        // AddAircraft();
                        break;

                    case "4":
                        //CreateFlight();
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
