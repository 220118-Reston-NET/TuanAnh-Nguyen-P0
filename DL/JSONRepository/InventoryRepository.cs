using System.Text.Json;
using Model;

namespace DL
{
  public class InventoryRepository : IInventoryRepository
  {
    private string _filepath = "../DL/Database/";
    private string _jsonString;
    private ProductRepository _prodRe = new ProductRepository();

    public List<Products> GetAllInStockProductsDetailFromStore(string p_storeID)
    {
      List<Inventory> _listInvens = GetAllProductsFromStore(p_storeID);
      List<Products> _listProds = _prodRe.GetAllProducts();
      List<Products> _listStoreProd = new List<Products>();

      foreach (var inven in _listInvens)
      {
        for (int i = 0; i < _listProds.Count(); i++)
        {
          if (_listProds[i].ProductID == inven.ProductID && inven.Quantity > 0)
          {
            _listStoreProd.Add(_listProds[i]);
          }
        }
      }

      return _listStoreProd;
    }

    public List<Inventory> GetAllProducts()
    {
      string _path = _filepath + "Inventory.json";
      List<Inventory> _listProds = new List<Inventory>();
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

          _listProds = JsonSerializer.Deserialize<List<Inventory>>(_jsonString2);
        }
      }
      else
      {
        return _listProds;
      }
      return _listProds;
    }

    public List<Inventory> GetAllProductsFromStore(string p_storeID)
    {
      string _path = _filepath + "Inventory.json";
      List<Inventory> _listProds = new List<Inventory>();
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

          _listProds = JsonSerializer.Deserialize<List<Inventory>>(_jsonString2);

          _listProds = _listProds.Where(p => p.StoreID == p_storeID).ToList();
        }
      }
      else
      {
        return _listProds;
      }
      return _listProds;
    }

    public Inventory ImportProduct(Inventory p_inven)
    {
      string _path = _filepath + "Inventory.json";
      List<Inventory> _listInvens = new List<Inventory>();

      //Get a random ID for Inventory
      p_inven.InventoryID = Guid.NewGuid().ToString();

      _listInvens = GetAllProducts();
      _listInvens.Add(p_inven);

      _jsonString = JsonSerializer.Serialize(_listInvens, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText(_path, _jsonString);

      return p_inven;
    }

    public void ReplenishProduct(string p_invenID, int p_quantity)
    {
      string _path = _filepath + "Inventory.json";
      List<Inventory> _listProds = GetAllProducts();
      Inventory _filterInven = new Inventory();

      for (int i = 0; i < _listProds.Count(); i++)
      {
        if (_listProds[i].InventoryID == p_invenID)
        {
          _listProds[i].Quantity += p_quantity;
          _filterInven = _listProds[i];
          break;
        }
      }

      string _jsonString2 = JsonSerializer.Serialize(_listProds, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText(_path, _jsonString2);
    }

    public void SubtractProduct(string p_pID, string p_storeID, int p_quantity)
    {
      string _path = _filepath + "Inventory.json";
      List<Inventory> _listProds = GetAllProducts();

      for (int i = 0; i < _listProds.Count(); i++)
      {
        if (_listProds[i].ProductID == p_pID && _listProds[i].StoreID == p_storeID)
        {
          _listProds[i].Quantity -= p_quantity;
          break;
        }
      }

      string _jsonString2 = JsonSerializer.Serialize(_listProds, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText(_path, _jsonString2);
    }
  }
}