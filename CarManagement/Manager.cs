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

        public override bool ViewSystem()
        {
            bool flag = false;

            do
            {
                PrintMenu("Car Management System", new List<string> {
                    "Manage Car", "Manage Invoice", "Quit"
                });

                Console.Write("Your choice: ");
                int choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        PrintMenu("Car Management System", new List<string> {
                            "Add car", "Delete car", "View car", "Update car", "Back"
                        });

                        while (!flag)
                        {
                            Console.Write("Your choice: ");
                            choice = Int32.Parse(Console.ReadLine());

                            switch (choice)
                            {
                                case 1:
                                    this.AddCar();
                                    break;
                                case 2:
                                    this.DeleteCar();
                                    break;
                                case 3:
                                    this.ViewCar();
                                    break;
                                case 4:
                                    this.UpdateCar();
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
                        PrintMenu("Car Management System", new List<string> {
                            "View invoice", "View detail invoice", "Update invoice", "Back"
                        });

                        while (!flag)
                        {
                            Console.Write("Your choice: ");
                            choice = Int32.Parse(Console.ReadLine());

                            switch (choice)
                            {
                                case 1:
                                    this.ViewInvoice();
                                    break;
                                case 2:
                                    this.ViewDetailInvoice();
                                    break;
                                case 3:
                                    this.UpdateInvoice();
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
                        return true;
                    default:
                        Console.WriteLine("Your choice must be in range [1, 2]");
                        break;
                }
            } while (!flag);

            return true;
        }

        public void AddCar()
        {
            Car car;

            if (Program.Cars.Count == 0)
            {
                car = new Car(1);
            }
            else
            {
                car = new Car(Program.Cars[Program.Cars.Count - 1].carID + 1);
            }

            Console.Write("Enter car name: ");
            car.carName = Console.ReadLine();

            Console.Write("Enter car price: ");
            car.carPrice = float.Parse(Console.ReadLine());

            Console.Write("Enter car quantity: ");
            car.quantity = Int32.Parse(Console.ReadLine());

            Program.Cars.Add(car);
        }


        public void DeleteCar()
        {
            Console.Write("Enter carId to delete: ");

            int carID = Int32.Parse(Console.ReadLine());

            if (Program.Cars.FirstOrDefault(c => c.carID == carID) == null)
            {
                Console.WriteLine("Car is not existed");
                return;
            }

            Program.Cars.Remove(Program.Cars.FirstOrDefault(c => c.carID == carID));
        }

        public void UpdateCar()
        {
            Console.Write("Enter carId to update: ");

            int carID = Int32.Parse(Console.ReadLine());

            if (Program.Cars.FirstOrDefault(c => c.carID == carID) == null)
            {
                Console.WriteLine("Car is not existed");
                return;
            }

            int index = Program.Cars.FindIndex(c => c.carID == carID);

            Console.Write("Enter car name: ");
            Program.Cars[index].carName = Console.ReadLine();

            Console.Write("Enter car price: ");
            Program.Cars[index].carPrice = float.Parse(Console.ReadLine());

            Console.Write("Enter car quantity: ");
            Program.Cars[index].quantity = Int32.Parse(Console.ReadLine());
        }

        public void UpdateInvoice()
        {
            Console.Write("Enter invoiceId to update: ");

            int invoiceId = Int32.Parse(Console.ReadLine());

            if (Program.Invoices.FirstOrDefault(i => i.InvoiceID == invoiceId) == null)
            {
                Console.WriteLine("Invoice is not existed");
                return;
            }

            int index = Program.Invoices.FindIndex(i => i.InvoiceID == invoiceId);

            Console.Write("Enter time: ");
            Program.Invoices[index].Time = Console.ReadLine();

            Console.Write("Enter status: ");
            Program.Invoices[index].Status = Console.ReadLine();
        }

        public void ViewCar()
        {
            foreach (var car in Program.Cars)
            {
                Console.WriteLine(car.ToString());
            }
        }

        public void ViewDetailInvoice()
        {
            foreach (var detailInvoice in Program.DetailInvoices)
            {
                Console.WriteLine(detailInvoice.ToString());
            }
        }

        public void ViewInvoice()
        {
            foreach (var i in Program.Invoices)
            {
                Console.WriteLine(i.ToString());
            }
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

        private static void PrintMenu(String header, List<string> choices)
        {
            Console.Write(new String(' ', 10));
            Console.Write(new string('-', 23));
            Console.Write(String.Format("{0,21}", header));
            Console.Write(new string('-', 23));
            Console.WriteLine();
            foreach (var c in choices.Select((value, index) => new { index, value }))
            {
                Console.Write(new String(' ', 10));
                Console.Write(String.Format("{0,-31}", $"| {c.index + 1}. {c.value}"));
                Console.WriteLine(String.Format("{0,34} |", " "));
            }
            Console.Write(new String(' ', 10));
            Console.WriteLine(new String('-', 67));
        }
    }
}
