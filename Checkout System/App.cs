using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout_System
{
    internal class App
    {
        string receiptFilePath = "C:\\Users\\Matti\\OneDrive\\Skrivbord\\RECEIPT" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
        string productsFilePath = "C:\\Users\\Matti\\OneDrive\\Skrivbord\\" + "Products" + ".txt";
        List<Product> products = new List<Product>();

        public App()
        {
            Product banana = new Product(1, 25, Product.PriceTypes.PricePerKG, "Banana");
            Product apple = new Product(2, 30, Product.PriceTypes.PricePerKG, "Apple");
            AddProductToList(banana);
            AddProductToList(apple);
        }
        public void Run()
        {
            Console.WriteLine("Checkout:");
            Console.WriteLine("1. New checkout");
            Console.WriteLine("2. Exit");
            string answer = Console.ReadLine();

            if (answer == "1") { Checkout(); }
            else if (answer == "2") { Environment.Exit(0); }
            else { Run(); }
        }

        void AddProductToList(Product product)
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

        void Checkout()
        {
            Console.WriteLine("Checkout");
            Console.WriteLine("Add products below: <product id> <quantity>");
            string answer = Console.ReadLine();
            string[] answerSplit;

            if (answer.ToLower() != "pay")
            {
                answerSplit = answer.Split(" ");

                try
                {
                    int id = Convert.ToInt32(answerSplit[0]);
                    int quantity = Convert.ToInt32(answerSplit[1]);
                }
                catch
                {
                    Console.WriteLine("ERROR (wrong input-format)");
                    Checkout();
                }
            }
            else
            {
                ReceiptToFile(products);
            }
        }

        void ReceiptToFile(List<Product> list)
        {
            File.WriteAllText(receiptFilePath, "");
        }
    }
}
