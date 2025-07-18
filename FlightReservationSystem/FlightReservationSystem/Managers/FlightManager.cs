using System.Collections.Generic;
using System.IO;
using System.Linq;
using FlightReservationSystem.Models;

namespace FlightReservationSystem.Managers
{
    public class FlightManager
    {
        public List<Flight> Flights { get; private set; } = new List<Flight>();

        public void LoadFlights(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                Flights.Add(new Flight
                {
                    FlightCode = parts[0].Trim(),
                    Airline = parts[1].Trim(),
                    Origin = parts[2].Trim(),
                    Destination = parts[3].Trim(),
                    Day = parts[4].Trim(),
                    Time = parts[5].Trim(),
                    Seats = int.Parse(parts[6].Trim()),
                    Cost = double.Parse(parts[7].Trim())
                });
            }
        }

        public List<Flight> FindFlights(string origin, string destination, string day)
        {
            return Flights.Where(f =>
                f.Origin.ToLower() == origin.ToLower() &&
                f.Destination.ToLower() == destination.ToLower() &&
                f.Day.ToLower() == day.ToLower()).ToList();
        }
    }
}