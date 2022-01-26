namespace Model
{
  public class StoreFront
  {
    public string StoreID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }

    public StoreFront()
    {
      Name = "";
      Address = "";
    }
  }
}