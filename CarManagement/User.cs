using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarManagement
{
    public class User
    {
        protected int userID { get; set; }
        protected string name { get; set; }
        protected string password { get; set; }

        public User()
        {
        }

        public User(int userID)
        {
            this.userID = userID;
        }

        public User(string name, string password)
        {
            this.name = name;
            this.password = password;
        }

        public User(int userID, string name, string password)
        {
            this.userID = userID;
            this.name = name;
            this.password = password;
        }

        public int GetUserId()
        {
            return this.userID;
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetPassword()
        {
            return this.password;
        }

        public virtual void register()
        {
            Console.Write("Enter your name: ");
            this.name = Console.ReadLine();
            Console.Write("Enter your password: ");
            this.password = Console.ReadLine();
        }

        public User logIn()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your password: ");
            string password = Console.ReadLine();

            return Program.Users.FirstOrDefault(u => u.name.CompareTo(name) == 0 && u.password.CompareTo(password) == 0);
        }

        public virtual bool ViewSystem()
        {
            return false;
        }


    }
}
