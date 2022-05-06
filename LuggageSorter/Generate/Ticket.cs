using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSorter
{
    class Ticket
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Destination { get; private set; }
        public int AmountOfLuggage { get; private set; }

        public Ticket(int id)
        {
            Id = id;
            Name = RandomizeName();
            Destination = RandomizeDestination();
            AmountOfLuggage = RandomizeLuggageAmount();
        }

        Random random = new Random();

        private string RandomizeName()
        {
            int selector = random.Next(0, 100);

            if (selector < 33)
            {
                Name = $"Male customer";
                return Name;
            }
            else if (selector > 66)
            {
                Name = $"Female customer";
                return Name;
            }
            else
            {
                Name = $"Family customers";
                return Name;
            }
        }

        private string RandomizeDestination()
        {
            int selector = random.Next(0, 100);

            if (selector < 33)
            {
                Destination = "Amsterdam";
                return Destination;
            }
            else if (selector > 66)
            {
                Destination = "Barcelona";
                return Destination;
            }
            else
            {
                Destination = "London";
                return Destination;
            }
        }

        private int RandomizeLuggageAmount()
        {
            int selector = random.Next(1, 4);

            return selector;
        }
    }
}
