using System.Collections.Generic;
using System.Linq;

namespace TechnicalTestFlights
{
    public class Family
    {
        private const int MaxAdults = 2;
        private const int MaxChildren = 3;

        public Family()
        {
            Members = new List<Passenger>();
        }

        public List<Passenger> Members { get; }
        
        public int RequiredSeats => 
            Members.Sum(m => m.TakenSpace);
        public int TotalPrice => 
            Members.Sum(m => m.GetTicketPrice());

        public bool AddMember(Passenger passenger)
        {
            if (!VerifyAdultPassenger(passenger) || !VerifyChildPassenger(passenger))
            {
                return false;
            }

            Members.Add(passenger);
            return true;
        }

        private bool VerifyAdultPassenger(Passenger passenger)
        {
            if (passenger.Type == PassengerType.Adult &&
                Members.Count(m => m.Type == PassengerType.Adult) >= MaxAdults)
            {
                return false;
            }

            if (passenger.Type == PassengerType.DoubleAdult && 
                Members.Count(m => m.Type == PassengerType.DoubleAdult) >= MaxAdults)
            {
                return false;
            }

            return true;
        }

        private bool VerifyChildPassenger(Passenger passenger)
        {
            if (passenger.Type == PassengerType.Child &&
                Members.Count(m => m.Type == PassengerType.Child) >= MaxChildren)
            {
                return false;
            }

            return true;
        }
    }
}