using CarManagement;
using System;
using System.Collections.Generic;

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

        static void Main(string[] args)
        {
            PrintHeader("Car Management System");
            ViewSystem();
        }

        private static void PrintHeader(String header)
        {
            for (int i = 0; i < 15; i++)
            {
                Console.Write("-");
            }
            Console.Write(header);
            for (int i = 0; i < 15; i++)
            {
                Console.Write("-");
            }
            Console.WriteLine();
        }

        private static int GetLastUserID()
        {
            if (users.Count == 0)
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
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Quit");

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
                            User us = currentUser.logIn(users);

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
                    IDictionary<String, Object> dict;

                    if (currentUser.GetType().Name.CompareTo("Manager") == 0)
                    {
                        dict = currentUser.ViewSystem(cars, invoices, detailInvoices);

                        if (dict.ContainsKey("ManageCar"))
                        {
                            cars = (List<Car>)dict["ManageCar"];
                        }

                        if (dict.ContainsKey("ManageInvoice"))
                        {
                            invoices = (List<Invoice>)dict["ManageInvoice"];
                        }

                        if (dict.ContainsKey("Quit"))
                        {
                            flag = false;
                        }
                    }
                    else if (currentUser.GetType().Name.CompareTo("Customer") == 0)
                    {
                        dict = currentUser.ViewSystem(cars, invoices, detailInvoices, payments);

                        if (dict.ContainsKey("Cars"))
                        {
                            cars = (List<Car>)dict["Cars"];
                        }

                        if (dict.ContainsKey("Invoices"))
                        {
                            invoices = (List<Invoice>)dict["Invoices"];
                        }

                        if (dict.ContainsKey("DetailInvoices"))
                        {
                            detailInvoices = (List<DetailInvoice>)dict["DetailInvoices"];
                        }

                        if (dict.ContainsKey("Payments"))
                        {
                            payments = (List<Payment>)dict["Payments"];
                        }

                        if (dict.ContainsKey("Quit"))
                        {
                            flag = false;
                        }
                    }
                } while (flag);

                flag = true;
            } while (flag);
        }
    }
}
