using System.Text.Json;
using Model;

namespace DL
{
  public class CustomerRepositoty : ICustomerRepository
  {
    private string _filepath = "../DL/Database/";
    private string _jsonString;
    public Customer AddCustomer(Customer p_cus)
    {
      string _path = _filepath + "Customer.json";
      List<Customer> _listCus = new List<Customer>();

      //Get a random ID for Customer
      p_cus.CustomerID = Guid.NewGuid().ToString();

      //Check if the JSON file is exists.
      if (File.Exists(_path))
      {
        //Check if the file have values
        if (new FileInfo(_path).Length == 0)
        {

          _listCus.Add(p_cus);
          _jsonString = JsonSerializer.Serialize(_listCus, new JsonSerializerOptions { WriteIndented = true });

          File.WriteAllText(_path, _jsonString);
        }
        else
        {
          string _jsonString2 = File.ReadAllText(_path);

          _listCus = JsonSerializer.Deserialize<List<Customer>>(_jsonString2);
          _listCus.Add(p_cus);

          _jsonString = JsonSerializer.Serialize(_listCus, new JsonSerializerOptions { WriteIndented = true });
          File.WriteAllText(_path, _jsonString);
        }
      }
      else
      {
        _listCus.Add(p_cus);
        _jsonString = JsonSerializer.Serialize(_listCus, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(_path, _jsonString);
      }

      return p_cus;
    }

    public List<Customer> GetALlCustomers()
    {
      string _path = _filepath + "Customer.json";
      List<Customer> _listCus = new List<Customer>();
      //Check if the JSON file is exists.
      if (File.Exists(_path))
      {
        //Check if the file have values
        if (new FileInfo(_path).Length == 0)
        {
          return _listCus;
        }
        else
        {
          string _jsonString2 = File.ReadAllText(_path);

          _listCus = JsonSerializer.Deserialize<List<Customer>>(_jsonString2);
          return _listCus;
        }
      }
      else
      {
        return _listCus;
      }
    }
  }

  public class StoreFrontRepository : IStoreFrontRepository
  {
    private string _filepath = "../DL/Database/";
    private string _jsonString;
    public StoreFront AddStoreFront(StoreFront p_storef)
    {
      string _path = _filepath + "StoreFront.json";
      List<StoreFront> _listStoreF = new List<StoreFront>();

      //Get a random ID for Customer
      p_storef.StoreID = Guid.NewGuid().ToString();

      //Check if the JSON file is exists.
      if (File.Exists(_path))
      {
        //Check if the file have values
        if (new FileInfo(_path).Length == 0)
        {
          _listStoreF.Add(p_storef);
          _jsonString = JsonSerializer.Serialize(_listStoreF, new JsonSerializerOptions { WriteIndented = true });

          File.WriteAllText(_path, _jsonString);
        }
        else
        {
          string _jsonString2 = File.ReadAllText(_path);

          _listStoreF = JsonSerializer.Deserialize<List<StoreFront>>(_jsonString2);
          _listStoreF.Add(p_storef);

          _jsonString = JsonSerializer.Serialize(_listStoreF, new JsonSerializerOptions { WriteIndented = true });
          File.WriteAllText(_path, _jsonString);
        }
      }
      else
      {
        _listStoreF.Add(p_storef);
        _jsonString = JsonSerializer.Serialize(_listStoreF, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(_path, _jsonString);
      }

      return p_storef;
    }

    public List<StoreFront> GetALlStoreFronts()
    {
      string _path = _filepath + "StoreFront.json";
      List<StoreFront> _listStoreF = new List<StoreFront>();
      //Check if the JSON file is exists.
      if (File.Exists(_path))
      {
        //Check if the file have values
        if (new FileInfo(_path).Length == 0)
        {
          return _listStoreF;
        }
        else
        {
          string _jsonString2 = File.ReadAllText(_path);

          _listStoreF = JsonSerializer.Deserialize<List<StoreFront>>(_jsonString2);
          return _listStoreF;
        }
      }
      else
      {
        return _listStoreF;
      }
    }
  }

