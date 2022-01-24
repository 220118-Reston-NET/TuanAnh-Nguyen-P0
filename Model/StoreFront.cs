namespace Model
{
  public class StoreFront
  {
    public string Name { get; set; }
    public string Address { get; set; }
    public List<Products> ListProducts;
    public List<Orders> ListOrders;
  }
}