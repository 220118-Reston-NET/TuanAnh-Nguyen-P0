global using Serilog;
using Microsoft.Extensions.Configuration;
using UI;
using BL;
using DL;

Log.Logger = new LoggerConfiguration().WriteTo.File("./logs/user.txt").CreateLogger();

var configuration = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json")
                        .Build();

string _connectionString = configuration.GetConnectionString("Reference2DB");

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
      menu = new AddCustomerMenu(new CustomerBL(new CustomerSQLRepository(_connectionString)));
      break;
    case "AddNewStoreFront":
      Log.Information("Displaying the Add New StoreFront Menu");
      menu = new AddNewStoreFrontMenu(new StoreFrontBL(new StoreFrontSQLRepository(_connectionString)));
      break;
    //Sign In Options
    case "ListStores":
      Log.Information("Displaying the List of Stores Menu");
      menu = new ListStoresMenu(new StoreFrontBL(new StoreFrontSQLRepository(_connectionString)));
      break;
    case "ListCustomers":
      Log.Information("Displaying the List Of Customers Menu");
      menu = new ListCustomersMenu(new CustomerBL(new CustomerSQLRepository(_connectionString)));
      break;

    //Customer Options after signed in
    case "PlaceNewOrderMenu":
      Log.Information("Displaying the Place New Order Menu");
      menu = new PlaceNewOrderMenu(new StoreFrontBL(new StoreFrontSQLRepository(_connectionString)));
      break;
    case "ListOrderableProdMenu":
      Log.Information("Displaying the List All The Available Product to Order Menu");
      menu = new ListOrderableProdMenu(new OrderBL(new OrderSQLRepository(_connectionString)), new InventoryBL(new InventorySQLRepository(_connectionString)));
      break;
    case "ListCustomerOrdersMenu":
      Log.Information("Displaying the List All Customer Orders Menu");
      menu = new ListCustomerOrdersMenu(new OrderBL(new OrderSQLRepository(_connectionString)));
      break;

    //StoreFront Options after signed in
    case "InventoryMenu":
      Log.Information("Displaying the Inventory Menu");
      menu = new InventoryMenu();
      break;
    case "ImportNewProduct":
      Log.Information("Displaying the Import New Product Menu");
      menu = new ListAllMallProdMenu(new ProductBL(new ProductSQLRepository(_connectionString)), new InventoryBL(new InventorySQLRepository(_connectionString)));
      break;
    case "ReplenishInventory":
      Log.Information("Displaying the Replenish Inventory Menu");
      menu = new ReplenishMenu(new ProductBL(new ProductSQLRepository(_connectionString)), new InventoryBL(new InventorySQLRepository(_connectionString)));
      break;
    case "EditProduct":
      Log.Information("Displaying the Replenish Product Menu");
      menu = new EditProductMenu(new ProductBL(new ProductSQLRepository(_connectionString)), new InventoryBL(new InventorySQLRepository(_connectionString)));
      break;
    case "ListStoreOrdersMenu":
      Log.Information("Displaying the List of All Stores Menu");
      menu = new ListStoreOrdersMenu(new OrderBL(new OrderSQLRepository(_connectionString)));
      break;

    //Mall Department
    case "MallMenu":
      Log.Information("Displaying the Mall Department Menu");
      menu = new MallMenu();
      break;
    case "AddNewProductMallMenu":
      Log.Information("Displaying the Add New Product from Mall Department Menu");
      menu = new AddNewProductMallMenu(new ProductBL(new ProductSQLRepository(_connectionString)));
      break;
    case "ListAllMallProductsMenu":
      Log.Information("Displaying the All Products from Mall Department Menu");
      menu = new ListAllMallProductMenu(new ProductBL(new ProductSQLRepository(_connectionString)), new InventoryBL(new InventorySQLRepository(_connectionString)));
      break;
    case "EditProductMallMenu":
      Log.Information("Displaying the Product Modifier from Mall Department Menu");
      menu = new EditProductMallMenu(new ProductBL(new ProductSQLRepository(_connectionString)));
      break;

    //Search Menu
    case "SearchCustomerMenu":
      Log.Information("Displaying the Search Customer Menu");
      menu = new SearchCustomerMenu(new CustomerBL(new CustomerSQLRepository(_connectionString)));
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
