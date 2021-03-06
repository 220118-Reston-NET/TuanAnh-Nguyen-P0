namespace Model
{
  public class Products
  {
    public string ProductID { get; set; }
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
    //       throw new Exception("Name of product should not be empty!");
    //     }
    //   }
    // }
    public string Name { get; set; }
    public int Price { get; set; }
    public string Desc { get; set; }
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
    //       throw new Exception("Quantity need to be more than 0");
    //     }
    //   }
    // }
    public DateTime createdAt { get; set; }
    public int MinimumAge { get; set; }

    public Products()
    {
      Name = "";
      Price = 0;
      Desc = "";
      MinimumAge = 0;
    }

    public override string ToString()
    {
      return $"Name: {Name}\nPrice: {Price}\nDescription: {Desc}\nMinimum Age: {MinimumAge}";
    }
  }
}