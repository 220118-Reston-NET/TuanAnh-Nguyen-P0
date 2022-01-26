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
  }

  public class StoreFrontBL : IStoreFrontBL
  {
    private IStoreFrontRepository _repo;
    public StoreFrontBL(IStoreFrontRepository p_repo)
    {
      _repo = p_repo;
    }
    public StoreFront AddStoreFront(StoreFront p_storef)
    {
      return _repo.AddStoreFront(p_storef);
    }

    public List<StoreFront> GetALlStoreFronts()
    {
      return _repo.GetALlStoreFronts();
    }
  }

  public class ProductBL : IProductBL
  {
    private IProductRepository _repo;
    public ProductBL(IProductRepository p_repo)
    {
      _repo = p_repo;
    }
    public Products AddProduct(Products p_prod)
    {
      return _repo.AddProduct(p_prod);
    }

    public List<Products> GetAllProductsFromStore(string _storeID)
    {
      return _repo.GetAllProductsFromStore(_storeID);
    }

    public Products SaveProduct(Products p_prod)
    {
      return _repo.SaveProduct(p_prod);
    }
  }
}