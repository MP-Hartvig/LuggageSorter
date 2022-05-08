using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuggageSorter
{
    class Desk
    {
        public int Id { get; private set; }
        public bool Open { get; private set; }
        public DateTime Timestamp { get; private set; }
        public Queue<Luggage> LuggageQueue { get; private set; }

        public Desk(int id, bool open, DateTime timestamp)
        {
            Id = id;
            Open = open;
            Timestamp = timestamp;
            LuggageQueue = new Queue<Luggage>();
        }
    }
}
