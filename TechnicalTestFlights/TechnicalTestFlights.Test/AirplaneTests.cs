using System.Collections.Generic;
using NUnit.Framework;
using TechnicalTestFlights.Strategies;
using Moq;

namespace TechnicalTestFlights.Test
{
    [TestFixture]
    public class AirplaneTest
    {
        [TestFixture]
        public class AirplaneTests
        {
            private Mock<IRevenueMaximizationStrategy> _strategyMock;
            private Airplane _airplane;

            [SetUp]
            public void SetUp()
            {
                _strategyMock = new Mock<IRevenueMaximizationStrategy>();
                _airplane = new Airplane(_strategyMock.Object);
            }

            [Test]
            public void MaximizeRevenue_CallsMaximizeRevenueWithFamilies_ShouldReturnCorrectMaxRevenue()
            {
                // Arrange
                var families = new List<Family> { new Family(), new Family(), new Family() };
                var expectedMaxRevenue = 1000;

                _strategyMock.Setup(x => x.MaximizeRevenue(families, It.IsAny<List<Family>>()))
                    .Returns(expectedMaxRevenue);

                // Act
                var maxRevenue = _airplane.MaximizeRevenue(families);

                // Assert
                Assert.AreEqual(expectedMaxRevenue, maxRevenue);
                _strategyMock.Verify(x => x.MaximizeRevenue(families, It.IsAny<List<Family>>()), Times.Once);
            }

            [Test]
            public void MaximizeRevenue_Successful_ShouldAddFamiliesToSeatedFamilies()
            {
                // Arrange
                var expectedFamilies = new List<Family> { new Family(), new Family(), new Family() };
                var expectedMaxRevenue = 1000;

                _strategyMock.Setup(x => x.MaximizeRevenue(expectedFamilies, It.IsAny<List<Family>>()))
                    .Returns(expectedMaxRevenue)
                    .Callback((List<Family> familyPool, List<Family> seatedFamilies) =>
                        seatedFamilies.AddRange(expectedFamilies));

                // Act
                _airplane.MaximizeRevenue(expectedFamilies);

                // Assert
                CollectionAssert.AreEqual(expectedFamilies, _airplane.Families);
            }

            [Test]
            public void MaximizeRevenue_NotSuccessful_FamiliesPropertyStayEmpty()
            {
                // Arrange
                var families = new List<Family> { new Family(), new Family(), new Family() };
                var expectedMaxRevenue = 0;

                _strategyMock.Setup(x => x.MaximizeRevenue(families, It.IsAny<List<Family>>()))
                    .Returns(expectedMaxRevenue);

                // Act
                _airplane.MaximizeRevenue(families);

                // Assert
                Assert.IsEmpty(_airplane.Families);
            }
        }
    }
}