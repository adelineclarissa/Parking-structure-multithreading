using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2
{
    class OrderObject
    {
        // Private class fields
        private int cardNo;
        private string senderId;
        private string receiverId;
        private int quantity;
        private double unitPrice;

        public OrderObject(string sid, int card, int quan, double pr)
        {
            this.senderId = sid;
            this.cardNo = card;
            this.quantity = quan;
            this.unitPrice = pr;
        }

        // Public function to get and set the private fields
        public string SenderId
        {
            get { return senderId; }
            set { senderId = value; }
        }

        public int CardNumber
        {
            get { return cardNo; }
            set { cardNo = value; }
        }

        public string ReceiverId
        {
            get { return receiverId; }
            set { receiverId = value; }
        }

        public int ParkingSpaceQuantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

        public double UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value;  }
        }

    }
}
