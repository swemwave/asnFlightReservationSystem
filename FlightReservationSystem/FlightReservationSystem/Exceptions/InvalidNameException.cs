using System;

namespace FlightReservationSystem.Exceptions
{
    public class InvalidNameException : Exception
    {
        public InvalidNameException(string message) : base(message) { }
    }
}