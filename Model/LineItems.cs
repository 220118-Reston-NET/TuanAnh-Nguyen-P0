namespace Model
{
  public class LineItems
  {
    // private string _Product;
    // public string Product
    // {
    //   get { return _Product; }
    //   set
    //   {
    //     if (value != "")
    //     {
    //       _Product = value;
    //     }
    //     else
    //     {
    //       throw new Exception("Name of the product can not be empty!");
    //     }
    //   }
    // }
    // private int _Quantity;
    // public int Quantity
    // {
    //   get { return _Quantity; }
    //   set
    //   {
    //     if (value >= 0)
    //     {
    //       _Quantity = value;
    //     }
    //     else
    //     {
    //       throw new Exception("Quantity should be greater than 0!");
    //     }
    //   }
    // }
    public string ProductName { get; set; }
    public int PriceAtCheckedOut { get; set; }
    public int Quantity { get; set; }
    public string ProductID { get; set; }
    public string OrderID { get; set; }

    public LineItems()
    {
      ProductName = "";
      PriceAtCheckedOut = 0;
      Quantity = 0;
    }

    public override string ToString()
    {
      return $"{ProductName} - ${PriceAtCheckedOut}({Quantity})";
    }
  }
}