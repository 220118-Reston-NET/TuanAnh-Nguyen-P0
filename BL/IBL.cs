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
    Products GetProductDetail(string p_prodId);
  }

  public interface IOrderBL
  {
    Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID, int _totalPrice);
    List<Orders> GetAllOrdersByCustomerID(string p_cusID);
    List<Orders> GetAllOrdersByStoreID(string p_storeID);
    List<Orders> GetAllOrders();
  }

  public interface IInventoryBL
  {
    Inventory ImportProduct(Inventory p_prod);
    Inventory GetProductDetail(string p_prodId, string p_storeID);
    List<Inventory> GetAllProductsFromStore(string p_storeID);
    List<Products> GetAllInStockProductsDetailFromStore(string p_storeID);
    void ReplenishProduct(string p_invenID, int p_quantity);
  }
}