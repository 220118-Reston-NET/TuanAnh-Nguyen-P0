namespace UI
{
  /*
    Mainmenu inherits IMenu interface but since it is a class it needs to give actual implementation details to the methods
    stated inside of the interface
  */
  public class MainMenu : IMenu
  {
    public void Display()
    {
      Console.WriteLine("Welcome to Our Shopping Mall");

      //Check if current Customer or current Store Manager is chosen
      if (ListCustomersMenu._currentCustomer.Name != "")
      {
        Console.WriteLine("Hello " + ListCustomersMenu._currentCustomer.Name + ", you are signing in as a Customer!");
        Console.WriteLine("What do you want to do today?");
        Console.WriteLine("[S] - Sign Out");
        Console.WriteLine("[1] - Order History");
        Console.WriteLine("[2] - Place a new order");
      }
      else if (ListStoresMenu._currentStoreFront.Name != "")
      {
        Console.WriteLine("Hello " + ListStoresMenu._currentStoreFront.Name + ", you are signing in as a Store Manager!");
        Console.WriteLine("What do you want to do today?");
        Console.WriteLine("[S] - Sign Out");
        Console.WriteLine("[1] - Orders");
        Console.WriteLine("[2] - Inventory");
      }
      else
      {
        Console.WriteLine("---If you are new, try to sign up here---");
        Console.WriteLine("[1] - Customer");
        Console.WriteLine("[2] - Store Manager");
        Console.WriteLine("---If you are already a part of our shopping mall, use options below to sign in---");
        Console.WriteLine("[3] - Customer");
        Console.WriteLine("[4] - Store Manager");
        Console.WriteLine("---Mall Department Only---");
        Console.WriteLine("[5] - Product Management");
      }
      Console.WriteLine("---You also can use this option below to search for:");
      Console.WriteLine("[9] - Customer");
      Console.WriteLine("-------------");
      Console.WriteLine("[0] - Exit");
      Console.WriteLine("What would you like to do?");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      //Switch cases are just useful if you are doing a bunch of comparision
      switch (_userInput)
      {
        case "0":
          return "Exit";
        case "S":
          if (ListCustomersMenu._currentCustomer.Name != "")
          {
            Log.Information($"{ListCustomersMenu._currentCustomer.Name} signed out as a Customer");
            ListCustomersMenu._currentCustomer.Name = "";
            return "MainMenu";
          }
          else if (ListStoresMenu._currentStoreFront.Name != "")
          {
            Log.Information($"{ListStoresMenu._currentStoreFront.Name} signed out as a Store Manager");
            ListStoresMenu._currentStoreFront.Name = "";
            return "MainMenu";
          }
          else
          {
            Log.Information("User is trying to Signout without Login!");
            goto default;
          }
        case "1":
          if (ListCustomersMenu._currentCustomer.Name != "")
          {
            return "ListCustomerOrdersMenu";
          }
          else if (ListStoresMenu._currentStoreFront.Name != "")
          {
            return "ListStoreOrdersMenu";
          }
          else
          {
            return "AddNewCustomer";
          }
        case "2":
          if (ListCustomersMenu._currentCustomer.Name != "")
          {
            return "PlaceNewOrderMenu";
          }
          else if (ListStoresMenu._currentStoreFront.Name != "")
          {
            return "InventoryMenu";
          }
          else
          {
            return "AddNewStoreFront";
          }

        case "3":
          if (ListCustomersMenu._currentCustomer.Name != "" || ListStoresMenu._currentStoreFront.Name != "")
          {
            Log.Information("Customer is logged in but tried to log in as another one");
            goto default;
          }
          else
          {
            return "ListCustomers";
          }
        case "4":
          if (ListCustomersMenu._currentCustomer.Name != "" || ListStoresMenu._currentStoreFront.Name != "")
          {

            Log.Information("Store manager is logged in but tried to log in as another one");
            goto default;
          }
          else
          {
            return "ListStores";
          }
        case "5":
          if (ListCustomersMenu._currentCustomer.Name != "" || ListStoresMenu._currentStoreFront.Name != "")
          {
            goto default;
          }
          else
          {
            return "MallMenu";
          }
        case "9":
          return "SearchCustomerMenu";
        default:
          Console.WriteLine("Please input a valid resonse!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "MainMenu";
      }
    }
  }
}