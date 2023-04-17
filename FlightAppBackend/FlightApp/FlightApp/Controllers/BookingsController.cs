using System;
using System.Linq;
using FlightApp.Data;
using FlightApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController
    {
        private readonly FlightContext _context;

        public BookingsController(FlightContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<bool> CreateBooking([FromBody] Booking booking)
        {
            try
            {
                var flight = _context.Flights.FirstOrDefault(f => f.Id == booking.FlightId);

                if (flight == null)
                {
                    return false;
                }

                flight.Capacity -= booking.NumberOfPassengers; 
                _context.Entry(flight).State = EntityState.Modified;
                
                _context.Bookings.Add(booking);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}