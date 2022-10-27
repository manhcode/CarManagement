using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement
{
    public class Banking : Payment
    {

        public Banking(int paymentID, float price, string status) : base()
        {
            this.PaymentID = paymentID;
            this.Price = price;
            this.status = status;
        }

        public new void PerformPayment()
        {
            base.PerformPayment();
        }

    }
}
