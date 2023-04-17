using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout_System
{
    public class App
    {
        string receiptFilePath = "C:\\Users\\Matti\\OneDrive\\Skrivbord\\RECEIPT" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
        string productsFilePath = "C:\\Users\\Matti\\OneDrive\\Skrivbord\\" + "Products" + ".txt";
        List<Product> allProducts = new List<Product>();
        List<ReceiptObject> receiptProducts = new List<ReceiptObject>();
        Receipt receipt;

        public App()
        {
            receipt = new Receipt(receiptProducts);
            Product banana = new Product(1, 25, Product.PriceTypes.PricePerKG, "Banana");
            Product apple = new Product(2, 30, Product.PriceTypes.PricePerKG, "Apple");
            Admin.AddProductToList(banana, allProducts);
            Admin.AddProductToList(apple, allProducts);
        }
        public void Run()
        {
            Console.WriteLine("Checkout:");
            Console.WriteLine("1. New checkout");
            Console.WriteLine("2. Change file directory");
            Console.WriteLine("3. Admin page");
            Console.WriteLine("4. Exit");
            string answer = Console.ReadLine();

            if (answer == "1") { Checkout(); }
            else if (answer == "2")
            {
                string s = FileAndFormat.ChangeFileDirectory();
                if (s.ToLower() == "back") { Run(); }
            }
            else if (answer == "3") {  }
            else if (answer == "4") { Environment.Exit(0); }
            else { Run(); }
        }

        void Checkout()
        {
            Console.WriteLine("Checkout");
            while (true)
            {
                Console.WriteLine("Add products below: <product id> <quantity>");
                string answer = Console.ReadLine();
                string[] answerSplit;
                Product prod = null;

                if (answer.ToLower() != "pay")
                {
                    answerSplit = answer.Split(" ");

                    try
                    {
                        int id = Convert.ToInt32(answerSplit[0]);
                        int quantity = Convert.ToInt32(answerSplit[1]);
                        bool productExists = false;

                        allProducts.ForEach(p =>
                        {
                            if (p.ID == id)
                            {
                                productExists = true;
                                prod = new Product(p.ID, p.Price, p.PriceType, p.Name);
                            }
                        });

                        if (!productExists)
                        {
                            Console.WriteLine("Could not find any product with the given id!");
                        }
                        else if (prod != null)
                        {
                            receiptProducts.Add(new ReceiptObject(prod, quantity));
                            Console.WriteLine($"Added {quantity} {prod.Name}(s)");
                        }
                    }
                    catch
                    {
                        Console.WriteLine("ERROR (wrong input-format)");
                    }
                }
                else
                {
                    FileAndFormat.ProductsToFile(allProducts, productsFilePath);
                    FileAndFormat.FileToProducts(productsFilePath);
                    string receiptString = FileAndFormat.ReceiptToFile(receipt, receiptFilePath);
                    File.WriteAllText(receiptFilePath, File.ReadAllText(receiptFilePath) + receiptString);
                    FileAndFormat.FileToReceipt(receiptFilePath);
                    Run();
                    break;
                }
            }
        }
    }
}
