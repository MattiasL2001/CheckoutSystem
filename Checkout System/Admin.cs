
namespace Checkout_System
{
    static class Admin
    {
        public static void AddProduct()
        {
            string input;
            List<Product> products = FileAndFormat.FileToProducts(App.productsFilePath);
            Product product = new Product(0, 0, 0, "");

            while (true)
            {
                Console.WriteLine("Input an ID for the product");
                input = Console.ReadLine();

                if (!int.TryParse(input, out int intInput))
                {
                    Console.WriteLine("Could not recognize input as an integer!");
                    continue;
                }

                if (CheckIfProductExists(Convert.ToInt32(input)))
                {
                    Console.WriteLine("A product with the given ID already exists!");
                    continue;
                }

                product.ID = Convert.ToInt32(input);
                break;
            }

            while (true)
            {
                Console.WriteLine("Input a name for the product");
                input = Console.ReadLine();
                if (CheckIfProductExists(input))
                {
                    Console.WriteLine("A product with the given name already exists!");
                }
                else { product.Name = input; break; }
            }

            while (true)
            {
                Console.WriteLine("Price per unit or price per KG?");
                Console.WriteLine("1. For price per unit");
                Console.WriteLine("2. For price per KG");
                input = Console.ReadLine().Trim();

                if (input == "1") { product.PriceType = Product.PriceTypes.PricePerUnit; break; }
                else if (input == "2") { product.PriceType = Product.PriceTypes.PricePerKG; break; }
                else { Console.WriteLine("Invalid input!"); }
            }

            while (true)
            {
                Console.WriteLine("Input a price for the product");
                input = Console.ReadLine();

                if (int.TryParse(input, out int output)) { product.Price = output; break; }
                else { Console.WriteLine("Could not recognize input as an integer!"); }
            }

            products = FileAndFormat.FileToProducts(App.productsFilePath);
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
                if (p.Name == name) { return true; }
            }
            return false;
        }
    }
}
