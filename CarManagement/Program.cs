using CarManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarManagement
{
    class Program
    {
        private static List<User> users = new List<User>();
        private static User currentUser = new User();
        private static List<Car> cars = new List<Car>();
        private static List<Invoice> invoices = new List<Invoice>();
        private static List<DetailInvoice> detailInvoices = new List<DetailInvoice>();
        private static List<Payment> payments = new List<Payment>();

        public static List<User> Users
        {
            get { return users; }
            set
            {
                users = Users;
            }
        }

        public static User CurrentUser
        {
            get { return currentUser; }
            set
            {
                currentUser = CurrentUser;
            }
        }

        public static List<Car> Cars
        {
            get { return cars; }
            set
            {
                cars = Cars;
            }
        }

        public static List<Invoice> Invoices
        {
            get { return invoices; }
            set
            {
                invoices = Invoices;
            }
        }

        public static List<DetailInvoice> DetailInvoices
        {
            get { return detailInvoices; }
            set
            {
                detailInvoices = DetailInvoices;
            }
        }

        public static List<Payment> Payments
        {
            get { return payments; }
            set
            {
                payments = Payments;
            }
        }

        private Program()
        {
        }

        static void Main(string[] args)
        {
            ViewSystem();
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

        private static int GetLastUserID()
        {
            if (Program.users.Count == 0)
                return 0;
            return users[users.Count - 1].GetUserId();
        }

        private static void Register()
        {
            currentUser = new User(GetLastUserID() + 1);
            currentUser.register();

            Console.Write("You are manager (1) or customer (2): ");
            int role = Int32.Parse(Console.ReadLine());

            if (role == 1)
            {
                currentUser = new Manager(currentUser.GetUserId(), currentUser.GetName(), currentUser.GetPassword());
            }
            else
            {
                currentUser = new Customer(currentUser.GetUserId(), currentUser.GetName(), currentUser.GetPassword());
            }

            currentUser.register();

            users.Add(currentUser);
        }

        private static void ViewSystem()
        {
            bool flag = true;

            do
            {
                PrintMenu("Car Management System", new List<string> { "Register", "Login", "Quit" });

                do
                {
                    Console.Write("Your choice: ");

                    int choice = Int32.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Register();
                            flag = false;
                            break;
                        case 2:
                            User us = currentUser.logIn();

                            if (us == null)
                            {
                                Console.WriteLine("Wrong credentials");
                            }
                            else
                            {
                                currentUser = us;
                                flag = false;
                            }
                            break;
                        case 3:
                            System.Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Your choice must be in range [1, 2]");
                            break;

                    }
                } while (flag);

                flag = true;

                do
                {
                    flag = !currentUser.ViewSystem();
                } while (flag);

                flag = true;
            } while (flag);
        }
    }
}
