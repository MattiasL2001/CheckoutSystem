
namespace Checkout_System
{
    public class App
    {
        //public static string receiptFilePath =
        //"RECEIPT" + DateTime.Today.ToString("yyyyMMdd") + ".txt";

        public static string productsFilePath =
        "Products" + ".txt";

        public static string campaignsFilePath =
        "Campaigns" + ".txt";
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
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   _____ _               _               _      _____           _                 \r\n" +
                              "  / ____| |             | |             | |    / ____|         | |                \r\n" +
                              " | |    | |__   ___  ___| | _____  _   _| |_  | (___  _   _ ___| |_ ___ _ __ ___  \r\n" +
                              " | |    | '_ \\ / _ \\/ __| |/ / _ \\| | | | __|  \\___ \\| | | / __| __/ _ \\ '_ ` _ \\ \r\n" +
                              " | |____| | | |  __/ (__|   < (_) | |_| | |_   ____) | |_| \\__ \\ ||  __/ | | | | |\r\n" +
                              "  \\_____|_| |_|\\___|\\___|_|\\_\\___/ \\__,_|\\__| |_____/ \\__, |___/\\__\\___|_| |_| |_|\r\n" +
                              "                                                       __/ |                      \r\n" +
                              "                                                      |___/                       ");
            Console.ResetColor();
            Console.WriteLine("Checkout:");
            Console.WriteLine("1. New checkout");
            Console.WriteLine("2. Change file directory");
            Console.WriteLine("3. Admin page");
            Console.WriteLine("4. Exit");
            string answer = Console.ReadLine();

            if (answer == "1") { Checkout(); }
            else if (answer == "2")
            {
                string result = FileAndFormat.ChangeFileDirectory();
                if (result.ToLower() == "back") { Run(); }
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
                Console.WriteLine("5. To add product campaign");
                Console.WriteLine("Input 'Back' to go to the main menu.");
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
                        Product productCast = (Product)product;
                        List<Product> newProductList = new List<Product>();

                        listOfProducts.ForEach(product =>
                        {
                            if (product.ID != productCast.ID) { newProductList.Add(product); }
                        });

                        string newPrice;

                        while(true)
                        {
                            Console.WriteLine("Enter a new price for the product!");
                            newPrice = Console.ReadLine();
                            if (int.TryParse(newPrice, out int intPrice))
                            {
                                productCast.Price = intPrice;
                                break;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Could not recognize input as an integer!");
                                Console.ResetColor();
                            }
                        }

                        newProductList.Add(productCast);
                        FileAndFormat.ProductsToFile(newProductList, productsFilePath);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Could not find any product with the given id or name!");
                        Console.ResetColor();
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
                        Product productCast = (Product)product;
                        List<Product> newProductList = new List<Product>();

                        listOfProducts.ForEach(product =>
                        {
                            if (product.ID != productCast.ID) { newProductList.Add(product); }
                        });

                        Console.WriteLine("Enter a new name for the product!");
                        string newName = Console.ReadLine();
                        productCast.Name = newName;
                        newProductList.Add(productCast);
                        FileAndFormat.ProductsToFile(newProductList, productsFilePath);
                    }
                    else
                    {
                        Console.WriteLine(product);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Could not find any product with the given id or name!");
                        Console.ResetColor();
                    }
                }

                else if (answer == "5")
                {
                    int id;
                    int discountPercent;
                    string title;

                    while(true)
                    {
                        Console.WriteLine("Enter the ID for the product to add a campaign on!");
                        string productID = Console.ReadLine();

                        if (!int.TryParse(productID, out id))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Could not recognize input as an integer!");
                            Console.ResetColor();
                            continue;
                        }

                        if (Checkout_System.Admin.CheckIfProductExists(id))
                        {
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("No product with the given ID exists!");
                            Console.ResetColor();
                        }
                    }

                    while(true)
                    {
                        Console.WriteLine("How much in % should the discount be? 0-100");
                        if (int.TryParse(Console.ReadLine(), out int discount))
                        {
                            if (discount > 100) { discount = 100; }
                            discountPercent = discount;
                            break;
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Could not recognize input as an integer!");
                            Console.ResetColor();
                        }
                    }

                    Console.WriteLine("Enter a title for the campaign!");
                    title = Console.ReadLine();

                    var campaignList = new List<Campaign>();

                    FileAndFormat.FileToCampaigns(campaignsFilePath).ForEach(campaignList.Add);
                    campaignList.Add(new Campaign(id, discountPercent, title));

                    FileAndFormat.CampaignsToFile(campaignList, campaignsFilePath);
                    Console.WriteLine("The campaign was successfully added!");
                }
            }
        }

        void Checkout()
        {
            Console.WriteLine("Checkout");
            while (true)
            {
                Console.WriteLine("Add products below: <product id> <quantity>");
                Console.WriteLine("Input 'Back' to go to the main menu.");
                string answer = Console.ReadLine();
                string[] answerSplit;
                Product product = null;

                if (answer.ToLower() == "back")
                {
                    Run();
                    break;
                }

                if (answer.ToLower() != "pay")
                {
                    listOfProducts = FileAndFormat.FileToProducts(productsFilePath);
                    answerSplit = answer.Split(" ");

                    try
                    {
                        int id = Convert.ToInt32(answerSplit[0]);
                        int quantity = Convert.ToInt32(answerSplit[1]);
                        bool productExists = false;

                        listOfProducts.ForEach(prod =>
                        {
                            if (prod.ID == id && prod.PriceType == Product.PriceTypes.PricePerKG)
                            {
                                productExists = true;
                                product = new Product(prod.ID, prod.Price, prod.PriceType, prod.Name, prod.Weight);
                            }
                            else if (prod.ID == id)
                            {
                                productExists = true;
                                product = new Product(prod.ID, prod.Price, prod.PriceType, prod.Name);
                            }
                        });

                        if (!productExists)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Could not find any product with the given id!");
                            Console.ResetColor();
                        }
                        else if (product != null)
                        {
                            receiptProducts.Add(new ReceiptObject(product, quantity));
                            Console.WriteLine($"Added {quantity} {product.Name}(s)");
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("ERROR (wrong input-format)");
                        Console.ResetColor();
                    }
                }
                else
                {
                    FileAndFormat.ReceiptToFile(receipt);
                    Run();
                    break;
                }
            }
        }
    }
}
