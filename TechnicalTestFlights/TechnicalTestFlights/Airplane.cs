using System.Collections.Generic;
using TechnicalTestFlights.Strategies;

namespace TechnicalTestFlights
{
    public class Airplane
    {
        private readonly IRevenueMaximizationStrategy _maximizationStrategy;
        public List<Family> Families { get; set; }
        public int MaxRevenue { get; set; }

        public Airplane(IRevenueMaximizationStrategy maximizationStrategy)
        {
            Families = new List<Family>();
            _maximizationStrategy = maximizationStrategy;
        }

        public int MaximizeRevenue(List<Family> families)
        {
            return MaxRevenue = _maximizationStrategy.MaximizeRevenue(families, Families);
        }
    }
}