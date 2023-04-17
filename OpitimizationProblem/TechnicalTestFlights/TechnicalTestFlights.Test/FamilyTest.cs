using System.Linq;
using NUnit.Framework;

namespace TechnicalTestFlights.Test
{
    [TestFixture]
    public class FamilyTest
    {
        private Family _family;
        private Passenger _adult = new Passenger { Type = PassengerType.Adult };
        private Passenger _doubleAdult = new Passenger { Type = PassengerType.DoubleAdult };
        private Passenger _child = new Passenger { Type = PassengerType.Child };

        [SetUp]
        public void Setup()
        {
            _family = new Family();
        }

        [Test]
        public void AddMember_AddsAdult_ReturnsTrue()
        {
            Assert.IsTrue(_family.AddMember(_adult));
            Assert.AreEqual(1, _family.Members.Count);
            Assert.AreEqual(PassengerType.Adult, _family.Members.First().Type);
        }

        [Test]
        public void AddMember_AddsChild_ReturnsTrue()
        {
            Assert.IsTrue(_family.AddMember(_child));
            Assert.AreEqual(1, _family.Members.Count);
            Assert.AreEqual(PassengerType.Child, _family.Members.First().Type);
        }

        [Test]
        public void AddMember_AddsTooManyAdults_ReturnsFalse()
        {
            _family.AddMember(_adult);
            _family.AddMember(_adult);
            Assert.IsFalse(_family.AddMember(_adult));
            Assert.AreEqual(2, _family.Members.Count(m => m.Type == PassengerType.Adult));
        }

        [Test]
        public void AddMember_AddsTooManyChildren_ReturnsFalse()
        {
            _family.AddMember(_child);
            _family.AddMember(_child);
            _family.AddMember(_child);
            Assert.IsFalse(_family.AddMember(_child));
            Assert.AreEqual(3, _family.Members.Count(m => m.Type == PassengerType.Child));
        }

        [Test]
        public void AddMember_AddsTooManyDoubleAdults_ReturnsFalse()
        {
            _family.AddMember(_doubleAdult);
            _family.AddMember(_doubleAdult);
            Assert.IsFalse(_family.AddMember(_doubleAdult));
            Assert.AreEqual(2, _family.Members.Count(m => m.Type == PassengerType.DoubleAdult));
        }
    }
}