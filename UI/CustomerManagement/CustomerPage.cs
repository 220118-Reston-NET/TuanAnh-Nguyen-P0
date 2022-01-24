namespace UI
{
  public class CustomerPage : IMenu
  {
    public void Display()
    {
      Console.WriteLine("What do you want to do today?");
      Console.WriteLine("[1] - My Profile");
      Console.WriteLine("[2] - Place a new order");
      Console.WriteLine("[3] - My Orders");
      Console.WriteLine("-----");
      Console.WriteLine("[0] - Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      //Switch cases are just useful if you are doing a bunch of comparision
      switch (_userInput)
      {
        case "0":
          return "Exit";
        case "1":
          return "ProfileMenu";
        case "2":
          return "PlaceNewOrder";
        case "3":
          return "MyOrdersMenu";
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "CustomerManager";
      }
    }
  }
}