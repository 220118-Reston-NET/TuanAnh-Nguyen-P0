namespace UI
{
  public class ListStores : IMenu
  {
    public void Display()
    {
      Console.WriteLine("List all stores in shopping mall:");
      //List of Store

      Console.WriteLine("-----");
      Console.WriteLine("Please select the store that you want to look at");
      Console.WriteLine("[0] - Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "StoresMenu";

        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "ListStores";
      }
    }
  }
}