using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement
{

    public class Customer : User
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<DetailInvoice> Cart { get; set; }
        public List<Invoice> Invoice { get; set; }

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

        public override IDictionary<String, Object> ViewSystem(List<Car> Cars, List<Invoice> Invoices, List<DetailInvoice> DetailInvoices, List<Payment> Payments)
        {
            bool flag = false;
            IDictionary<String, Object> dict = new Dictionary<String, Object>();

            Console.WriteLine("1. View Car");
            Console.WriteLine("2. View Cart");
            Console.WriteLine("3. Add car to Cart");
            Console.WriteLine("4. Delete car from Cart");
            Console.WriteLine("5. Discharge");
            Console.WriteLine("6. Quit");

            while (!flag)
            {
                Console.Write("Your choice: ");
                int choice = Int32.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        this.ViewCar(Cars);
                        break;
                    case 2:
                        this.ViewCart();
                        break;
                    case 3:
                        this.AddCarToCart(Cars, DetailInvoices);
                        break;
                    case 4:
                        this.DeleteCarFromCart();
                        break;
                    case 5:
                        dict = this.Discharge(Cars, Invoices, DetailInvoices, Payments);
                        break;
                    case 6:
                        flag = true;
                        dict.Add("Quit", true);
                        break;
                    default:
                        Console.WriteLine("Your choice must be in range [1, 4]");
                        break;
                }
            }

            return dict;
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

        public void AddCarToCart(List<Car> Cars, List<DetailInvoice> DetailInvoices)
        {
            bool flag = true;

            do
            {
                Car car = new Car();
                do
                {
                    car = FindCar(Cars);
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

                if (DetailInvoices.Count == 0)
                {
                    detailInvoice = new DetailInvoice(1);
                }
                else
                {
                    detailInvoice = new DetailInvoice(DetailInvoices.Count + 1);
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

        public Car FindCar(List<Car> Cars)
        {
            Console.Write("Enter carId to add to cart: ");

            int carID = Int32.Parse(Console.ReadLine());

            if (Cars.FirstOrDefault(c => c.carID == carID) == null)
            {
                Console.WriteLine("Car is not existed");
                return null;
            }

            return Cars.FirstOrDefault(c => c.carID == carID);
        }

        public IDictionary<String, Object> Discharge(List<Car> Cars, List<Invoice> Invoices, List<DetailInvoice> DetailInvoices, List<Payment> Payments)
        {
            IDictionary<String, Object> dict = new Dictionary<String, Object>();
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

                if (Payments.Count == 0)
                {
                    payment = new Payment(1, price, "Completed");
                }
                else
                {
                    payment = new Payment(Payments.Count + 1, price, "Completed");
                }

                Payments.Add(payment);

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

            if (Invoices.Count == 0)
            {
                this.Invoice.Add(new Invoice(1, this.userID.ToString(), DateTime.Now.ToString(),
                        "Pending", payment, this.Cart));
            }
            else
            {
                this.Invoice.Add(new Invoice(Invoices.Count + 1, this.userID.ToString(), DateTime.Now.ToString(),
                        "Pending", payment, this.Cart));
            }

            Invoices.Add(this.Invoice[Invoice.Count - 1]);

            foreach (var c in this.Cart)
            {
                if (DetailInvoices.Count == 0)
                {
                    c.DetailInvoiceID = 1;
                }
                else
                {
                    c.DetailInvoiceID = DetailInvoices.Count + 1;
                }

                DetailInvoices.Add(c);
            }

            foreach (var cart in this.Cart)
            {
                Cars[Cars.FindIndex(c => c.carID == cart.Car.carID)].quantity -= cart.quantity;
            }

            dict.Add("Cars", Cars);
            dict.Add("Invoices", Invoices);
            dict.Add("DetailInvoices", DetailInvoices);
            dict.Add("Payments", Payments);

            this.Cart = new List<DetailInvoice>();

            return dict;
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
