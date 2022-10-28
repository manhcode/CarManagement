using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement
{
    public class DetailInvoice
    {
        private int detailInvoiceID;
        public Car Car { get; set; }
        public int quantity { get; set; }

        public int DetailInvoiceID
        {
            get { return this.detailInvoiceID; }
            set
            {
                this.detailInvoiceID = DetailInvoiceID;
            }
        }

        public DetailInvoice(int detailInvoiceID)
        {
            this.detailInvoiceID = detailInvoiceID;
        }

        public DetailInvoice(Car car, int quantity)
        {
            this.Car = car;
            this.quantity = quantity;
        }

        public void Price()
        {

        }

        public override string ToString()
        {
            return "{" +
                "Car={carID=" + Car.carID + ", carName=" + Car.carName + ", carPrice=" + Car.carPrice + "}" +
                ", quantity=" + quantity +
                '}';
        }
    }
}
