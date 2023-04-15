using System;
using FlightApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightApp.Data
{
    public class FlightContext : DbContext
    {
        public FlightContext(DbContextOptions<FlightContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string[] airportCodeNames = { "LEJ", "FEZ", "ORY" };
            var startDate = new DateTime(2023, 4, 13);
            var endDate = new DateTime(2023, 4, 18);
            
            var flights = FlightGenerator.GenerateRandomFlights(5, airportCodeNames, startDate, endDate);

            modelBuilder.Entity<Flight>().HasData(flights);
        }

        public DbSet<Flight> Flights { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    }
}