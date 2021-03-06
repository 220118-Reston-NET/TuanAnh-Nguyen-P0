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

      //Get Date Time when created 
      DateTime _createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
      p_prod.createdAt = _createdAt;

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

    public List<Products> GetAllProductsFromStore(string p_storeID)
    {
      throw new NotImplementedException();
    }

    public List<StoreFront> GetAllStoreFrontsByProductID(string p_prodID)
    {
      throw new NotImplementedException();
    }

    public Products GetProductDetailByProductId(string p_prodID)
    {
      throw new NotImplementedException();
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

          for (int i = 0; i < _listPreProds.Count(); i++)
          {
            if (_listPreProds[i].ProductID == p_prod.ProductID)
            {
              _listPreProds[i].Name = p_prod.Name;
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
  }
}