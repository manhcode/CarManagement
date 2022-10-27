using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement
{
    public class Invoice
    {
        public int InvoiceID { get; set; }
        public string CustomerID { get; set; }
        public string Time { get; set; }
        public string Status { get; set; }
        public Payment payment { get; set; }
        public List<DetailInvoice> detailInvoice { get; set; }

        public Invoice()
        {
        }

        public Invoice(int invoiceID, string customerID, string time, string status, Payment payment, List<DetailInvoice> detailInvoice)
        {
            InvoiceID = invoiceID;
            CustomerID = customerID;
            Time = time;
            Status = status;
            this.payment = payment;
            this.detailInvoice = detailInvoice;
        }

        public List<DetailInvoice> getDetailInvoice()
        {
            return detailInvoice;
        }

        public void setDetailInvoice(List<DetailInvoice> detailInvoice)
        {
            this.detailInvoice = detailInvoice;
        }

        public void Price() { }


        public override string ToString()
        {
            return "{" +
               "InvoiceID=" + InvoiceID +
               ", CustomerID='" + CustomerID + '\'' +
               ", Time='" + Time + '\'' +
               ", Status='" + Status + '\'' +
               ", payment=" + payment.ToString() +
               '}';
        }
    }
}
