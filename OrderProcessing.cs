using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class OrderProcessing
    {
        public void processOneOrder(OrderObject order)
        {
            // Debug
            if (order == null)
            {
                System.Environment.Exit(12345);
            }
            //Console.WriteLine("Processing order number: " + order.SenderId);

            // Check if credit card number is valid
            if (order.CardNumber <= 7000 && order.CardNumber >= 5000)
            {
                Random rand = new Random();

                // Calculate amount of charge
                double unitPrice = order.UnitPrice;
                int quantity = order.ParkingSpaceQuantity;
                double tax = rand.Next(8, 13);
                tax /= 100.0;
                double locCharge = rand.Next(2, 9);
                double totalAmt = (unitPrice * quantity) + (unitPrice * tax * locCharge);
                orderConfirmation(order, totalAmt); // print the order confirmation
            }
            else
            {
                // Invalid credit card handler
                Console.WriteLine($"Credit card number is invalid for ID: {order.SenderId}");

            }
        }

        private void orderConfirmation(OrderObject order, double amt)
        {
            if (order != null) // Waits until the order information is completed
            {
                
                Console.WriteLine("*****************************************\n" +
                    "Order confirmed! Here's your oder detail\n" +
                    $"Parking space quantity: {order.ParkingSpaceQuantity}\n" +
                    $"Total amount          : ${amt:F2}\n" +
                    $"Sender id             : {order.SenderId}\n" +
                    $"Card Number           : {order.CardNumber}\n" +
                    "****************************************");


                /*
                Console.WriteLine("*****************************************");
                Console.WriteLine("Order confirmed! Here's your oder detail\n");
                Console.WriteLine($"Parking space quantity: {order.ParkingSpaceQuantity}\n");
                Console.WriteLine($"Total amount          : ${amt:F2}\n");
                Console.WriteLine($"Sender id             : {order.SenderId}\n");
                Console.WriteLine($"Card Number           : {order.CardNumber}");
                Console.WriteLine("*****************************************");
                */
            }
        }

    }
}
