using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout_System
{
    static class Admin
    {
        public static void AddProductToList(Product product, List<Product> products)
        {
            foreach (Product p in products)
            {
                if (product.ID == p.ID)
                {
                    throw new Exception("Product ID already exists!");
                }

                else if (product.Name == p.Name)
                {
                    throw new Exception("Product name already exists!");
                }

                else
                {
                    products.Add(product);
                    return;
                }
            }
            products.Add(product);
        }

        public static void ChangeProductPrice(List<Product> products, int id)
        {
            if (products.Count < id) { Console.WriteLine("Error: ID is out of bounds"); }
        }
    }
}
