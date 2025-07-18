using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FlightReservationSystem.Models;
using FlightReservationSystem.Exceptions;

namespace FlightReservationSystem.Managers
{
    public class ReservationManager
    {
        private string filePath = "Data/reservations.json";
        private List<Reservation> reservations = new List<Reservation>();

        public ReservationManager()
        {
            Load();
        }

        public void Load()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                reservations = JsonSerializer.Deserialize<List<Reservation>>(json) ?? new List<Reservation>();
            }
        }

        public void Persist()
        {
            var json = JsonSerializer.Serialize(reservations, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public Reservation MakeReservation(Flight flight, string name, string citizenship)
        {
            if (flight == null)
                throw new NoFlightSelectedException("Flight must be selected.");
            if (string.IsNullOrWhiteSpace(name))
                throw new InvalidNameException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(citizenship))
                throw new InvalidCitizenshipException("Citizenship cannot be empty.");
            if (!flight.HasAvailableSeats)
                throw new NoSeatsAvailableException("Flight is fully booked.");

            flight.BookSeat();

            string code = GenerateCode();
            var reservation = new Reservation
            {
                Code = code,
                FlightCode = flight.FlightCode,
                Airline = flight.Airline,
                Name = name,
                Citizenship = citizenship,
                Cost = flight.Cost
            };

            reservations.Add(reservation);
            Persist();
            return reservation;
        }

        private string GenerateCode()
        {
            Random rand = new Random();
            char letter = (char)rand.Next('A', 'Z' + 1);
            int digits = rand.Next(1000, 9999);
            return $"{letter}{digits}";
        }

        public List<Reservation> FindReservations(string code, string airline, string name)
        {
            return reservations.Where(r =>
                (string.IsNullOrEmpty(code) || r.Code.Equals(code, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(airline) || r.Airline.IndexOf(airline, StringComparison.OrdinalIgnoreCase) >= 0) &&
                (string.IsNullOrEmpty(name) || r.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
        }

        public List<Reservation> GetAllReservations() => reservations;

        public void UpdateReservation(Reservation r, string newName, string newCitizenship, bool isActive)
        {
            if (string.IsNullOrWhiteSpace(newName))
                throw new InvalidNameException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(newCitizenship))
                throw new InvalidCitizenshipException("Citizenship cannot be empty.");

            r.Name = newName;
            r.Citizenship = newCitizenship;
            r.IsActive = isActive;

            Persist();
        }
    }
}
