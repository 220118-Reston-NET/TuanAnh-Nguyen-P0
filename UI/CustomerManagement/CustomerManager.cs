namespace UI
{
  public class CustomerManager : IMenu
  {
    public void Display()
    {
      Console.WriteLine("Please choose the user that you want to start:");
      //Showing the list of user from DL
      Console.WriteLine("-----");
      Console.WriteLine("[0] - Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "MainMenu";
        //List of users from DL
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "CustomerManager";
      }
    }
  }
}