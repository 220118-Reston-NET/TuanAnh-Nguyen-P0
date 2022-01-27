using System.Text.Json;
using Model;

namespace DL
{
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
}