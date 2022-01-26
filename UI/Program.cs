// See https://aka.ms/new-console-template for more information
using Model;
using UI;
using BL;
using DL;

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
    //Sign Up Options
    case "AddNewCustomer":
      menu = new AddCustomerMenu(new CustomerBL(new CustomerRepositoty()));
      break;
    case "AddNewStoreFront":
      menu = new AddNewStoreFrontMenu(new StoreFrontBL(new StoreFrontRepository()));
      break;
    //Sign In Options
    case "ListStores":
      menu = new ListStoresMenu(new StoreFrontBL(new StoreFrontRepository()));
      break;
    case "ListCustomers":
      menu = new ListCustomersMenu(new CustomerBL(new CustomerRepositoty()));
      break;

    //Customer Options after signed in
    case "PlaceNewOrderMenu":
      menu = new PlaceNewOrderMenu(new StoreFrontBL(new StoreFrontRepository()));
      break;
    case "ListOrderableProdMenu":
      menu = new ListOrderableProdMenu(new ProductBL(new ProductRepository()));
      break;

    //StoreFront Options after signed in
    case "InventoryMenu":
      menu = new InventoryMenu();
      break;
    case "AddNewProduct":
      menu = new AddNewProductMenu(new ProductBL(new ProductRepository()));
      break;
    case "ReplenishInventory":
      menu = new ReplenishMenu(new ProductBL(new ProductRepository()));
      break;
    case "EditProduct":
      menu = new EditProductMenu(new ProductBL(new ProductRepository()));
      break;





    //Customers Options Menu

    case "SearchCustomer":
      menu = new SearchCustomerMenu();
      break;

    case "SearchStore":
      menu = new SearchStoreMenu();
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
