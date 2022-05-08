using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LuggageSorter
{
    class Manager
    {
        public Manager()
        {
        }

        int id = 1;

        Thread produce;
        Thread sorter;

        Queue<Luggage> amsterdamQueue = new Queue<Luggage>();
        Queue<Luggage> barcelonaQueue = new Queue<Luggage>();
        Queue<Luggage> londonQueue = new Queue<Luggage>();

        Desk deskInfo = new Desk(1, true, DateTime.Now.ToLocalTime());
        Desk deskInfo2 = new Desk(2, true, DateTime.Now.ToLocalTime());
        Desk deskInfo3 = new Desk(3, true, DateTime.Now.ToLocalTime());

        public void Initializer()
        {
            produce = new Thread(ProduceLuggage);
            sorter = new Thread(SortLuggage);

            produce.Start();
            sorter.Start();
        }

        public void Terminator()
        {
            try
            {
                produce.Join();
                sorter.Join();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        private void ProduceLuggage()
        {
            Random rand = new Random();            

            while (true)
            {
                int selector = rand.Next(1, 4);

                Ticket randomTicket = new Ticket(id); // New tickets are generated but destination stays the same

                int arraySize = randomTicket.AmountOfLuggage;

                Luggage[] amountOfLuggage = new Luggage[arraySize];

                if (arraySize > 1)
                {
                    for (int i = 0; i < arraySize; i++)
                    {
                        amountOfLuggage[i] = new Luggage(id, randomTicket);
                    }
                }
                else
                {
                    amountOfLuggage[0] = new Luggage(id, randomTicket);
                }

                if (selector == 1) // Needs another condition
                {
                    amountOfLuggage[0].AddLuggageToQueue(deskInfo.LuggageQueue, amountOfLuggage);
                    Debug.WriteLine("Luggage was added to desk1");
                }
                else if (selector == 2) // Needs another condition
                {
                    amountOfLuggage[0].AddLuggageToQueue(deskInfo2.LuggageQueue, amountOfLuggage);
                    Debug.WriteLine("Luggage was added to desk2");
                }
                else
                {
                    amountOfLuggage[0].AddLuggageToQueue(deskInfo3.LuggageQueue, amountOfLuggage);
                    Debug.WriteLine("Luggage was added to desk3");
                }
                id++;
                Thread.Sleep(100 / 15);
            }
        }

        private void SortLuggage()
        {
            Sorter sortDesk1 = new Sorter(deskInfo.LuggageQueue, amsterdamQueue, barcelonaQueue, londonQueue);
            Sorter sortDesk2 = new Sorter(deskInfo.LuggageQueue, amsterdamQueue, barcelonaQueue, londonQueue);
            Sorter sortDesk3 = new Sorter(deskInfo.LuggageQueue, amsterdamQueue, barcelonaQueue, londonQueue);            

            while (true)
            {
                if (deskInfo.LuggageQueue.Count > 0)
                {
                    sortDesk1.SortByDestination();
                    Debug.WriteLine("Queue was sent from disk1 to sorter");
                }

                if (deskInfo2.LuggageQueue.Count > 0)
                {
                    sortDesk2.SortByDestination();
                    Debug.WriteLine("Queue was sent from disk2 to sorter");
                }

                if (deskInfo3.LuggageQueue.Count > 0)
                {
                    sortDesk3.SortByDestination();
                    Debug.WriteLine("Queue was sent from disk3 to sorter");
                }
                Thread.Sleep(500);
            }
        }
    }
}
