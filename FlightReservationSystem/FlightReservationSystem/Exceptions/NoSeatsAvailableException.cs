using System;

namespace FlightReservationSystem.Exceptions
{
    public class NoSeatsAvailableException : Exception
    {
        public NoSeatsAvailableException(string message) : base(message) { }
    }
}