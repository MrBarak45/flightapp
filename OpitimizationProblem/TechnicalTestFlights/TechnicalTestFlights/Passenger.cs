using System;

namespace TechnicalTestFlights
{
    public class Passenger
    {
        public PassengerType Type { get; set; }

        public int TakenSpace => 
            Type == PassengerType.DoubleAdult ? 2 : 1;

        public int GetTicketPrice()
        {
            if (Type == PassengerType.Adult)
                return 250;
            if (Type == PassengerType.Child)
                return 150;
            if (Type == PassengerType.DoubleAdult)
                return 500;

            throw new ArgumentOutOfRangeException();
        }
    }
}