using System.Text.Json;
using Model;

namespace DL
{
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
}