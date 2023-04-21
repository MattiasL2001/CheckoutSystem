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
            FileAndFormat.ProductsToFile(products, App.productsFilePath);
        }

        public static object GetProduct(int id)
        {
            List<Product> products = FileAndFormat.FileToProducts(App.productsFilePath);

            if (CheckIfProductExists(id))
            {
                foreach (Product p in products)
                {
                    if (p.ID == id) { return p; }
                }
            }
            return null;
        }

        public static object GetProduct(string name)
        {
            List<Product> products = FileAndFormat.FileToProducts(App.productsFilePath);

            if (CheckIfProductExists(name))
            {
                foreach (Product p in products)
                {
                    if (p.Name == name) { return p; }
                }
            }
            return null;
        }

        public static void RemoveProduct(int id)
        {
            List<Product> products = FileAndFormat.FileToProducts(App.productsFilePath);

            if (CheckIfProductExists(id))
            {
                List<Product> newProductList = new List<Product>();

                products.ForEach(x =>
                {
                    if (x.ID == id)
                    {
                        Console.WriteLine("Successfully removed the product: " + x.Name);
                        Console.WriteLine(x.Name);
                        newProductList.Remove(x);
                    }
                    else { newProductList.Add(x); }
                });
                products = newProductList;
                FileAndFormat.ProductsToFile(products, App.productsFilePath);
            }
            else
            {
                Console.WriteLine("No product with the given ID exists!");
            }
        }

        public static void RemoveProduct(string name)
        {
            List<Product> products = FileAndFormat.FileToProducts(App.productsFilePath);

            if (CheckIfProductExists(name))
            {
                List<Product> newProductList = new List<Product>();

                products.ForEach(x =>
                {
                    if (x.Name == name)
                    {
                        Console.WriteLine("Successfully removed the product: " + x.Name);
                        newProductList.Remove(x);
                    }
                    else { newProductList.Add(x); }
                });
                products = newProductList;
                FileAndFormat.ProductsToFile(products, App.productsFilePath);
            }
            else
            {
                Console.WriteLine("No product with the given name exists!");
            }
        }

        public static void ChangeProductPrice(int id)
        {
            List<Product> products = FileAndFormat.FileToProducts(App.productsFilePath);
            if (products.Count < id) { Console.WriteLine("Error: ID is out of bounds"); }
        }

        public static void ChangeProductName(string name)
        {
            List<Product> products = FileAndFormat.FileToProducts(App.productsFilePath);

            if (CheckIfProductExists(name))
            {
                Console.WriteLine("Input new name for the product!");
                string answer = Console.ReadLine();

                if (!CheckIfProductExists(answer))
                {
                    products.ForEach(x =>
                    {
                        if (x.Name == name)
                        {
                            x.Name = answer;
                            Console.WriteLine("Name of product changed!");
                        }
                    });
                }
                else
                {
                    Console.WriteLine("No products with the given name exists!");
                }
            }
        }

        public static bool CheckIfProductExists(int id)
        {
            List<Product> products = FileAndFormat.FileToProducts(App.productsFilePath);

            foreach (Product p in products)
            {
                if (p.ID == id) { return true; }
            }
            return false;
        }
        public static bool CheckIfProductExists(string name)
        {
            List<Product> products = FileAndFormat.FileToProducts(App.productsFilePath);
            foreach (Product p in products)
            {
                Console.WriteLine(p.Name);
                if (p.Name == name) { return true; }
            }
            return false;
        }
    }
}
