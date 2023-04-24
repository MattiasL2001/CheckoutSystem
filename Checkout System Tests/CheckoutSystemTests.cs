namespace Checkout_System_Tests
{
    [TestClass]
    public class CheckoutSystemTests
    {
        string filePath = "C:\\Users\\Matti\\OneDrive\\Skrivbord\\test.txt";

        [TestMethod]
        public void Write_To_File_Then_Check_If_File_Exists()
        {
            filePath = "C:\\Users\\Matti\\OneDrive\\Skrivbord\\test.txt";
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
            Assert.AreEqual(3, File.ReadAllLines(filePath));
        }
    }
}
