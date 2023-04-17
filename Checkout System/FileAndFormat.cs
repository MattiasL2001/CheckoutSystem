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
                stringBuilder += list[i].Name + ",\n";
                stringBuilder += "}";

                if (list.Count > 1 && i + 1 < list.Count) { stringBuilder += ",\n"; }
            }

            File.WriteAllText(productsFilePath, stringBuilder);
        }

        public static List<Receipt> FileToProducts(string filePath)
        {
            string fileContent = File.ReadAllText(filePath);

            fileContent = fileContent.Replace("},", "");
            fileContent = fileContent.Replace("}", "");
            fileContent = fileContent.Replace("{", "");
            fileContent = fileContent.Replace("\n", "");

            Console.WriteLine(fileContent);
            return new List<Receipt>();
        }

        public static string ReceiptToFile(Receipt receipt, string receiptFilePath)
        {
            float price = 0;
            string stringBuilder = "";
            stringBuilder += "RECEIPT: " + DateTime.Now + "\n";

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
                    stringBuilder += $"{x.Product.Name} {x.Quantity} x {x.Product.Price}";
                    stringBuilder += $" = {price}";
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
