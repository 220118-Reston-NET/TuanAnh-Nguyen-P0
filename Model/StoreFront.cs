namespace Model
{
  public class StoreFront
  {
    public string StoreID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Products> ListProducts;
    public List<Orders> ListOrders;

    public StoreFront()
    {
      Name = "";
      Address = "";
    }
  }
}