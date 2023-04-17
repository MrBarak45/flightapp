using System.Linq;
using FlightApp.Data;
using FlightApp.Models;

namespace FlightApp.Services
{
    public class FlightsService : IFlightsService
    {
        private readonly FlightContext _context;

        public FlightsService(FlightContext context)
        {
            _context = context;
        }

        public IQueryable<Flight> FilterFlights(string departureCity, string arrivalCity,
            string departureDate, string returnDate, int passengerCount)
        {
            var flights = _context.Flights.AsQueryable();
            if (!string.IsNullOrEmpty(departureCity))
                flights = flights.Where(f => f.DepartureCity == departureCity);

            if (!string.IsNullOrEmpty(arrivalCity))
                flights = flights.Where(f => f.ArrivalCity == arrivalCity);

            if (!string.IsNullOrEmpty(departureDate))
                flights = flights.Where(f => f.DepartureDate == departureDate);

            if (!string.IsNullOrEmpty(returnDate))
                flights = flights.Where(f => f.ReturnDate == returnDate);

            flights = flights.Where(f => f.Capacity >= passengerCount);

            return flights.OrderBy(f => f.Price);
        }
    }
}