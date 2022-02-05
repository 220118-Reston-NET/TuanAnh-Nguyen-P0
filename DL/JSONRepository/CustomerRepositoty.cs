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

      //Get Date Time when created 
      DateTime _createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
      p_cus.createdAt = _createdAt;

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

    public Customer GetCustomerInfoByID(string p_cusID)
    {
      throw new NotImplementedException();
    }

    public Customer SaveCustomer(Customer p_cus)
    {
      throw new NotImplementedException();
    }
  }
}