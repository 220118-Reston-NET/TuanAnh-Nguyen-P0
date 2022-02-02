namespace UI
{
  public class MallMenu : IMenu
  {
    public void Display()
    {
      Console.WriteLine("MALL DEPARTMENT");
      Console.WriteLine("What do you want to do?");
      Console.WriteLine("[1] - Add New Product");
      Console.WriteLine("[2] - View All Products");
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
          return "AddNewProductMallMenu";
        case "2":
          return "ListAllMallProductsMenu";
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "MallMenu";
      }
    }
  }
}