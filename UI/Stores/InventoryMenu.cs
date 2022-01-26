namespace UI
{
  public class InventoryMenu : IMenu
  {
    public void Display()
    {
      Console.WriteLine("You are currently manage the  " + ListStoresMenu._currentStoreFront.Name + "'s inventory");
      Console.WriteLine("What do you want to do?");
      Console.WriteLine("[1] - Add New Product");
      Console.WriteLine("[2] - View All Products & Replenishment");
      Console.WriteLine("[0] - Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "MainMenu";
        case "1":
          return "AddNewProduct";
        case "2":
          return "ReplenishInventory";
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "InventoryMenu";
      }
    }
  }
}