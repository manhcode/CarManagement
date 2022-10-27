using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement
{

    public class Manager : User, IManagerCar, IManagerInvoice
    {
        private string Phone { get; set; }
        private string Email { get; set; }
        private string Address { get; set; }

        public Manager(int userID, string name, string password) : base()
        {
            this.userID = userID;
            this.name = name;
            this.password = password;
        }

        public Manager(String phone, String email, String address) : base()
        {
            Phone = phone;
            Email = email;
            Address = address;
        }

        public override IDictionary<String, Object> ViewSystem(List<Car> Cars, List<Invoice> Invoices, List<DetailInvoice> DetailInvoices)
        {
            bool flag = false;
            IDictionary<String, Object> dict = new Dictionary<String, Object>();

            do
            {
                Console.WriteLine("1. Manage Car");
                Console.WriteLine("2. Manage Invoice");
                Console.WriteLine("3. Quit");

                Console.Write("Your choice: ");
                int choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("1. Add car");
                        Console.WriteLine("2. Delete car");
                        Console.WriteLine("3. View car");
                        Console.WriteLine("4. Update car");
                        Console.WriteLine("5. Back");

                        while (!flag)
                        {
                            Console.Write("Your choice: ");
                            choice = Int32.Parse(Console.ReadLine());

                            switch (choice)
                            {
                                case 1:
                                    if (!dict.ContainsKey("ManageCar"))
                                    {
                                        dict.Add("ManageCar", this.AddCar(Cars));
                                    }
                                    else
                                    {
                                        dict["ManageCar"] = this.AddCar(Cars);
                                    }
                                    Cars = (List<Car>)dict["ManageCar"];
                                    break;
                                case 2:
                                    if (!dict.ContainsKey("ManageCar"))
                                    {
                                        dict.Add("ManageCar", this.DeleteCar(Cars));
                                    }
                                    else
                                    {
                                        dict["ManageCar"] = this.DeleteCar(Cars);
                                    }
                                    Cars = (List<Car>)dict["ManageCar"];
                                    break;
                                case 3:
                                    this.ViewCar(Cars);
                                    break;
                                case 4:
                                    if (!dict.ContainsKey("ManageCar"))
                                    {
                                        dict.Add("ManageCar", this.UpdateCar(Cars));
                                    }
                                    else
                                    {
                                        dict["ManageCar"] = this.UpdateCar(Cars);
                                    }
                                    Cars = (List<Car>)dict["ManageCar"];
                                    break;
                                case 5:
                                    flag = true;
                                    break;
                                default:
                                    Console.WriteLine("Your choice must be in range [1, 4]");
                                    break;
                            }
                        }
                        flag = false;
                        break;
                    case 2:
                        Console.WriteLine("1. View invoice");
                        Console.WriteLine("2. View detail invoice");
                        Console.WriteLine("3. Update invoice");
                        Console.WriteLine("4. Back");
                        while (!flag)
                        {
                            Console.Write("Your choice: ");
                            choice = Int32.Parse(Console.ReadLine());

                            switch (choice)
                            {
                                case 1:
                                    this.ViewInvoice(Invoices);
                                    break;
                                case 2:
                                    this.ViewDetailInvoice(DetailInvoices);
                                    break;
                                case 3:
                                    dict.Add("ManageInvoice", this.UpdateInvoice(Invoices));
                                    Invoices = (List<Invoice>)dict["ManageInvoice"];
                                    break;
                                case 4:
                                    flag = true;
                                    break;
                                default:
                                    Console.WriteLine("Your choice must be in range [1, 3]");
                                    break;
                            }
                        }
                        flag = false;
                        break;
                    case 3:
                        dict.Add("Quit", true);
                        flag = true;
                        break;
                    default:
                        Console.WriteLine("Your choice must be in range [1, 2]");
                        break;
                }
            } while (!flag);

            return dict;
        }

        public List<Car> AddCar(List<Car> Cars)
        {
            Car car;

            if (Cars.Count == 0)
            {
                car = new Car(1);
            }
            else
            {
                car = new Car(Cars[Cars.Count - 1].carID + 1);
            }

            Console.Write("Enter car name: ");
            car.carName = Console.ReadLine();

            Console.Write("Enter car price: ");
            car.carPrice = float.Parse(Console.ReadLine());

            Console.Write("Enter car quantity: ");
            car.quantity = Int32.Parse(Console.ReadLine());

            Cars.Add(car);

            return Cars;
        }

        public List<Car> DeleteCar(List<Car> Cars)
        {
            Console.Write("Enter carId to delete: ");

            int carID = Int32.Parse(Console.ReadLine());

            if (Cars.FirstOrDefault(c => c.carID == carID) == null)
            {
                Console.WriteLine("Car is not existed");
                return Cars;
            }

            return Cars.FindAll(c => c.carID != carID);
        }

        public List<Car> UpdateCar(List<Car> Cars)
        {
            Console.Write("Enter carId to update: ");

            int carID = Int32.Parse(Console.ReadLine());

            if (Cars.FirstOrDefault(c => c.carID == carID) == null)
            {
                Console.WriteLine("Car is not existed");
                return Cars;
            }

            int index = Cars.FindIndex(c => c.carID == carID);

            Console.Write("Enter car name: ");
            Cars[index].carName = Console.ReadLine();

            Console.Write("Enter car price: ");
            Cars[index].carPrice = float.Parse(Console.ReadLine());

            Console.Write("Enter car quantity: ");
            Cars[index].quantity = Int32.Parse(Console.ReadLine());

            return Cars;
        }

        public List<Invoice> UpdateInvoice(List<Invoice> invoice)
        {
            Console.Write("Enter invoiceId to update: ");

            int invoiceId = Int32.Parse(Console.ReadLine());

            if (invoice.FirstOrDefault(i => i.InvoiceID == invoiceId) == null)
            {
                Console.WriteLine("Invoice is not existed");
                return null;
            }

            int index = invoice.FindIndex(i => i.InvoiceID == invoiceId);

            Console.Write("Enter time: ");
            invoice[index].Time = Console.ReadLine();

            Console.Write("Enter car price: ");
            invoice[index].Status = Console.ReadLine();

            return invoice;
        }

        public void ViewCar(List<Car> Cars)
        {
            Console.Write("Enter carId to view: ");

            int carID = Int32.Parse(Console.ReadLine());

            if (Cars.FirstOrDefault(c => c.carID == carID) == null)
            {
                Console.WriteLine("Car is not existed");
                return;
            }

            Console.WriteLine(Cars.FirstOrDefault(c => c.carID == carID).ToString());
        }

        public void ViewDetailInvoice(List<DetailInvoice> DetailInvoice)
        {
            Console.Write("Enter detailInvoiceId to view: ");

            int detailInvoiceId = Int32.Parse(Console.ReadLine());

            if (DetailInvoice.FirstOrDefault(di => di.DetailInvoiceID == detailInvoiceId) == null)
            {
                Console.WriteLine("DetailInvoice is not existed");
                return;
            }

            Console.WriteLine(DetailInvoice.FirstOrDefault(di => di.DetailInvoiceID == detailInvoiceId).ToString());
        }

        public void ViewInvoice(List<Invoice> invoice)
        {
            Console.Write("Enter invoiceId to view: ");

            int invoiceId = Int32.Parse(Console.ReadLine());

            if (invoice.FirstOrDefault(i => i.InvoiceID == invoiceId) == null)
            {
                Console.WriteLine("Invoice is not existed");
                return;
            }

            Console.WriteLine(invoice.FirstOrDefault(i => i.InvoiceID == invoiceId).ToString());
        }

        public override void register()
        {
            Console.Write("Enter your phone: ");
            this.Phone = Console.ReadLine();
            Console.Write("Enter your email: ");
            this.Email = Console.ReadLine();
            Console.Write("Enter your address: ");
            this.Address = Console.ReadLine();
        }
    }
}
