using System.Linq;
using FlightApp.Models;

namespace FlightApp.Services
{
    public interface IFlightsService
    {
        IQueryable<Flight> FilterFlights(string departureCity, string arrivalCity,
            string departureDate, string returnDate, int passengerCount);
    }
}