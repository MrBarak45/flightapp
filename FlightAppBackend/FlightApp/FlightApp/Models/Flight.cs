using System.ComponentModel.DataAnnotations;

namespace FlightApp.Models
{
    public class Flight
    {
        [Key]
        public int Id { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public string DepartureDate { get; set; }
        public string ReturnDate { get; set; }
        public int Capacity { get; set; }
        public double Price { get; set; }
    }
}
