using System.Collections.Generic;
using FlightApp.Data;
using FlightApp.Models;
using FlightApp.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;

namespace FlightApp.Test
{
    [TestFixture]
    public class FlightsServiceTest
    {
        private FlightsService _flightsService;
        private FlightContext _context;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<FlightContext>()
                .UseInMemoryDatabase(databaseName: "Flight")
                .Options;

            _context = new FlightContext(options);
            _flightsService = new FlightsService(_context);

            GenerateFlightsOnDatabase();
        }

        [Test]
        public void FilterFlights_Should_FilterBy_DepartureCity()
        {
            // Arrange
            var departureCity = "ORY";

            // Act
            var filteredFlights = _flightsService.FilterFlights(departureCity, null, null, null, 0);

            // Assert
            Assert.IsTrue(filteredFlights.All(f => f.DepartureCity == departureCity));
        }

        [Test]
        public void FilterFlights_Should_FilterBy_ArrivalCity()
        {
            // Arrange
            var arrivalCity = "AUS";

            // Act
            var filteredFlights = _flightsService.FilterFlights(null, arrivalCity, null, null, 0);

            // Assert
            Assert.IsTrue(filteredFlights.All(f => f.ArrivalCity == arrivalCity));
        }

        [Test]
        public void FilterFlights_Should_FilterBy_DepartureDate()
        {
            // Arrange
            var departureDate = "2023-05-01";

            // Act
            var filteredFlights = _flightsService.FilterFlights(null, null, departureDate, null, 0);

            // Assert
            Assert.IsTrue(filteredFlights.All(f => f.DepartureDate == departureDate));
        }

        [Test]
        public void FilterFlights_Should_FilterBy_ReturnDate()
        {
            // Arrange
            var returnDate = "2023-05-10";

            // Act
            var filteredFlights = _flightsService.FilterFlights(null, null, null, returnDate, 0);

            // Assert
            Assert.IsTrue(filteredFlights.All(f => f.ReturnDate == returnDate));
        }

        [Test]
        public void FilterFlights_Should_FilterBy_Capacity()
        {
            // Arrange
            var passengerCount = 50;

            // Act
            var filteredFlights = _flightsService.FilterFlights(null, null, null, null, passengerCount);

            // Assert
            Assert.IsTrue(filteredFlights.All(f => f.Capacity >= passengerCount));
        }

        [Test]
        public void FilterFlights_Should_ReturnFlights_SortedByPrice()
        {
            // Act
            var filteredFlights = _flightsService.FilterFlights(null, null, null, null, 0).ToList();

            //Assert
            for (var i = 1; i < filteredFlights.Count; i++)
            {
                Assert.LessOrEqual(filteredFlights[i - 1].Price, filteredFlights[i].Price);
            }
        }

        private void GenerateFlightsOnDatabase()
        {
            _context.Flights.AddRange(new List<Flight>
            {
                new Flight { Id = 1, DepartureCity = "ORY", ArrivalCity = "FES", DepartureDate = "2023-04-05", ReturnDate = "2023-04-12", Capacity = 150, Price = 678 },
                new Flight { Id = 2, DepartureCity = "ORY", ArrivalCity = "JFK", DepartureDate = "2023-04-05", ReturnDate = "2023-04-12", Capacity = 150, Price = 678 },
                new Flight { Id = 3, DepartureCity = "FES", ArrivalCity = "AUS", DepartureDate = "2023-05-01", ReturnDate = "2023-06-10", Capacity = 124, Price = 552 },
                new Flight { Id = 4, DepartureCity = "LAX", ArrivalCity = "AUS", DepartureDate = "2023-05-01", ReturnDate = "2023-06-11", Capacity = 124, Price = 552 },
                new Flight { Id = 5, DepartureCity = "CDG", ArrivalCity = "ORY", DepartureDate = "2023-03-02", ReturnDate = "2023-05-07", Capacity = 30, Price = 355 }
            });

            _context.SaveChanges();
        }

    }
}