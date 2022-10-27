namespace CarManagement
{
    public class Car
    {
        public int carID { get; set; }
        public string carName { get; set; }
        public float carPrice { get; set; }
        public int quantity { get; set; }

        public Car()
        {
        }

        public Car(int carID)
        {
            this.carID = carID;
        }

        public Car(int carID, string carName, float carPrice, int quantity)
        {
            this.carID = carID;
            this.carName = carName;
            this.carPrice = carPrice;
            this.quantity = quantity;
        }

        public bool AvailableCar()
        {
            return true;
        }

        public override string ToString()
        {
            return "{" +
                    "carID=" + carID +
                    ", carName='" + carName + '\'' +
                    ", carPrice=" + carPrice +
                    ", quantity=" + quantity +
                    '}';
        }
    }
}