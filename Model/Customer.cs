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
    public string CustomerID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public DateTime createdAt { get; set; }
    public DateTime DateOfBirth { get; set; }

    //Default constructor to add default values to the properties
    public Customer()
    {
      Name = "";
      Address = "";
      Email = "";
      PhoneNumber = "";
      DateOfBirth = DateTime.UtcNow;
    }

    public override string ToString()
    {
      return $"Name: {Name}\nAddress: {Address}\nEmail: {Email}\nPhoneNumber: {PhoneNumber}\nCreated At: {createdAt}\nDOB: {DateOfBirth.ToShortDateString()}({Age(DateOfBirth)})";
    }

    protected int Age(DateTime p_dateOfBirth)
    {
      int _age = DateTime.UtcNow.Year - p_dateOfBirth.Year;
      if (DateTime.UtcNow.Month < p_dateOfBirth.Month || DateTime.UtcNow.Month == p_dateOfBirth.Month && DateTime.UtcNow.Day < p_dateOfBirth.Day)
      {
        _age--;
      }
      return _age;
    }
  }
}
