namespace FlightReservationSystem.Models
{
    public class Airport
    {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{Code} - {Name}";
        }
    }
}
