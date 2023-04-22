
namespace Checkout_System
{
    class ReceiptObject
    {
        public Product Product;
        public int Quantity;

        public ReceiptObject(Product product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
    }
    internal class Receipt
    {
        public List<ReceiptObject> ProductList = new List<ReceiptObject>();

        public Receipt(List<ReceiptObject> productList)
        {
            ProductList = productList;
        }

        public object GetProduct(int index)
        {
            if (ProductList.Count >= index) { return ProductList[index].Product; }
            else { Console.WriteLine("Error: Index out of bounds!"); return null; }
        }
    }
}
