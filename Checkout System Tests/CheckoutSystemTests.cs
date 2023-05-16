using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Checkout_System_Tests
{
    [TestClass]
    public class CheckoutSystemTests
    {
        string filePath = "C:\\Users\\Matti\\OneDrive\\Skrivbord\\test.txt";

        [TestMethod]
        public void Write_To_File_Then_Check_If_File_Exists()
        {
            File.WriteAllText(filePath, "");
            Assert.IsTrue(File.Exists(filePath));
        }

        [TestMethod]
        public void Write_To_File_Then_Check_File_Lines()
        {
            string stringBuilder = "";
            stringBuilder += "{\n   ";
            stringBuilder += "Testing\n";
            stringBuilder += "}";
            File.WriteAllText(filePath, stringBuilder);
            Assert.AreEqual(3, File.ReadAllLines(filePath).Length);
        }

        [TestMethod]
        public void Get_Parts_Of_Product_String_Then_Validate()
        {
            string appleProductString = "1, 29, Apple, PricePerKG, 0.120";
            string chocolateProductString = "1, 29, Apple, PricePerUnit";


            Assert.AreEqual(appleProductString.Split(',').Length, 5);
            Assert.AreEqual(chocolateProductString.Split(',').Length, 4);
            Assert.AreEqual(appleProductString.Split(',')[2].Trim(), "Apple");
        }

        [TestMethod]
        public void Format_Number_Then_Validate()
        {
            int numberOfDecimals = 3;
            string stringNumber = "15,137262";
            if (!stringNumber.Contains(",")) { stringNumber += ","; }
            stringNumber += "00000000000000000000000000000";
            decimal newNumber = Math.Round(Convert.ToDecimal(stringNumber), numberOfDecimals);
            Assert.AreEqual((double)newNumber, 15.137);
        }
    }
}
