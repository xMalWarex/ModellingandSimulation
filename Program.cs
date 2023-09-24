﻿using System;
using MathNet.Numerics.Distributions;

 

class Simulation

{

    static void Main(string[] args)

    {

        int MaxSimTime = 360; // Total simulation time in minutes

        int Time = 0;

 

        // Create a customer queue

        CustomerQueue customerQueue = new CustomerQueue();

        Random rand = new Random();

 

        double lambda = 1.0 / 4.0; // Adjust this value for the desired arrival rate (1 customer per 4 minutes)

        double avgServiceTime = 4.0; // Adjust this for the desired average service time

        int maxQueueSize = 8; // Maximum queue size

 

        int timeCustomerArrival = (int)Exponential.Sample(lambda);

        bool printserverFree = true;

        double printserviceTime = Normal.Sample(avgServiceTime, 5.0);

 

        int totalCustomersArrived = 0; // Track total customers arrived

        int totalCustomersServed = 0; // Track total customers served

 

        // Simulation time loop

        while (true)

        {

            // Check if the shop is closed at 360 mins and the queue is empty

            if (Time >= MaxSimTime && customerQueue.NumCustomers() == 0)

            {

                break;

            }

 

            // Possible events:

            // 1) Customer arrives and enters the queue (if queue size is below the limit)

            // 2) Customer leaves the queue, server starts processing, server is busy

            // 3) Server finishes processing; server becomes free (using a Gaussian distribution)

 

            if (timeCustomerArrival <= 0 && Time < MaxSimTime && customerQueue.NumCustomers() < maxQueueSize)

            {

                // A customer arrives and joins the queue

                Customer customer = new Customer();

                customerQueue.Push(customer);

 

                // Calculate time until the next customer arrival using an exponential distribution

                timeCustomerArrival = (int)Exponential.Sample(lambda);

                totalCustomersArrived++;

            }

 

            if (printserverFree && customerQueue.NumCustomers() > 0)

            {

                // The server is free, and there are customers in the queue

                Customer toPrint = customerQueue.Pop();

 

                // Calculate time needed to serve the customer using a normal distribution

                printserviceTime = Normal.Sample(avgServiceTime, 5.0);

                printserverFree = false;

                totalCustomersServed++;

            }

            else if (!printserverFree)

            {

                // The server is busy serving a customer

                printserviceTime -= 1.0;

 

                if (printserviceTime <= 0)

                {

                    // The server finished serving the customer and becomes free

                    printserverFree = true;

                }

            }

 

            // Output the current queue size

            Console.WriteLine("Time: " + Time + " Queue size: " + customerQueue.NumCustomers());

 

            // Decrement the time until the next customer arrival

            timeCustomerArrival--;

            Time++;

        }


        // Output the total number of customers served during the simulation

        Console.WriteLine("Total Customers Served: " + totalCustomersServed);

    }

}