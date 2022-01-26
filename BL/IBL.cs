using Model;

namespace BL
{
  public interface ICustomerBL
  {
    Customer AddCustomer(Customer p_cus);
    List<Customer> GetALlCustomers();
    List<Customer> SearchCustomersByName(string p_cusName);
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
    List<Products> GetAllProducts();
    List<Products> GetAllProductsFromStore(string _storeID);
    void SubtractProduct(string p_pID, int p_quantity, string _storeID);
  }

  public interface IOrderBL
  {
    Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID);
    List<Orders> GetAllOrdersByCustomerID(string p_cusID);
    List<Orders> GetAllOrdersByStoreID(string p_storeID);
    List<Orders> GetAllOrders();
  }
}