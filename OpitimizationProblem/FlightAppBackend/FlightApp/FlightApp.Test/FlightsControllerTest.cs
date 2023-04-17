using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using FlightApp.Controllers;
using FlightApp.Models;
using FlightApp.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace FlightApp.Test
{
    [TestFixture]
    public class FlightsControllerTest
    {
        private FlightsController _controller;
        private Mock<IFlightsService> _flightServiceMock;

        [SetUp]
        public void Setup()
        {
            _flightServiceMock = new Mock<IFlightsService>();
            _controller = new FlightsController(_flightServiceMock.Object);
        }

        [Test]
        public void SearchFlights_Should_Return_CorrectValues()
        {
            // Arrange
            var expectedTestFlights = new Faker<Flight>().Generate(10);
            
            _flightServiceMock
                .Setup(fs => fs.FilterFlights(It.Is<string>(m => m == "ORY"),
                    It.Is<string>(m => m == "FES"),
                    It.Is<string>(m => m == "2023-04-05"),
                    It.Is<string>(m => m == "2023-04-12"),
                    It.Is<int>(m => m == 3)))
                .Returns(expectedTestFlights.AsQueryable());

            // Act
            var actionResult = _controller.SearchFlights("ORY", "FES", "2023-04-05", "2023-04-12", 3);

            // Assert
            Assert.IsInstanceOf<ActionResult<IEnumerable<Flight>>>(actionResult);
            var flights = actionResult.Value.ToList();

            CollectionAssert.AreEqual(expectedTestFlights, flights);
        }

        [Test]
        public void SearchFlights_Should_ThrowException()
        {
            // Arrange
            var expectedTestFlight = new Faker<Flight>().Generate();
            
            _flightServiceMock
                .Setup(fs => fs.FilterFlights(It.Is<string>(m => m == expectedTestFlight.DepartureCity),
                    It.Is<string>(m => m == expectedTestFlight.ArrivalCity),
                    It.Is<string>(m => m == expectedTestFlight.DepartureDate),
                    It.Is<string>(m => m == expectedTestFlight.ReturnDate),
                    It.Is<int>(m => m == expectedTestFlight.Capacity)))
                .Throws(new InvalidOperationException("An error occurred"));

            // Act
            var actionResult = _controller.SearchFlights(expectedTestFlight.DepartureCity,
                expectedTestFlight.ArrivalCity, expectedTestFlight.DepartureDate, expectedTestFlight.ReturnDate,
                expectedTestFlight.Capacity);

            // Assert
            _flightServiceMock.Verify(fs => fs.FilterFlights(expectedTestFlight.DepartureCity,
                expectedTestFlight.ArrivalCity, expectedTestFlight.DepartureDate, expectedTestFlight.ReturnDate,
                expectedTestFlight.Capacity));

            Assert.AreEqual(500, ((ObjectResult)actionResult.Result).StatusCode);
        }
    }
}