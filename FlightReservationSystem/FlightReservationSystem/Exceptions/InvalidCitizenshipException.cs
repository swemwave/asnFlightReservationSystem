using System;

namespace FlightReservationSystem.Exceptions
{
    public class InvalidCitizenshipException : Exception
    {
        public InvalidCitizenshipException(string message) : base(message) { }
    }
}