using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement
{

    public class Customer : User
    {
        private string Phone { get; set; }
        private string Email { get; set; }
        private string Address { get; set; }
        private List<DetailInvoice> Cart { get; set; }
        private List<Invoice> Invoice { get; set; }

        public Customer(int userID, string name, string password) : base()
        {
            this.userID = userID;
            this.name = name;
            this.password = password;
            this.Cart = new List<DetailInvoice>();
            this.Invoice = new List<Invoice>();
        }
        public Customer(String phone, String email, String address) : base()
        {
            Phone = phone;
            Email = email;
            Address = address;
            this.Cart = new List<DetailInvoice>();
            this.Invoice = new List<Invoice>();
        }

        public override bool ViewSystem()
        {
            bool flag = false;

            PrintMenu("Car Management System", new List<string> {
                "View Car", "View Cart", "Add car to Cart", "Delete car from Cart", "Discharge", "Quit"
            });

            while (!flag)
            {
                Console.Write("Your choice: ");
                int choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        this.ViewCar();
                        break;
                    case 2:
                        this.ViewCart();
                        break;
                    case 3:
                        this.AddCarToCart();
                        break;
                    case 4:
                        this.DeleteCarFromCart();
                        break;
                    case 5:
                        this.Discharge();
                        break;
                    case 6:
                        return true;
                    default:
                        Console.WriteLine("Your choice must be in range [1, 4]");
                        break;
                }
            }
            return true;
        }

        public void ViewCar()
        {
            foreach (var car in Program.Cars)
            {
                Console.WriteLine(car.ToString());
            }
        }

        public void AddCarToCart()
        {
            bool flag = true;

            do
            {
                Car car = new Car();
                do
                {
                    car = FindCar();
                    if (car != null)
                    {
                        flag = false;
                    }
                    else
                    {
                        Console.WriteLine("Please enter another carId");
                    }
                } while (flag);

                flag = true;

                Console.Write("Enter quantity: ");
                int quantity = Int32.Parse(Console.ReadLine());

                DetailInvoice detailInvoice;

                if (Program.DetailInvoices.Count == 0)
                {
                    detailInvoice = new DetailInvoice(1);
                }
                else
                {
                    detailInvoice = new DetailInvoice(Program.DetailInvoices.Count + 1);
                }

                detailInvoice.Car = car;
                detailInvoice.quantity = quantity;

                this.Cart.Add(detailInvoice);

                Console.Write("Do you want to buy more? (Y or N): ");
                string choice = Console.ReadLine();

                if (choice.ToUpper().CompareTo("N") == 0)
                {
                    flag = false;
                }
            } while (flag);
        }

        public void DeleteCarFromCart()
        {
            Console.Write("Enter carId to delete: ");

            int carID = Int32.Parse(Console.ReadLine());

            if (this.Cart.FirstOrDefault(c => c.Car.carID == carID) == null)
            {
                Console.WriteLine("Car is not existed");
                return;
            }

            this.Cart = this.Cart.Where(c => c.Car.carID != carID).ToList();
            Console.WriteLine("Car is deleted from Cart");
        }

        public void ViewCart()
        {
            if (this.Cart.Count == 0)
            {
                Console.WriteLine("Cart is empty");
                return;
            }

            foreach (DetailInvoice detailInvoice in this.Cart)
            {
                Console.WriteLine(detailInvoice.ToString());
            }
        }

        public Car FindCar()
        {
            Console.Write("Enter carId to add to cart: ");

            int carID = Int32.Parse(Console.ReadLine());

            if (Program.Cars.FirstOrDefault(c => c.carID == carID) == null)
            {
                Console.WriteLine("Car is not existed");
                return null;
            }

            return Program.Cars.FirstOrDefault(c => c.carID == carID);
        }

        public void Discharge()
        {
            bool flag = true;
            Payment payment;

            Console.WriteLine("Choose payment method: ");
            Console.WriteLine("1. Banking");
            Console.WriteLine("2. Cash");

            do
            {
                Console.Write("Your choice: ");
                int ch = Int32.Parse(Console.ReadLine());

                float price = this.Cart.Sum(c => c.Car.carPrice * c.quantity);

                if (Program.Payments.Count == 0)
                {
                    payment = new Payment(1, price, "Completed");
                }
                else
                {
                    payment = new Payment(Program.Payments.Count + 1, price, "Completed");
                }

                Program.Payments.Add(payment);

                switch (ch)
                {
                    case 1:
                        //Tính đa hình
                        payment = new Banking(payment.PaymentID, payment.Price, payment.status);
                        flag = false;
                        break;
                    case 2:
                        //Tính đa hình
                        payment = new Cash(payment.PaymentID, payment.Price, payment.status);
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Your choice must be in range [1, 2]");
                        break;
                }
            } while (flag);

            if (Program.Invoices.Count == 0)
            {
                this.Invoice.Add(new Invoice(1, this.userID.ToString(), DateTime.Now.ToString(),
                        "Pending", payment, this.Cart));
            }
            else
            {
                this.Invoice.Add(new Invoice(Program.Invoices.Count + 1, this.userID.ToString(), DateTime.Now.ToString(),
                        "Pending", payment, this.Cart));
            }

            Program.Invoices.Add(this.Invoice[Invoice.Count - 1]);

            foreach (var c in this.Cart)
            {
                if (Program.DetailInvoices.Count == 0)
                {
                    c.DetailInvoiceID = 1;
                }
                else
                {
                    c.DetailInvoiceID = Program.DetailInvoices.Count + 1;
                }

                Program.DetailInvoices.Add(c);
            }

            foreach (var cart in this.Cart)
            {
                Program.Cars[Program.Cars.FindIndex(c => c.carID == cart.Car.carID)].quantity -= cart.quantity;
            }

            this.Cart = new List<DetailInvoice>();
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
