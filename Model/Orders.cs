namespace Model
{
  public class Orders
  {
    public string OrderID { get; set; }
    public List<LineItems> ListLineItems;
    public string StoreFrontLocation;
    public int TotalPrice { get; set; }
  }
}