using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuggageSorter
{
    class Luggage
    {
        public int Id { get; private set; }
        public Ticket AssociatedTicket { get; private set; }

        public Luggage(int id, Ticket ticket)
        {
            Id = id;
            AssociatedTicket = ticket;
        }

        public void AddLuggageToQueue(Queue<Luggage> luggageQueue, Luggage[] luggage)
        {
            if (Monitor.TryEnter(luggageQueue))
            {
                for (int i = 0; i < luggage.Length; i++)
                {
                    try
                    {
                        luggageQueue.Enqueue(luggage[i]);
                        Debug.WriteLine("Added luggage to queue");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                }
                Monitor.PulseAll(luggageQueue);
                Monitor.Exit(luggageQueue);
                Thread.Sleep(100 / 15);
            }
        }
    }
}
