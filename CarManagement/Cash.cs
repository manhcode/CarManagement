using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement
{
    public class Cash : Payment
    {

        public Cash(int paymentID, float price, string status) : base()
        {
            this.PaymentID = paymentID;
            this.Price = price;
            this.status = status;
        }


        public void PerformPayment()
        {
            base.PerformPayment();
        }
    }
}
