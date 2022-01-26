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

    public List<Products> GetAllProducts()
    {
      return _repo.GetAllProducts();
    }

    public List<Products> GetAllProductsFromStore(string _storeID)
    {
      return _repo.GetAllProductsFromStore(_storeID);
    }

    public Products SaveProduct(Products p_prod)
    {
      return _repo.SaveProduct(p_prod);
    }

    public void SubtractProduct(string p_pID, int p_quantity, string _storeID) { }
  }

  public class OrderBL : IOrderBL
  {
    private IOrderRepository _repo;
    public OrderBL(IOrderRepository p_repo)
    {
      _repo = p_repo;
    }

    public List<Orders> GetAllOrders()
    {
      return _repo.GetAllOrders();
    }

    public List<Orders> GetAllOrdersByCustomerID(string p_cusID)
    {
      return _repo.GetAllOrdersByCustomerID(p_cusID);
    }

    public List<Orders> GetAllOrdersByStoreID(string p_storeID)
    {
      return _repo.GetAllOrdersByStoreID(p_storeID);
    }

    public Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID)
    {
      return _repo.PlaceOrder(p_lineItems, _storeID, _customerID);
    }
  }
}