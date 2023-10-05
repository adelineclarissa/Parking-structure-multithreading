using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    public delegate void PriceCutEvent(Int32 pr);

    class PricingModel
    {

        public event PriceCutEvent priceCut;
        private static int parkingPrice = 20; // Default starting value
        
        // Getter
        public int getParkingPrice()
        {
            return parkingPrice;
        }
    
        // Check if there is a price cut, then invoke the event
        public bool checkPriceCut(Int32 p)
        {
            bool isPriceCut = false;
            //Console.WriteLine($"checkPriceCut - p, parkingPrice: {p}, {parkingPrice}");
            if(p < parkingPrice)
            {
                //Console.WriteLine("checkPriceCut - new price is less than old price");
                if(priceCut != null)
                {
                    priceCut(p);
                    //priceCut.Invoke(p);
                    isPriceCut = true;
                }
                
            }
            parkingPrice = p;
            return isPriceCut;
        }

        // Generate random price in range 10-40
        public int generatePrice()
        {
            var r = new Random();
            return r.Next(10, 41);
        }
        
        

    }
}
