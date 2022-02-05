using DL;
using Model;

namespace BL
{
  public class CustomerBL : ICustomerBL
  {
    private ICustomerRepository _repo;
    public CustomerBL(ICustomerRepository p_repo)
    {
      _repo = p_repo;
    }

    public Customer AddCustomer(Customer p_cus)
    {
      List<Customer> _listCustomers = _repo.GetALlCustomers();

      for (int i = 0; i < _listCustomers.Count(); i++)
      {
        if (_listCustomers[i].Name == p_cus.Name)
        {
          throw new Exception("Cannot add new customer due to this name is already in the database!");
        }
      }

      return _repo.AddCustomer(p_cus);
      // if (_listCustomers.All(cus => cus.Name != p_cus.Name)) {}
    }

    public List<Customer> GetALlCustomers()
    {
      return _repo.GetALlCustomers();
    }

    public Customer GetCustomerInfoByID(string p_cusID)
    {
      Customer _cusDetail = GetALlCustomers().Where(p => p.CustomerID == p_cusID).First();

      return _cusDetail;
    }

    public Customer SaveCustomer(Customer p_cus)
    {
      List<Customer> _listCus = GetALlCustomers().Where(p => p.CustomerID != p_cus.CustomerID).ToList();
      if (_listCus.Any(p => p.Name == p_cus.Name))
      {
        throw new Exception("Cannot save customer due to name of customer is already in the store database!");
      }
      return _repo.SaveCustomer(p_cus);
    }

    public List<Customer> SearchCustomersByName(string p_cusName)
    {
      List<Customer> _filteredList = new List<Customer>();
      _filteredList = _repo.GetALlCustomers().Where(cus => cus.Name.Contains(p_cusName)).ToList();
      return _filteredList;
    }
  }
}