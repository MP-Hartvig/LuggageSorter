using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuggageSorter
{
    internal class Sorter
    {
        Queue<Luggage> LuggageQueue;
        Queue<Luggage> AmsterdamQueue;
        Queue<Luggage> BarcelonaQueue;
        Queue<Luggage> LondonQueue;

        public Sorter(Queue<Luggage> luggageQueue, Queue<Luggage> amsterdamQueue, 
                      Queue<Luggage> barcelonaQueue, Queue<Luggage> londonQueue)
        {
            LuggageQueue = luggageQueue;
            AmsterdamQueue = amsterdamQueue;
            BarcelonaQueue = barcelonaQueue;
            LondonQueue = londonQueue;
        }

        public void SortByDestination()
        {
            Luggage luggage = null;

            while (true)
            {
                while (luggage == null)
                {
                    if (Monitor.TryEnter(LuggageQueue))
                    {
                        if (LuggageQueue.Count == 0)
                        {
                            Monitor.Wait(LuggageQueue);
                            Debug.WriteLine("Waiting for luggageQueue");
                        }

                        try
                        {
                            luggage = LuggageQueue.Peek();
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
                    }
                    Thread.Sleep(100 / 15);
                }

                if (luggage.AssociatedTicket.Destination.StartsWith("A"))
                {

                    if (Monitor.TryEnter(AmsterdamQueue))
                    {
                        try
                        {
                            AmsterdamQueue.Enqueue(luggage);
                            Debug.WriteLine("Luggage was added to the Amsterdam queue");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                        finally
                        {
                            Monitor.Pulse(AmsterdamQueue);
                            Monitor.Exit(AmsterdamQueue);
                        }
                        DequeueLuggage();
                    }
                }
                else if (luggage.AssociatedTicket.Destination.StartsWith("B"))
                {
                    if (Monitor.TryEnter(BarcelonaQueue))
                    {
                        try
                        {
                            BarcelonaQueue.Enqueue(luggage);
                            Debug.WriteLine("Luggage was added to the Barcelona queue");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                        finally
                        {
                            Monitor.Pulse(BarcelonaQueue);
                            Monitor.Exit(BarcelonaQueue);
                        }
                        DequeueLuggage();
                    }
                }
                else
                {
                    if (Monitor.TryEnter(LondonQueue))
                    {
                        try
                        {
                            LondonQueue.Enqueue(luggage);
                            Debug.WriteLine("Luggage was added to the London queue");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                        finally
                        {
                            Monitor.Pulse(LondonQueue);
                            Monitor.Exit(LondonQueue);
                        }
                        DequeueLuggage();
                    }
                }
                Thread.Sleep(100 / 15);
            }
        }

        public void DequeueLuggage()
        {
            if (Monitor.TryEnter(LuggageQueue))
            {
                if (LuggageQueue.Count == 0)
                {
                    Monitor.Wait(LuggageQueue);
                    Debug.WriteLine("Waiting for luggageQueue");
                }

                try
                {
                    LuggageQueue.Dequeue();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                finally
                {
                    if (Monitor.IsEntered(LuggageQueue))
                    {
                        Monitor.PulseAll(LuggageQueue);
                        Monitor.Exit(LuggageQueue);
                    }
                }
            }
        }
    }
}
