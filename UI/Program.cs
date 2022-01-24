// See https://aka.ms/new-console-template for more information
using Model;
using UI;

// Console.WriteLine("Hello, World!");

// Customer cus = new Customer();
// cus.Name = "A"; //Validation is working since can't input a empty string

bool repeat = true;
IMenu menu = new MainMenu();

while (repeat)
{
  Console.Clear();

  menu.Display();
  string ans = menu.UserChoice();

  switch (ans)
  {
    //Customers Options Menu
    case "CustomersMenu":
      menu = new CustomersMenu();
      break;
    case "AddNewCustomer":
      menu = new AddCustomerMenu();
      break;
    case "SearchCustomer":
      menu = new SearchCustomerMenu();
      break;

    //Stores Options Menu
    case "StoresMenu":
      menu = new StoresMenu();
      break;
    case "SearchStore":
      menu = new SearchStoreMenu();
      break;
    case "ListStores":
      menu = new ListStores();
      break;

    //Customer/Store Manager
    case "CustomerManager":
      menu = new CustomerManager();
      break;
    case "StoreManager":
      menu = new StoreManger();
      break;

    //Main Menu
    case "MainMenu":
      menu = new MainMenu();
      break;

    //Exit
    case "Exit":
      repeat = false;
      break;

    default:
      Console.WriteLine("Page does not exist!");
      break;
  }
}
