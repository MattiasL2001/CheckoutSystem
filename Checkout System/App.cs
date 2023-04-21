using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout_System
{
    public class App
    {
        public static string receiptFilePath = "C:\\Users\\Matti\\OneDrive\\Skrivbord\\RECEIPT" + DateTime.Today.ToString("yyyyMMdd") + ".txt";
        public static string productsFilePath = "C:\\Users\\Matti\\OneDrive\\Skrivbord\\" + "Products" + ".txt";
        List<Product> listOfProducts = new List<Product>();
        List<ReceiptObject> receiptProducts = new List<ReceiptObject>();
        Receipt receipt;

        public App()
        {
            receipt = new Receipt(receiptProducts);
            listOfProducts = FileAndFormat.FileToProducts(productsFilePath);

            //Product banana = new Product(1, 25, Product.PriceTypes.PricePerKG, "Banana");
            //Product apple = new Product(2, 30, Product.PriceTypes.PricePerKG, "Apple");
            //Product chocolateBar = new Product(3, 20, Product.PriceTypes.PricePerUnit, "ChocolateBar");

            //Checkout_System.Admin.AddProduct(banana);
            //Checkout_System.Admin.AddProduct(apple);
            //Checkout_System.Admin.AddProduct(chocolateBar);
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
            else if (answer == "3") { Admin(); }
            else if (answer == "4") { Environment.Exit(0); }
            else { Run(); }
        }

        void Admin()
        {
            while (true)
            {
                Console.WriteLine("Admin page");
                Console.WriteLine("1. To add product");
                Console.WriteLine("2. To remove product");
                Console.WriteLine("3. To change product price");
                Console.WriteLine("4. To change product name");
                Console.WriteLine("'Back' to go back to main menu");
                string answer = Console.ReadLine();
                if (answer.ToLower() == "back")
                {
                    Run();
                    break;
                }
                else if (answer == "1") { Checkout_System.Admin.AddProduct(); }
                else if (answer == "2")
                {
                    Console.WriteLine("Enter an ID or name for the product to remove.");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out int result))
                    {
                        Checkout_System.Admin.RemoveProduct(result);
                    }
                    else
                    {
                        Checkout_System.Admin.RemoveProduct(input);
                    }
                }

                else if (answer == "3")
                {
                    Console.WriteLine("Enter an ID or name for the product to change price on.");
                    string input = Console.ReadLine();
                    object product;

                    if (int.TryParse(input, out int result))
                    {
                        product = Checkout_System.Admin.GetProduct(result);
                    }
                    else
                    {
                        product = Checkout_System.Admin.GetProduct(input);
                    }

                    if (product != null)
                    {
                        FileAndFormat.ClearProducts(productsFilePath);
                        Product p = product as Product;
                        List<Product> newProductList = new List<Product>();

                        listOfProducts.ForEach(prod =>
                        {
                            if (prod.ID != p.ID) { newProductList.Add(prod); }
                        });

                        string newPrice;

                        while(true)
                        {
                            Console.WriteLine("Enter a new price for the product!");
                            newPrice = Console.ReadLine();
                            if (int.TryParse(newPrice, out int intPrice))
                            {
                                p.Price = intPrice;
                                break;
                            }
                            else { Console.WriteLine("Could not recognize input as an integer!"); }
                        }

                        newProductList.Add(p);
                        FileAndFormat.ProductsToFile(newProductList, productsFilePath);
                    }
                    else
                    {
                        Console.WriteLine(product);
                        Console.WriteLine("Could not find any product with the given id or name!");
                    }
                }

                else if (answer == "4")
                {
                    Console.WriteLine("Enter an ID or name for the product to change name on.");
                    string input = Console.ReadLine();
                    object product;
                    if (int.TryParse(input, out int result))
                    {
                        product = Checkout_System.Admin.GetProduct(result);
                    }
                    else
                    {
                        product = Checkout_System.Admin.GetProduct(input);
                    }

                    if (product != null)
                    {
                        FileAndFormat.ClearProducts(productsFilePath);
                        Product p = product as Product;
                        List<Product> newProductList = new List<Product>();

                        listOfProducts.ForEach(prod =>
                        {
                            if (prod.ID != p.ID) { newProductList.Add(prod); }
                        });

                        Console.WriteLine("Enter a new name for the product!");
                        string newName = Console.ReadLine();
                        p.Name = newName;
                        newProductList.Add(p);
                        FileAndFormat.ProductsToFile(newProductList, productsFilePath);
                    }
                    else
                    {
                        Console.WriteLine(product);
                        Console.WriteLine("Could not find any product with the given id or name!");
                    }
                }
            }
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

                        listOfProducts.ForEach(p =>
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
                    FileAndFormat.ProductsToFile(listOfProducts, productsFilePath);
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
