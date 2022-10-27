using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement
{
    public class Payment
    {
        public int PaymentID { get; set; }
        public float Price { get; set; }
        public string status { get; set; }

        public Payment()
        {
        }

        public Payment(float price)
        {
            Price = price;
        }

        public Payment(int paymentID, float price, string status)
        {
            this.PaymentID = paymentID;
            this.Price = price;
            this.status = status;
        }

        public void PerformPayment()
        {
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            return "{" +
                "PaymentID=" + PaymentID +
                ", Price=" + Price +
                ", status='" + status + '\'' +
                '}';
        }
    }
}
