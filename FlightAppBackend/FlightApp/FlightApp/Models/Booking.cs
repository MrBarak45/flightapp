using System.ComponentModel.DataAnnotations;

namespace FlightApp.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public int FlightId { get; set; }
        public int NumberOfPassengers { get; set; }
        public string PassengerName { get; set; }
        public string PassengerEmail { get; set; }
        public string PassengerPhone { get; set; }
    }
}