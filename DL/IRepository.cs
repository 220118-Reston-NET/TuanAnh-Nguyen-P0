using Model;

namespace DL
{
  public interface ICustomerRepository
  {
    Customer AddCustomer(Customer p_cus);
    List<Customer> GetALlCustomers();
  }
  public interface IStoreFrontRepository
  {
    StoreFront AddStoreFront(StoreFront p_storef);
    List<StoreFront> GetALlStoreFronts();
  }

  public interface IProductRepository
  {
    Products AddProduct(Products p_prod);
    Products SaveProduct(Products p_prod);
    List<Products> GetAllProducts();
    void SubtractProduct(string p_pID, int p_quantity);
  }

  public interface IOrderRepository
  {
    Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID);
    List<Orders> GetAllOrders();
  }
}