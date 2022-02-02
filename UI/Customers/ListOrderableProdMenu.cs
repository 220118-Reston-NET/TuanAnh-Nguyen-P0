using BL;
using Model;

namespace UI
{
  public class ListOrderableProdMenu : IMenu
  {
    private IInventoryBL _listInvenBL;
    private IOrderBL _orderBL;
    public ListOrderableProdMenu(IOrderBL p_order, IInventoryBL p_listInvenBL)
    {
      _orderBL = p_order;
      _listInvenBL = p_listInvenBL;
    }
    private static List<Products> _listProducts = new List<Products>();
    private static List<Inventory> _listInventorys = new List<Inventory>();
    private static List<LineItems> _cart = new List<LineItems>();
    private static LineItems _lineItem;
    public void DisplayAllProducts()
    {
      _listProducts = _listInvenBL.GetAllInStockProductsDetailFromStore(PlaceNewOrderMenu._selectedStore.StoreID);
      _listInventorys = _listInvenBL.GetAllProductsFromStore(PlaceNewOrderMenu._selectedStore.StoreID);
      int quantity = 0;
      if (_listProducts.Count() > 0)
      {
        Console.WriteLine("Here are all products in the :" + PlaceNewOrderMenu._selectedStore.Name);
        for (int i = 0; i < _listProducts.Count(); i++)
        {
          quantity = _listInventorys.Where(p => p.ProductID == _listProducts[i].ProductID).First().Quantity;
          Console.WriteLine("- " + _listProducts[i].Name + " - $" + _listProducts[i].Price + " (" + quantity + " left)");
        }
        Console.WriteLine("-----");
        Console.WriteLine("Please enter the product name that you want to buy. Ex: '" + _listProducts[0].Name + "'");
      }
      else
      {
        Console.WriteLine("The store have none products!");
      }
    }
    public void DisplayCart()
    {
      if (_cart.Count() == 0)
      {
        Console.WriteLine("Your Cart is empty!");
      }
      else
      {
        int _totalPrice = 0;
        Console.WriteLine("Your Cart:");
        for (int i = 0; i < _cart.Count(); i++)
        {
          Console.WriteLine(_cart[i].ProductName + " - $" + _cart[i].Price + "(" + _cart[i].Quantity + ")");
          _totalPrice += _cart[i].Price * _cart[i].Quantity;
        }
        Console.WriteLine("Total Price: $" + _totalPrice);
      }
    }

    public Boolean AddToCart(LineItems p_lineItems)
    {
      int quantity = _listInventorys.Where(p => p.ProductID == p_lineItems.ProductID).First().Quantity;
      if (_cart.Count() == 0)
      {
        _cart.Add(p_lineItems);
        return true;
      }
      else
      {
        int _currentQuantity = 0;
        for (int i = 0; i < _listProducts.Count(); i++)
        {
          if (_listProducts[i].ProductID == p_lineItems.ProductID)
          {
            _currentQuantity = quantity;
          }
        }

        for (int i = 0; i < _cart.Count(); i++)
        {
          if (_cart[i].ProductID == p_lineItems.ProductID)
          {
            if (_cart[i].Quantity + p_lineItems.Quantity > _currentQuantity)
            {
              return false;
            }
            else
            {
              _cart[i].Quantity += p_lineItems.Quantity;
              return true;
            }
          }
        }
        _cart.Add(p_lineItems);
        return true;
      }
    }
    public void Display()
    {
      DisplayAllProducts();
      DisplayCart();
      Console.WriteLine("[9] - Place Order");
      Console.WriteLine("[0] - Cancel & Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          _cart = new List<LineItems>();
          return "PlaceNewOrderMenu";

        case "9":
          if (_cart.Count() == 0)
          {
            Console.WriteLine("Your cart is empty! Please start shopping first!");
            Console.WriteLine("Please press Enter to continue");
            Console.ReadLine();
            return "ListOrderableProdMenu";
          }
          else
          {
            //Placing order
            Log.Information("Adding a new order: ");
            foreach (var item in _cart)
            {
              Log.Information("" + item);
            }
            Log.Information($"Under {ListCustomersMenu._currentCustomer.Name} to store: {PlaceNewOrderMenu._selectedStore.Name}");
            _orderBL.PlaceOrder(_cart, PlaceNewOrderMenu._selectedStore.StoreID, ListCustomersMenu._currentCustomer.CustomerID);
            Log.Information("Added new order successfully");
            Console.WriteLine("Placed Order successfully!");
            Console.WriteLine("Returning to the previous menu...");
            System.Threading.Thread.Sleep(2000);
            return "PlaceNewOrderMenu";
          }

        default:
          //Check if the input is existed in Name of the Product
          for (int i = 0; i < _listProducts.Count(); i++)
          {
            if (_userInput == _listProducts[i].Name)
            {
              //Check if the quantity customer want to buy is more than the instock products
              int quantity = _listInventorys.Where(p => p.ProductID == _listProducts[i].ProductID).First().Quantity;

              Console.WriteLine("How many do you want to buy:");
              string _userInputQuantity = Console.ReadLine();

              while (!_userInputQuantity.All(Char.IsDigit) || _userInputQuantity == "" || Convert.ToInt32(_userInputQuantity) > quantity || Convert.ToInt32(_userInputQuantity) == 0)
              {
                Console.WriteLine("The amount have to be a number, less than the current stock and should not be empty!");
                Console.WriteLine("Please enter again:");
                _userInputQuantity = Console.ReadLine();
              }

              _lineItem = new LineItems();
              _lineItem.ProductID = _listProducts[i].ProductID;
              _lineItem.ProductName = _listProducts[i].Name;
              _lineItem.Price = _listProducts[i].Price;
              _lineItem.Quantity = Convert.ToInt32(_userInputQuantity);

              if (AddToCart(_lineItem))
              {
                Console.WriteLine("Added to cart successfully!");
                System.Threading.Thread.Sleep(2000);
                return "ListOrderableProdMenu";
              }
              else
              {
                Console.WriteLine("Can not add to cart!");
                Console.WriteLine("The amount is more than what store have! Please try again!");
                System.Threading.Thread.Sleep(2000);
                return "ListOrderableProdMenu";
              }
            }
          }

          Console.WriteLine("Please input a valid response!");
          Console.WriteLine("Please press Enter to continue");
          Console.ReadLine();
          return "ListOrderableProdMenu";
      }
    }
  }
}