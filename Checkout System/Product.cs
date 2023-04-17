using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout_System
{
    internal class Product
    {
        public enum PriceTypes { PricePerUnit, PricePerKG}

        public int ID;
        public float Price;
        public float Weight;
        public PriceTypes PriceType;
        public string Name;

        public Product(int id, float price, PriceTypes priceType, string name)
        {
            ID = id;
            Price = price;
            PriceType = priceType;
            Name = name;
        }

        public Product(int id, float price, PriceTypes priceType, string name, float weight)
        {
            ID = id;
            Price = price;
            PriceType = priceType;
            Name = name;
            Weight = weight;
        }
    }
}
