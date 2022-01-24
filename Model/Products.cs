namespace Model
{
  public class Products
  {
    private string _Name;
    public string Name
    {
      get { return _Name; }
      set
      {
        if (value != "")
        {
          _Name = value;
        }
        else
        {
          throw new Exception("Name of product should not be empty!");
        }
      }
    }
    public int Price { get; set; }
    public string Desc { get; set; }
    private int _Quantity;
    public int Quantity
    {
      get { return _Quantity; }
      set
      {
        if (value >= 0)
        {
          _Quantity = value;
        }
        else
        {
          throw new Exception("Quantity need to be more than 0");
        }
      }
    }
  }
}