global using Serilog;
using UI;
using BL;
using DL;

Log.Logger = new LoggerConfiguration().WriteTo.File("./logs/user.txt").CreateLogger();

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
      Log.Information("Displaying the Add New Customer Menu");
      menu = new AddCustomerMenu(new CustomerBL(new CustomerRepositoty()));
      break;
    case "AddNewStoreFront":
      Log.Information("Displaying the Add New StoreFront Menu");
      menu = new AddNewStoreFrontMenu(new StoreFrontBL(new StoreFrontRepository()));
      break;
    //Sign In Options
    case "ListStores":
      Log.Information("Displaying the List of Stores Menu");
      menu = new ListStoresMenu(new StoreFrontBL(new StoreFrontRepository()));
      break;
    case "ListCustomers":
      Log.Information("Displaying the List Of Customers Menu");
      menu = new ListCustomersMenu(new CustomerBL(new CustomerRepositoty()));
      break;

    //Customer Options after signed in
    case "PlaceNewOrderMenu":
      Log.Information("Displaying the Place New Order Menu");
      menu = new PlaceNewOrderMenu(new StoreFrontBL(new StoreFrontRepository()));
      break;
    case "ListOrderableProdMenu":
      Log.Information("Displaying the List All The Available Product to Order Menu");
      menu = new ListOrderableProdMenu(new OrderBL(new OrderRepository()), new ProductBL(new ProductRepository()));
      break;
    case "ListCustomerOrdersMenu":
      Log.Information("Displaying the List All Customer Orders Menu");
      menu = new ListCustomerOrdersMenu(new OrderBL(new OrderRepository()));
      break;

    //StoreFront Options after signed in
    case "InventoryMenu":
      Log.Information("Displaying the Inventory Menu");
      menu = new InventoryMenu();
      break;
    case "AddNewProduct":
      Log.Information("Displaying the Add New Product Menu");
      menu = new AddNewProductMenu(new ProductBL(new ProductRepository()));
      break;
    case "ReplenishInventory":
      Log.Information("Displaying the Replenish Inventory Menu");
      menu = new ReplenishMenu(new ProductBL(new ProductRepository()));
      break;
    case "EditProduct":
      Log.Information("Displaying the Edit Product Menu");
      menu = new EditProductMenu(new ProductBL(new ProductRepository()));
      break;
    case "ListStoreOrdersMenu":
      Log.Information("Displaying the List of All Stores Menu");
      menu = new ListStoreOrdersMenu(new OrderBL(new OrderRepository()));
      break;

    //Search Menu
    case "SearchCustomerMenu":
      Log.Information("Displaying the Search Customer Menu");
      menu = new SearchCustomerMenu(new CustomerBL(new CustomerRepositoty()));
      break;

    //Main Menu
    case "MainMenu":
      Log.Information("Displaying the Main Menu");
      menu = new MainMenu();
      break;

    //Exit
    case "Exit":
      Log.Information("Exiting application!");
      Log.CloseAndFlush();
      repeat = false;
      break;

    default:
      Console.WriteLine("Page does not exist!");
      break;
  }
}
