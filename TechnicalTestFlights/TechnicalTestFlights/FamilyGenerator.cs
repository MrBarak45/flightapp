using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TechnicalTestFlights
{
    public class FamilyGenerator
    {
        public List<Family> GenerateFamilies()
        {
            var families = new List<Family>();

            var family1 = new Family();
            family1.AddMember(new Passenger { Type = PassengerType.Adult });
            family1.AddMember(new Passenger { Type = PassengerType.Child });
            families.Add(family1);

            var family2 = new Family();
            family2.AddMember(new Passenger { Type = PassengerType.Adult });
            family2.AddMember(new Passenger { Type = PassengerType.Child });
            family2.AddMember(new Passenger { Type = PassengerType.Child });
            families.Add(family2);

            var family3 = new Family();
            family3.AddMember(new Passenger { Type = PassengerType.Adult });
            family3.AddMember(new Passenger { Type = PassengerType.Child });
            family3.AddMember(new Passenger { Type = PassengerType.Child });
            families.Add(family3);

            var singleAdult = new Family();
            singleAdult.AddMember(
                new Passenger { Type = PassengerType.Adult });
            families.Add(singleAdult);

            var largeAdult = new Family();
            largeAdult.AddMember(new Passenger { Type = PassengerType.DoubleAdult });
            families.Add(largeAdult);

            return families;
        }

        public List<Family> GenerateFamiliesFromJson(string jsonPath)
        {
            var jsonData = File.ReadAllText(jsonPath);
            var familiesData = JsonConvert.DeserializeObject<List<List<Passenger>>>(jsonData, new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> { new StringEnumConverter() }
            });

            var families = new List<Family>();

            foreach (var familyData in familiesData)
            {
                var family = new Family();

                foreach (var passenger in familyData)
                {
                    family.AddMember(passenger);
                }

                families.Add(family);
            }

            return families;
        }
    }
}