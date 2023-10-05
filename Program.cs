using System;
using System.Threading;

namespace Project2
{
    class Program
    {
        static void Main(string[] args)
        {
            const int N = 5; // number of parking agents
            const int K = 3; // number of parking structures

            PricingModel pricingModel = new PricingModel();
            MultiCellBuffer multiCellBuffer = new MultiCellBuffer();

            // Create parking agents
            ParkingAgent[] parkingAgents = new ParkingAgent[N];
            for(int i = 0; i < N; i++)
            {
                int agentNum = i + 1;
                parkingAgents[i] = new ParkingAgent(pricingModel, multiCellBuffer, "Parking Agent#" + agentNum);

                // start thread
                Thread thread = new Thread(parkingAgents[i].generateOrderObject);
                thread.Start();
            }

            //Console.WriteLine("Parking agents created");

            // Create parking structures
            ParkingStructure[] parkingStructures = new ParkingStructure[K];
            for (int i = 0; i < K; i++)
            {
      
                int structNum = i + 1;
                parkingStructures[i] = new ParkingStructure(pricingModel, multiCellBuffer, "Parking Structure#" + structNum);

                // invoke event handler for each agent in pakingAgents
                for (int j = 0; j < parkingAgents.Length; j++)
                {
                    parkingStructures[i].priceEvent += parkingAgents[j].priceCutEventHandler;
                }

                // start thread
                Thread thread = new Thread(parkingStructures[i].start);
                thread.Start();
            }

            //Console.WriteLine("Parking structures created");


        }
    }
}
