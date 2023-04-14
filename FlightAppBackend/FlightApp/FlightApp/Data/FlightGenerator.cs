using System;
using System.Collections.Generic;
using FlightApp.Models;

namespace FlightApp.Data
{
    public class FlightGenerator
    {
        public static List<Flight> GenerateRandomFlights(int numberOfFlights, string[] cities, DateTime startDate, DateTime endDate)
        {
            var random = new Random();
            var flights = new List<Flight>();

            var totalDays = (endDate - startDate).Days;
            for (var i = 0; i < numberOfFlights; i++)
            {
                var departureCity = cities[random.Next(cities.Length)];
                string arrivalCity;

                do
                {
                    arrivalCity = cities[random.Next(cities.Length)];
                } while (arrivalCity == departureCity);

                var randomDepartureDay = random.Next(totalDays);
                var departureDate = startDate.AddDays(randomDepartureDay);

                var remainingDays = (endDate - departureDate).Days;
                var returnDate = departureDate.AddDays(random.Next(1, remainingDays + 1));

                var capacity = random.Next(151);
                var price = random.Next(80, 1301);

                flights.Add(new Flight
                {
                    Id = i + 1,
                    DepartureCity = departureCity,
                    ArrivalCity = arrivalCity,
                    DepartureDate = departureDate.ToString("yyyy-MM-dd"),
                    ReturnDate = returnDate.ToString("yyyy-MM-dd"),
                    Capacity = capacity,
                    Price = price
                });
            }

            return flights;
        }
    }
}