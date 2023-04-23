
namespace Checkout_System
{
    internal class Product
    {
        public enum PriceTypes { PricePerUnit, PricePerKG}

        public int ID;
        public double Price;
        public float Weight;
        public PriceTypes PriceType;
        public string Name;

        public Product(int id, double price, PriceTypes priceType, string name)
        {
            ID = id;
            Price = price;
            PriceType = priceType;
            Name = name;
        }

        public Product(int id, double price, PriceTypes priceType, string name, float weight)
        {
            ID = id;
            Price = price;
            PriceType = priceType;
            Name = name;
            Weight = weight;
        }
    }
}
