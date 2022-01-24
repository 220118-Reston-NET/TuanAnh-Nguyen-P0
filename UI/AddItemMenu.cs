using Model;

namespace UI
{
  public class AddItem : IMenu
  {
    //static non-access modifier is needed to keep this variable consistent to all objects we create out of our AddItemMenu
    private static Products _newProduct = new Products();
    public void Display()
    {
      Console.WriteLine("Enter item information");
      Console.WriteLine("[1] - Name: " + _newProduct.Name);
      Console.WriteLine("[2] - Quantity: " + _newProduct.Quantity);
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
          Console.WriteLine("Please enter a name:");
          _newProduct.Name = Console.ReadLine();
          return "AddNewItem";
        case "2":
          Console.WriteLine("Please enter quantity:");
          _newProduct.Quantity = Convert.ToInt32(Console.ReadLine());
          return "AddNewItem";
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "AddNewItem";
      }
    }
  }
}