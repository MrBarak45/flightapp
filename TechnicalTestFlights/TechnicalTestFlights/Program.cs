using System;
using TechnicalTestFlights.Strategies;

namespace TechnicalTestFlights
{
    class Program
    {
        static void DisplayResults(Airplane airplane)
        {
            var count = 1;
            foreach (var family in airplane.Families)
            {
                Console.WriteLine($"Family {count++}:");
             
                foreach (var member in family.Members)
                    Console.WriteLine($"-{member.Type}");

                Console.WriteLine();
            }

            Console.WriteLine($"Total revenue: {airplane.MaxRevenue}");
        }

        static void Main(string[] args)
        {
            var families = new FamilyGenerator().GenerateFamilies();
            var airplane = new Airplane(new RevenueMaximizationStrategy(200));

            airplane.MaximizeRevenue(families);
            DisplayResults(airplane);
        }
    }
}

