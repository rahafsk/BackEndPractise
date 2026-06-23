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
