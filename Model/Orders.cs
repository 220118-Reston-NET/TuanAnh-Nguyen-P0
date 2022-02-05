namespace Model
{
  public class Orders
  {
    public string OrderID { get; set; }
    public List<LineItems> ListLineItems { get; set; }
    public string CustomerID { get; set; }
    public string StoreID { get; set; }
    public DateTime createdAt { get; set; }
    public int TotalPrice { get; set; }
    public string Status { get; set; }
    public List<Shipment> ListTrackings { get; set; }
    public Orders()
    {
      OrderID = "";
      ListLineItems = new List<LineItems>() { new LineItems() };
      CustomerID = "";
      StoreID = "";
      createdAt = DateTime.Now;
      TotalPrice = 0;
      Status = "";
      ListTrackings = new List<Shipment>() { new Shipment() };
    }
  }
}