using Model;

namespace BL
{
  public interface ICustomerBL
  {
    Customer AddCustomer(Customer p_cus);
    Customer SaveCustomer(Customer p_cus);
    List<Customer> GetALlCustomers();
    Customer GetCustomerInfoByID(string p_cusID);
    List<Customer> SearchCustomersByName(string p_cusName);
  }

  public interface IStoreFrontBL
  {
    StoreFront AddStoreFront(StoreFront p_storef);
    StoreFront SaveStoreFront(StoreFront p_storef);
    List<StoreFront> GetALlStoreFronts();
    StoreFront GetStoreFrontInfoByID(string p_storeID);
  }

  public interface IProductBL
  {
    Products AddProduct(Products p_prod);
    Products SaveProduct(Products p_prod);
    List<Products> GetAllProducts();
    List<Products> GetAllProductsFromStore(string p_storeID);
    Products GetProductDetail(string p_prodId);
    List<Products> GetAllProductsByProductName(string p_prodName);
    List<StoreFront> GetAllStoreFrontsByProductID(string p_prodID);
  }

  public interface IOrderBL
  {
    Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID, int _totalPrice);
    List<Orders> GetAllOrdersByCustomerID(string p_cusID);
    List<Orders> GetAllOrdersByStoreID(string p_storeID);
    Orders GetOrderByOrderID(string p_orderID);
    List<Orders> GetAllOrdersByCustomerIDWithFilter(string p_cusID, string p_filter);
    List<Orders> GetAllOrdersByStoreIDWithFilter(string p_storeID, string p_filter);
    List<Orders> GetAllOrders();
    void UpdateOrderDetail(string p_orderID, string p_status);
    Shipment AddNewTrackingNumber(string p_orderID, string p_trackingNo);
    void RemoveAllTrackingByOrderID(string p_orderID);
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