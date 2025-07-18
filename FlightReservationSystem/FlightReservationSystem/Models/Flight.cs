using System;

namespace FlightReservationSystem.Models
{
    public abstract class FlightBase
    {
        public string FlightCode { get; set; } = string.Empty;
        public string Airline { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string Day { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public int Seats { get; set; }
        public double Cost { get; set; }

        public abstract override string ToString();
    }

    public class Flight : FlightBase
    {
        public int BookedSeats { get; private set; } = 0;

        public bool HasAvailableSeats => BookedSeats < Seats;

        public void BookSeat()
        {
            if (!HasAvailableSeats)
                throw new InvalidOperationException("No more seats available on this flight.");
            BookedSeats++;
        }

        public override string ToString()
        {
            return $"{FlightCode} - {Airline} | {Origin} -> {Destination} on {Day} at {Time}, ${Cost}";
        }
    }
}
