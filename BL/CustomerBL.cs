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
      return _repo.AddCustomer(p_cus);
    }

    public List<Customer> GetALlCustomers()
    {
      return _repo.GetALlCustomers();
    }

    public List<Customer> SearchCustomersByName(string p_cusName)
    {
      List<Customer> _filteredList = new List<Customer>();
      _filteredList = _repo.GetALlCustomers().Where(cus => cus.Name.Contains(p_cusName)).ToList();
      return _filteredList;
    }
  }
}