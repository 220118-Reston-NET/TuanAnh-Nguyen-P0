using Model;

namespace BL
{
  public interface ICustomerBL
  {
    Customer AddCustomer(Customer p_cus);
    List<Customer> GetALlCustomers();
  }

  public interface IStoreFrontBL
  {
    StoreFront AddStoreFront(StoreFront p_storef);
    List<StoreFront> GetALlStoreFronts();
  }

  public interface IProductBL
  {
    Products AddProduct(Products p_prod);
    Products SaveProduct(Products p_prod);
    List<Products> GetAllProductsFromStore(string _storeID);
  }
}