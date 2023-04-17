using System.Collections.Generic;
using NUnit.Framework;
using TechnicalTestFlights.Strategies;

namespace TechnicalTestFlights.Test
{
    [TestFixture]
    public class RevenueMaximizationStrategyTest
    {
        private RevenueMaximizationStrategy _strategy;

        [SetUp]
        public void Setup()
        {
            _strategy = new RevenueMaximizationStrategy(10);
        }

        [Test]
        public void MaximizeRevenue_EnoughSeats_AllFamiliesAdded()
        {
            // Arrange
            var jsonPath = "FamilySets/families-enough-seats.json";
            var expectedFamilies = new FamilyGenerator().GenerateFamiliesFromJson(jsonPath);
            var seatedFamilies = new List<Family>();

            // Act
            var revenue = _strategy.MaximizeRevenue(expectedFamilies, seatedFamilies);

            // Assert
            Assert.AreEqual(1300, revenue);
            CollectionAssert.AreEquivalent(expectedFamilies, seatedFamilies);
        }

        [Test]
        public void MaximizeRevenue_NotEnoughSeats_MaximizeRevenue()
        {
            // Arrange
            var jsonPath = "FamilySets/families-not-enough-seats.json";
            var families = new FamilyGenerator().GenerateFamiliesFromJson(jsonPath);
            var seatedFamilies = new List<Family>();

            // Act
            var revenue = _strategy.MaximizeRevenue(families, seatedFamilies);

            // Assert
            Assert.AreEqual(1900, revenue);
            CollectionAssert.Contains(seatedFamilies, families[0]);
            CollectionAssert.Contains(seatedFamilies, families[1]);
            CollectionAssert.DoesNotContain(seatedFamilies, families[2]);
        }

        [Test]
        public void MaximizeRevenue_NoFamilies_ReturnsZero()
        {
            // Arrange
            var families = new List<Family>();
            var seatedFamilies = new List<Family>();

            // Act
            var revenue = _strategy.MaximizeRevenue(families, seatedFamilies);

            // Assert
            Assert.AreEqual(0, revenue);
            Assert.IsEmpty(seatedFamilies);
        }

        [Test]
        public void MaximizeRevenue_LargeFamilies_SufficientSeats()
        {
            // Arrange
            var jsonPath = "FamilySets/family-set-1.json";
            var families = new FamilyGenerator().GenerateFamiliesFromJson(jsonPath);
            var strategy = new RevenueMaximizationStrategy(200);

            // Act
            var revenue = strategy.MaximizeRevenue(families, new List<Family>());

            // Assert
            Assert.AreEqual(19250, revenue);
            Assert.AreEqual(101, strategy.RemainingSeats);
        }

        [Test]
        public void MaximizeRevenue_LargeFamilies_InsufficientSeats()
        {
            // Arrange
            var jsonPath = "FamilySets/family-set-2.json";
            var families = new FamilyGenerator().GenerateFamiliesFromJson(jsonPath);
            var strategy = new RevenueMaximizationStrategy(100);

            // Act
            var revenue = strategy.MaximizeRevenue(families, new List<Family>());

            // Assert
            Assert.AreEqual(19800, revenue);
            Assert.AreEqual(0, strategy.RemainingSeats);
        }
    }
}