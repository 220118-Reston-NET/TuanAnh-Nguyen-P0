namespace UI
{
  public class StoreManger : IMenu
  {
    public void Display()
    {
      Console.WriteLine("Please choose the store that you manage:");
      //Showing the list of store from DL
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
        //List of stores from DL
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "StoreManager";
      }
    }
  }
}