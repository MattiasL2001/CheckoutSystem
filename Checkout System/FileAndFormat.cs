using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout_System
{
    internal class FileAndFormat
    {
        public static string ChangeFileDirectory()
        {
            while (true)
            {
                Console.WriteLine("Enter an existing directory!");
                Console.WriteLine("Input 'Back' to go to the main menu.");
                string directory = Console.ReadLine();
                string filePath = "";
                string fileName;

                if (directory.ToLower() == "back")
                {
                    return "back";
                }

                if (Directory.Exists(directory))
                {
                    Console.WriteLine("Enter a file name:");
                    fileName = Console.ReadLine();
                    try
                    {
                        File.WriteAllText(directory + "\\" + fileName + ".txt", "");
                    }
                    catch
                    {
                        Console.WriteLine("Access denied to that directory, choose another one:");
                        Console.WriteLine(new UnauthorizedAccessException());
                        ChangeFileDirectory();
                    }

                    filePath = directory + "\\" + fileName + ".txt";
                    Console.WriteLine("Directory changed to:");
                    Console.WriteLine(filePath);
                    return "";
                }
                else
                {
                    Console.WriteLine("You must enter a VALID directory that exists!");
                }
            }
        }

        public static void ProductsToFile(List<Product> list, string productsFilePath)
        {
            string stringBuilder = "";

            for (int i = 0; i < list.Count; i++)
            {
                stringBuilder += "{\n   ";
                stringBuilder += list[i].ID + ",\n   ";
                stringBuilder += list[i].Price + ",\n   ";
                stringBuilder += list[i].Name + ",\n   ";
                stringBuilder += list[i].PriceType + "\n";
                stringBuilder += "}";

                if (list.Count > 1 && i + 1 < list.Count) { stringBuilder += ",\n"; }
            }

            File.WriteAllText(productsFilePath, stringBuilder);
        }

        public static List<Product> FileToProducts(string filePath)
        {
            string fileContent = "";
            List<Product> products = new List<Product>();

            if (File.Exists(filePath)) { fileContent = File.ReadAllText(filePath); }
            else
            {
                File.WriteAllText(filePath, "");
                fileContent = File.ReadAllText(filePath);
            }

            if (fileContent.Contains("},"))
            {
                string[] productsString = fileContent.Split("},");

                foreach (string prod in productsString)
                {
                    string prodString = prod;
                    prodString = prodString.Replace("},", "");
                    prodString = prodString.Replace("{", "");
                    prodString = prodString.Replace("}", "");
                    prodString = prodString.Replace("\n", "");

                    int prodId = Convert.ToInt32(prodString.Split(',')[0]);
                    int prodPrice = Convert.ToInt32(prodString.Split(',')[1]);
                    string prodName = prodString.Split(',')[2].Trim();
                    Product.PriceTypes prodPriceType;

                    if (prodString.Split(",")[3].Contains("Unit"))
                    { prodPriceType = Product.PriceTypes.PricePerUnit; }
                    else { prodPriceType = Product.PriceTypes.PricePerKG; };

                    products.Add(new Product(prodId, prodPrice, prodPriceType, prodName));
                }
            }
            else if (fileContent != "")
            {
                fileContent = fileContent.Replace("}", "");
                fileContent = fileContent.Replace("{", "");
                fileContent = fileContent.Replace("\n", "");
                int productID = Convert.ToInt32(fileContent.Split(',')[0]);
                int productPrice = Convert.ToInt32(fileContent.Split(',')[1]);
                string productName = fileContent.Split(",")[2].Trim();
                Product.PriceTypes priceType = new Product.PriceTypes();

                if (fileContent.Split(",")[3].Contains("Unit"))
                { priceType = Product.PriceTypes.PricePerUnit; }
                else { priceType = Product.PriceTypes.PricePerKG; };

                products.Add(new Product(productID, productPrice, priceType, productName));
            }

            fileContent = fileContent.Replace("},", "");
            fileContent = fileContent.Replace("}", "");
            fileContent = fileContent.Replace("{", "");
            fileContent = fileContent.Replace("\n", "");

            return products;
        }

        public static void ClearProducts(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath, "");
            }
            else
            {
                Console.WriteLine("The file:");
                Console.WriteLine(filePath);
                Console.WriteLine("does not exist!");
            }
        }

        public static int GetPreviousSerialNumber(string receiptFilePath)
        {
            List<string> receipts = File.ReadAllText(receiptFilePath).
                Split("----------------------------------").ToList();
            List<string> receiptObject = new List<string>();
            string serialNumber = "0";
            receipts.ForEach(receipt =>
            {
                if (receipt != "" && receipt != "\n")
                {
                    if (receipt.Split("\n")[1].Contains("SerialNumber"))
                    {
                        serialNumber = receipt.Split("\n")[1].Replace("SerialNumber: ", "");
                    }
                    else
                    {
                        serialNumber = receipt.Split("\n")[2].Replace("SerialNumber: ", "");
                    }
                }
            });

            return Convert.ToInt32(serialNumber);
        }

        public static string ReceiptToFile(Receipt receipt, string receiptFilePath)
        {
            float price = 0;
            string stringBuilder = "";
            int serialNumber = GetPreviousSerialNumber(receiptFilePath) + 1;
            stringBuilder += "RECEIPT: " + DateTime.Now + "\n";
            stringBuilder += $"SerialNumber: {serialNumber}\n";

            receipt.ProductList.ForEach(x =>
            {
                if (x.Product.PriceType == Product.PriceTypes.PricePerKG)
                {
                    var p = x.Product;
                    price += p.Price * p.Weight;
                    stringBuilder += $"{p.Name} {p.Price}kr/kg x {p.Weight.ToString("0.0")}kg";
                    stringBuilder += $" = {price}kr\n";
                }
                else
                {
                    price += x.Quantity * x.Product.Price;
                    stringBuilder += $"{x.Product.Name} {x.Quantity} x {x.Product.Price}kr";
                    stringBuilder += $" = {price}kr\n";
                }
            });

            stringBuilder += $"Total: {price}kr\n";
            stringBuilder += "----------------------------------" + "\n";
            return stringBuilder;
        }
        public static void FileToReceipt(string filePath)
        {
            string fileContent = File.ReadAllText(filePath);

            fileContent = fileContent.Replace("},", "");
            fileContent = fileContent.Replace("}", "");
            fileContent = fileContent.Replace("{", "");
            fileContent = fileContent.Replace("\n", "");
        }
    }
}
