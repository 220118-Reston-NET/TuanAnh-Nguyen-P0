namespace Model
{
  public class Orders
  {
    public List<LineItems> ListLineItems;
    public string StoreFrontLocation;
    public int TotalPrice { get; set; }
  }
}