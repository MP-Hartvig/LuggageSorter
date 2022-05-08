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

        int maxAmount = 0;

        public void AddLuggageToQueue(Queue<Luggage> luggageQueue, Luggage luggage)
        {
            while (maxAmount < 100)
            {
                if (Monitor.TryEnter(luggageQueue))
                {
                    try
                    {
                        luggageQueue.Enqueue(luggage);
                        maxAmount++;
                        Debug.WriteLine("Added luggage to queue");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                    finally
                    {
                        Monitor.Pulse(luggageQueue);
                        Monitor.Exit(luggageQueue);
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}
