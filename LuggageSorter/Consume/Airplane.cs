using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuggageSorter
{
    internal class Airplane
    {
        public int Id { get; private set; }
        public string Destination { get; private set; }
        public Queue<Luggage> LuggageQueue { get; private set; }

        public Airplane(int id, string destination, Queue<Luggage> luggageQueue)
        {
            Id = id;
            Destination = destination;
            LuggageQueue = luggageQueue;
        }

        public void TakeOff()
        {
            if (Monitor.TryEnter(LuggageQueue))
            {
                if (LuggageQueue.Count < 50)
                {
                    Monitor.Wait(LuggageQueue);
                    Debug.WriteLine($"Waiting for luggage on plane {Id} going to {Destination}");
                }

                while (LuggageQueue.Count != 0)
                {
                    try
                    {
                        LuggageQueue.Dequeue();
                        Debug.WriteLine($"Luggage was loaded on plane {Id} going to {Destination}");
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                    finally
                    {
                        Monitor.PulseAll(LuggageQueue);
                        Monitor.Exit(LuggageQueue);
                    }
                    Thread.Sleep(100);
                }
            }
        }
    }
}
