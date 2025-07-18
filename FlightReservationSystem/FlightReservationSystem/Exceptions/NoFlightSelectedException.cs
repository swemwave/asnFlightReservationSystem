using System;

namespace FlightReservationSystem.Exceptions
{
    public class NoFlightSelectedException : Exception
    {
        public NoFlightSelectedException(string message) : base(message) { }
    }
}