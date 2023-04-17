using System;
using System.Collections.Generic;
using System.Linq;
using FlightApp.Models;
using FlightApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        //todo add ILogger maybe ?
        private readonly IFlightsService _flightsService;

        public FlightsController(IFlightsService flightsService)
        {
            _flightsService = flightsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Flight>> SearchFlights(string departureCity, string arrivalCity, 
            string departureDate, string returnDate, int passengerCount)
        {
            try
            {
                var filteredFlights = _flightsService.FilterFlights(departureCity, arrivalCity, departureDate, returnDate, passengerCount);
                return filteredFlights.ToList();
            }
            catch (Exception e)
            {
                //log stuff ILogger
                return StatusCode(500, $"Following error occured: {e.Message}");
            }
        }
    }
}