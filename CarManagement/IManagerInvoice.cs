using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement
{
    public interface IManagerInvoice
    {
        void ViewInvoice(List<Invoice> invoice);
        void ViewDetailInvoice(List<DetailInvoice> DetailInvoice);
        List<Invoice> UpdateInvoice(List<Invoice> invoice);
    }
}
