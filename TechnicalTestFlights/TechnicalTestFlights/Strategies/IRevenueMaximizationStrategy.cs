using System.Collections.Generic;

namespace TechnicalTestFlights.Strategies
{
    public interface IRevenueMaximizationStrategy
    {
        int MaximizeRevenue(List<Family> familiesPool, List<Family> seatedFamilies);
    }
}