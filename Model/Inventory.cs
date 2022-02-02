namespace Model
{
  public class Inventory
  {
    public string InventoryID { get; set; }
    public string ProductID { get; set; }
    public string StoreID { get; set; }
    public int Quantity { get; set; }

    public Inventory()
    {
      InventoryID = "";
      ProductID = "";
      StoreID = "";
      Quantity = 0;
    }

    public override string ToString()
    {
      return $"Quantity: {Quantity}";
    }
  }
}