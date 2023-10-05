using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project2
{

    class ParkingStructure {

        // Class objects
        private PricingModel pricingModel; 
        private MultiCellBuffer multiCellBuffer;
        private OrderObject orderObject;
        private OrderProcessing orderProcessing = new OrderProcessing();
        public event PriceCutEvent priceEvent;

        // Class fields
        private string parkingStructureName;
        private int pricingEventCounter = 0; // for debugging

        // Constructor
        public ParkingStructure(PricingModel pm, MultiCellBuffer buff, string name)
        {
            this.pricingModel = pm;
            this.multiCellBuffer = buff;
            this.parkingStructureName = name;
            //this.pricingModel.priceCut += this.priceEvent;
        }
        

        // Start a thread
        public void start()
        {
            // make sure that event is notified
            this.pricingModel.priceCut += this.priceEvent;
            
            // counter t = 10
            for (int t = 0; t < 10; t++)
            {
                // generate new price using the pricing model
                int nPrice = pricingModel.generatePrice();

                // price change
                priceChangeEvent(nPrice);

                orderObject = multiCellBuffer.getCell();
                if (orderObject != null)
                {
                    Thread orderProcessThread = new Thread(() => orderProcessing.processOneOrder(orderObject));
                    orderProcessThread.Start(); // asynchronous processing of orders
                }
                


            }
           
              
        }

        public void priceChangeEvent(int nPrice)
        {
            //Console.WriteLine($"priceChangeEvent - nPrice: {nPrice}");
            // check if there is a price cut
            if (pricingModel.checkPriceCut(nPrice))
            {
                pricingEventCounter++; // for debugging
                Console.WriteLine($"[EVENT #{pricingEventCounter}] {this.parkingStructureName} has a price cut event! The new price is ${nPrice}");

            }
            else // if there is a price increase
            {
                pricingEventCounter++;
                Console.WriteLine($"[EVENT #{pricingEventCounter}] {this.parkingStructureName} has a price change! The new price is ${nPrice}");
            }
        }




    }
}
