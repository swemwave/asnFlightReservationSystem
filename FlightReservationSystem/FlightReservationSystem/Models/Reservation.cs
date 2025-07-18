using System;

namespace FlightReservationSystem.Models
{
    public class Reservation
    {
        public string Code { get; set; } = string.Empty;
        public string FlightCode { get; set; } = string.Empty;
        public string Airline { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Citizenship { get; set; } = string.Empty;
        public double Cost { get; set; }
        public bool IsActive { get; set; } = true;

        public override string ToString()
        {
            return $"{Code} | {Name} | {Citizenship} | {FlightCode} | {Airline} | {Cost:C} | {(IsActive ? "Active" : "Inactive")}";
        }
    }
}
