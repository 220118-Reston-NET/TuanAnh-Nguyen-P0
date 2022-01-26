namespace Model
{
  public class Customer
  {
    // private string _Name;
    // public string Name
    // {
    //   get { return _Name; }
    //   set
    //   {
    //     if (value != "")
    //     {
    //       _Name = value;
    //     }
    //     else
    //     {
    //       throw new Exception("Your name should not be empty!");
    //     }
    //   }
    // }
    public string Name { get; set; }
    public string CustomerID { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<Orders> ListOrders;

    //Default constructor to add default values to the properties
    public Customer()
    {
      Name = "";
      Address = "";
      Email = "";
      PhoneNumber = "";
    }
  }

}
