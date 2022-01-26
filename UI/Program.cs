using UI;
using BL;
using DL;

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
      menu = new ListOrderableProdMenu(new OrderBL(new OrderRepository()), new ProductBL(new ProductRepository()));
      break;
    case "ListCustomerOrdersMenu":
      menu = new ListCustomerOrdersMenu(new OrderBL(new OrderRepository()));
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
    case "ListStoreOrdersMenu":
      menu = new ListStoreOrdersMenu(new OrderBL(new OrderRepository()));
      break;

    //Search Menu
    case "SearchCustomerMenu":
      menu = new SearchCustomerMenu(new CustomerBL(new CustomerRepositoty()));
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
