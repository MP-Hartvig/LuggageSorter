﻿using System;
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

        int id = 0;

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
            while (id < 100)
            {
                Ticket randomTicket = new Ticket(id);

                //Luggage[] amountOfLuggage = new Luggage[3];

                //for (int i = 0; i <= randomTicket.AmountOfLuggage; i++)
                //{
                //    amountOfLuggage[i] = new Luggage(id, randomTicket);
                //}

                Luggage luggage = new Luggage(id, randomTicket);

                if (id < 33) // Needs another condition
                {
                    luggage.AddLuggageToQueue(deskInfo.LuggageQueue, luggage);
                    Debug.WriteLine("Luggage was added to desk1");
                }
                else if (id > 66) // Needs another condition
                {
                    luggage.AddLuggageToQueue(deskInfo2.LuggageQueue, luggage);
                    Debug.WriteLine("Luggage was added to desk3");
                }
                else
                {
                    luggage.AddLuggageToQueue(deskInfo3.LuggageQueue, luggage);
                    Debug.WriteLine("Luggage was added to desk2");
                }
                id++;
            }
        }

        private void SortLuggage()
        {
            Sorter sortDesk1 = new Sorter(deskInfo.LuggageQueue, amsterdamQueue, barcelonaQueue, londonQueue);
            Sorter sortDesk2 = new Sorter(deskInfo.LuggageQueue, amsterdamQueue, barcelonaQueue, londonQueue);
            Sorter sortDesk3 = new Sorter(deskInfo.LuggageQueue, amsterdamQueue, barcelonaQueue, londonQueue);            

            while (true)
            {
                if (deskInfo.LuggageQueue.Count > 5)
                {
                    sortDesk1.SortByDestination();
                    Debug.WriteLine("Queue was sent from disk1 to sorter");
                }

                if (deskInfo2.LuggageQueue.Count > 5)
                {
                    sortDesk2.SortByDestination();
                    Debug.WriteLine("Queue was sent from disk2 to sorter");
                }

                if (deskInfo3.LuggageQueue.Count > 5)
                {
                    sortDesk3.SortByDestination();
                    Debug.WriteLine("Queue was sent from disk3 to sorter");
                }
                Thread.Sleep(500);
            }
        }
    }
}
