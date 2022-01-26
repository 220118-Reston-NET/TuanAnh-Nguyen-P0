using BL;
using Model;

namespace UI
{
  public class ListOrderableProdMenu : IMenu
  {
    private IProductBL _listProdBL;
    public ListOrderableProdMenu(IProductBL p_listProdBL)
    {
      _listProdBL = p_listProdBL;
    }
    private List<Products> _listProducts;
    private static List<LineItems> _cart = new List<LineItems>();
    private static LineItems _lineItem;
    public void DisplayAllProducts()
    {
      _listProducts = _listProdBL.GetAllProductsFromStore(PlaceNewOrderMenu._selectedStore.StoreID);
      if (_listProducts.Count() > 0)
      {
        Console.WriteLine("Here are all products in the :" + PlaceNewOrderMenu._selectedStore.Name);
        for (int i = 0; i < _listProducts.Count(); i++)
        {
          Console.WriteLine("- " + _listProducts[i].Name + " - $" + _listProducts[i].Price + " (" + _listProducts[i].Quantity + " left)");
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
          Console.WriteLine(_cart[i].ProductName + " - " + _cart[i].Quantity);
        }
        //TODO Here Total Price
        Console.WriteLine("Total Price: " + _totalPrice);
      }
    }
    public void Display()
    {
      DisplayAllProducts();
      DisplayCart();
      Console.WriteLine("[9] - Place Order");
      Console.WriteLine("[0] - Go back");
    }

    public string UserChoice()
    {
      string _userInput = Console.ReadLine();

      switch (_userInput)
      {
        case "0":
          return "PlaceNewOrderMenu";

        case "9":
          //TODO Add Func in BL
          return "PlaceNewOrderMenu";

        default:
          //Check if the input is existed in Name of the Product
          for (int i = 0; i < _listProducts.Count(); i++)
          {
            if (_userInput == _listProducts[i].Name)
            {
              //Check if the quantity customer want to buy is more than the instock products

              Console.WriteLine("How many do you want to buy:");
              string _userInputQuantity = Console.ReadLine();

              while (!_userInputQuantity.All(Char.IsDigit) || _userInputQuantity == "" || Convert.ToInt32(_userInputQuantity) > _listProducts[i].Quantity)
              {
                Console.WriteLine("The amount have to be a number, less than the current stock and should not be empty!");
                Console.WriteLine("Please enter again:");
                _userInputQuantity = Console.ReadLine();
              }

              //TODO check duplicate LineItems -> Subtract the current left in product too
              _lineItem = new LineItems();
              _lineItem.ProductID = _listProducts[i].ProductID;
              _lineItem.ProductName = _listProducts[i].Name;
              _lineItem.Quantity = Convert.ToInt32(_userInputQuantity);

              _cart.Add(_lineItem);

              Console.WriteLine("Adding product to cart...");
              System.Threading.Thread.Sleep(2000);
              return "ListOrderableProdMenu";
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