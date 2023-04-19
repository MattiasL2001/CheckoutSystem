using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout_System
{
    static class Admin
    {
        public static void AddProduct(Product product)
        {
            List<Product> products = FileAndFormat.FileToProducts(App.productsFilePath);


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

        public static void RemoveProduct()
        {

        }

        public static void ChangeProductPrice(List<Product> products, int id)
        {
            if (products.Count < id) { Console.WriteLine("Error: ID is out of bounds"); }
        }

        public static void ChangeProductName(List<Product> products, string name)
        {

        }

        public static bool CheckIfProductExists(int id)
        {
            var products = FileAndFormat.FileToProducts(App.productsFilePath);

            foreach (Product p in products)
            {
                if (p.ID == id) { return true; }
            }
            return false;
        }
        public static bool CheckIfProductExists(string name)
        {
            var products = FileAndFormat.FileToProducts(App.productsFilePath);

            foreach (Product p in products)
            {
                if (p.Name == name) { return true; }
            }
            return false;
        }
    }
}