  public class ProductRepository : IProductRepository
  {
    private string _filepath = "../DL/Database/";
    private string _jsonString;
    public Products AddProduct(Products p_prod)
    {
      string _path = _filepath + "Product.json";
      List<Products> _listProducts = new List<Products>();

      //Get a random ID for Product
      p_prod.ProductID = Guid.NewGuid().ToString();

      //Check if the JSON file is exists.
      if (File.Exists(_path))
      {
        //Check if the file have values
        if (new FileInfo(_path).Length == 0)
        {
          _listProducts.Add(p_prod);
          _jsonString = JsonSerializer.Serialize(_listProducts, new JsonSerializerOptions { WriteIndented = true });

          File.WriteAllText(_path, _jsonString);
        }
        else
        {
          string _jsonString2 = File.ReadAllText(_path);

          _listProducts = JsonSerializer.Deserialize<List<Products>>(_jsonString2);
          _listProducts.Add(p_prod);

          _jsonString = JsonSerializer.Serialize(_listProducts, new JsonSerializerOptions { WriteIndented = true });
          File.WriteAllText(_path, _jsonString);
        }
      }
      else
      {
        _listProducts.Add(p_prod);
        _jsonString = JsonSerializer.Serialize(_listProducts, new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(_path, _jsonString);
      }

      return p_prod;
    }

    public List<Products> GetAllProducts()
    {
      string _path = _filepath + "Product.json";
      List<Products> _listProds = new List<Products>();
      //Check if the JSON file is exists.
      if (File.Exists(_path))
      {
        //Check if the file have values
        if (new FileInfo(_path).Length == 0)
        {
          return _listProds;
        }
        else
        {
          string _jsonString2 = File.ReadAllText(_path);

          _listProds = JsonSerializer.Deserialize<List<Products>>(_jsonString2);
        }
      }
      else
      {
        return _listProds;
      }
      return _listProds;
    }

    public List<Products> GetAllProductsFromStore(string _storeID)
    {
      string _path = _filepath + "Product.json";
      List<Products> _listProds = new List<Products>();
      //Check if the JSON file is exists.
      if (File.Exists(_path))
      {
        //Check if the file have values
        if (new FileInfo(_path).Length == 0)
        {
          return _listProds;
        }
        else
        {
          string _jsonString2 = File.ReadAllText(_path);

          _listProds = JsonSerializer.Deserialize<List<Products>>(_jsonString2);
          //Get only the Product that have the given storeID
          List<Products> _selectedProducts = new List<Products>();
          for (int i = 0; i < _listProds.Count(); i++)
          {
            if (_listProds[i].StoreID == _storeID)
            {
              _selectedProducts.Add(_listProds[i]);
            }
          }

          return _selectedProducts;
        }
      }
      else
      {
        return _listProds;
      }
    }

    public Products SaveProduct(Products p_prod)
    {
      string _path = _filepath + "Product.json";
      List<Products> _listPreProds = new List<Products>();
      //Check if the JSON file is exists.
      if (File.Exists(_path))
      {
        //Check if the file have values
        if (new FileInfo(_path).Length == 0)
        {
          return p_prod;
        }
        else
        {
          string _jsonString2 = File.ReadAllText(_path);

          _listPreProds = JsonSerializer.Deserialize<List<Products>>(_jsonString2);
          //Get only the Product that have the given storeID
          // List<Products> _selectedProducts = new List<Products>();
          for (int i = 0; i < _listPreProds.Count(); i++)
          {
            if (_listPreProds[i].ProductID == p_prod.ProductID)
            {
              _listPreProds[i].Name = p_prod.Name;
              _listPreProds[i].Quantity = p_prod.Quantity;
              _listPreProds[i].Price = p_prod.Price;
              _listPreProds[i].Desc = p_prod.Desc;
              break;
            }
          }
          _jsonString2 = JsonSerializer.Serialize(_listPreProds, new JsonSerializerOptions { WriteIndented = true });
          File.WriteAllText(_path, _jsonString2);
          return p_prod;
        }
      }
      else
      {
        return p_prod;
      }
    }

    public void SubtractProduct(string p_pID, int p_quantity)
    {
      string _path = _filepath + "Product.json";

      List<Products> _listProds = GetAllProducts();

      for (int i = 0; i < _listProds.Count(); i++)
      {
        if (_listProds[i].ProductID == p_pID)
        {
          _listProds[i].Quantity -= p_quantity;
          break;
        }
      }

      string _jsonString2 = JsonSerializer.Serialize(_listProds, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText(_path, _jsonString2);
    }
  }

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