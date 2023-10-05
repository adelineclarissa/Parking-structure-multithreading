using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project2
{
    class MultiCellBuffer
    {
        private OrderObject[] cellBuffer;
        private object[] locks;
        private Semaphore semaphore;

        // Default constructor
        public MultiCellBuffer()
        {
            this.cellBuffer = new OrderObject[3];
            this.semaphore = new Semaphore(3, 3);
            this.locks = new object[3];
            
            // Instantiate the locks
            for(int i = 0; i < 3; i++)
            {
                locks[i] = new object();
            }
            
        }

        // Method to set an order into a cell 
        public void setCell(OrderObject order)
        {
            semaphore.WaitOne(); // lock
            for (int i = 0; i < cellBuffer.Length; i++)
            {
                lock(locks[i])
                {
                    // if the cellbuffer is empty, place our order there
                    if (cellBuffer[i] == null)
                    {
                        cellBuffer[i] = order;
                        break; // immediately release the locks
                    }
                }
                
            }
            semaphore.Release(); // release all the locks
        }

        // Method to get the order from a cell
        public OrderObject getCell()
        {
            semaphore.WaitOne();
            OrderObject order = null;
            for(int i=0; i<cellBuffer.Length; i++)
            {
                lock(locks[i])
                {
                    // find the first cell with an order and return it
                    if(cellBuffer[i] != null)
                    {
                        order = cellBuffer[i];
                        cellBuffer[i] = null; // empty the cell buffer
                        break;
                    }
                }
            }
            semaphore.Release();
            return order;
        }

    }
}
