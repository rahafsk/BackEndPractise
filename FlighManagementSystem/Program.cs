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
    }
}
