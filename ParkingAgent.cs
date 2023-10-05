using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Project2
{
    /**
     * ParkingAgent is a consumer/subscriber that receives notification when a price cut event is invoked
     * by a parking structure, then generate an order and place it in the buffer
     */
    class ParkingAgent
    {
        // Class objects
        private PricingModel pricingModel;
        private MultiCellBuffer multiCellBuffer;
        private Random r; 

        // Class fields
        private string agentName;
        private Thread thread;
        private int priceAfterEvent;

        public int getPriceAfterEvent()
        {
            return priceAfterEvent;
        }

        // Constructor
        public ParkingAgent(PricingModel pricingModel, MultiCellBuffer buffer, string name)
        {
            this.pricingModel = pricingModel;
            this.multiCellBuffer = buffer;
            this.agentName = name;
            thread = new Thread(generateOrderObject);
        }

        // Event handler
        public void priceCutEventHandler(int newPrice)
        {
            this.priceAfterEvent = newPrice;

            // calculate the number of parking spaces to order
            r = new Random();
            int parkingSpace = r.Next(1, 6);
            int cardNum = r.Next(5000, 7001);
            double unitPrice = newPrice * 1.0;

            // create a new order
            OrderObject order = new OrderObject(agentName, cardNum, parkingSpace, unitPrice);

            // store it in the buffer
            multiCellBuffer.setCell(order);

        }

        // Generate orders when there is no price cut event
        public void generateOrderObject()
        {
            r = new Random();
            
            for (int i = 0; i < 3; i++)
            {
                int parkingSpace = r.Next(1, 6);
                int cardNum = r.Next(5000, 7001);
                double unitPrice = pricingModel.getParkingPrice();

                // create a new order
                OrderObject order = new OrderObject(agentName, cardNum, parkingSpace, unitPrice);

                // store it in the buffer
                multiCellBuffer.setCell(order);
            }
        }

    }
}
