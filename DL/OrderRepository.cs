using System.Text.Json;
using Model;

namespace DL
{
  public class OrderRepository : IOrderRepository
  {
    private string _filepath = "../DL/Database/";
    private string _jsonString;

    public List<Orders> GetAllOrders()
    {
      string _path = _filepath + "Orders.json";
      string _jsonString = File.ReadAllText(_path);
      List<Orders> _listOrders = new List<Orders>();

      _listOrders = JsonSerializer.Deserialize<List<Orders>>(_jsonString);

      return _listOrders;
    }

    public List<Orders> GetAllOrdersByCustomerID(string p_cusID)
    {
      List<Orders> _listOrders = GetAllOrders();
      List<Orders> _customerOrders = new List<Orders>();

      for (int i = 0; i < _listOrders.Count(); i++)
      {
        if (_listOrders[i].CustomerID == p_cusID)
        {
          _customerOrders.Add(_listOrders[i]);
        }
      }

      return _customerOrders;
    }

    public List<Orders> GetAllOrdersByStoreID(string p_storeID)
    {
      List<Orders> _listOrders = GetAllOrders();
      List<Orders> _storeOrders = new List<Orders>();

      for (int i = 0; i < _listOrders.Count(); i++)
      {
        if (_listOrders[i].StoreID == p_storeID)
        {
          _storeOrders.Add(_listOrders[i]);
        }
      }

      return _storeOrders;
    }

    public Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID)
    {
      string _path = _filepath + "Orders.json";
      List<Orders> _listOrders = new List<Orders>();
      Orders _newOrder = new Orders();

      //Get a random ID for Order
      _newOrder.OrderID = Guid.NewGuid().ToString();
      //Get Date Time when place order
      string _orderDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time")).ToString();
      _newOrder.OrderDate = _orderDate.ToString();

      _newOrder.CustomerID = _customerID;
      _newOrder.StoreID = _storeID;
      _newOrder.ListLineItems = p_lineItems;

      int _totalPrice = 0;
      ProductRepository _subtract = new ProductRepository();
      for (int i = 0; i < p_lineItems.Count(); i++)
      {
        _totalPrice += p_lineItems[i].Price * p_lineItems[i].Quantity;
        _subtract.SubtractProduct(p_lineItems[i].ProductID, p_lineItems[i].Quantity);
      }
      _newOrder.TotalPrice = _totalPrice;

      //Check if the JSON file is exists.
      if (File.Exists(_path))
      {
        //Check if the file have values
        if (new FileInfo(_path).Length == 0)
        {
          _listOrders.Add(_newOrder);
          _jsonString = JsonSerializer.Serialize(_listOrders, new JsonSerializerOptions { WriteIndented = true });

          File.WriteAllText(_path, _jsonString);
        }
        else
        {
          string _jsonString2 = File.ReadAllText(_path);

          _listOrders = JsonSerializer.Deserialize<List<Orders>>(_jsonString2);
          _listOrders.Add(_newOrder);

          _jsonString = JsonSerializer.Serialize(_listOrders, new JsonSerializerOptions { WriteIndented = true });
          File.WriteAllText(_path, _jsonString);
        }
      }
      else
      {
        _listOrders.Add(_newOrder);
        _jsonString = JsonSerializer.Serialize(_listOrders, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(_path, _jsonString);
      }

      return _newOrder;
    }
  }
}