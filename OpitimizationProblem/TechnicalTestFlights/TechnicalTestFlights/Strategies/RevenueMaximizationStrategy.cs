using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnicalTestFlights.Strategies
{
    public class RevenueMaximizationStrategy : IRevenueMaximizationStrategy
    {
        public int RemainingSeats { get; set; }
        
        public RevenueMaximizationStrategy(int seats)
        {
            RemainingSeats = seats;
        }

        public int MaximizeRevenue(List<Family> familiesPool, List<Family> seatedFamilies)
        {
            var maximizedRevenue = 0;

            var sortedFamilies = familiesPool
                .OrderByDescending(f => f.TotalPrice / Convert.ToDouble(f.RequiredSeats))
                .ThenBy(f => f.RequiredSeats)
                .ToList();

            foreach (var family in sortedFamilies)
            {
                if (family.RequiredSeats <= RemainingSeats)
                {
                    seatedFamilies.Add(family);
                    maximizedRevenue += family.TotalPrice;
                    RemainingSeats -= family.RequiredSeats;
                }
            }

            return maximizedRevenue;
        }
    }
}