using Model;

namespace BL
{
  public interface ICustomerBL
  {
    /// <summary>
    /// Will add a new customer to the database
    /// </summary>
    /// <param name="p_cus"></param>
    /// <returns></returns>
    Customer AddCustomer(Customer p_cus);

    /// <summary>
    /// Will save the modified information of customer to the database
    /// </summary>
    /// <param name="p_cus"></param>
    /// <returns></returns>
    Customer SaveCustomer(Customer p_cus);

    /// <summary>
    /// Will get all customers in the database
    /// </summary>
    /// <returns></returns>
    List<Customer> GetALlCustomers();

    /// <summary>
    /// Will get customer information by their ID
    /// </summary>
    /// <param name="p_cusID"></param>
    /// <returns></returns>
    Customer GetCustomerInfoByID(string p_cusID);

    /// <summary>
    /// Will search for all customer that have name matched with the input name
    /// </summary>
    /// <param name="p_cusName"></param>
    /// <returns></returns>
    List<Customer> SearchCustomersByName(string p_cusName);
  }

  public interface IStoreFrontBL
  {
    /// <summary>
    /// Will add a new store to the database
    /// </summary>
    /// <param name="p_storef"></param>
    /// <returns></returns>
    StoreFront AddStoreFront(StoreFront p_storef);

    /// <summary>
    /// Will save the modified information of the store to the database
    /// </summary>
    /// <param name="p_storef"></param>
    /// <returns></returns>
    StoreFront SaveStoreFront(StoreFront p_storef);

    /// <summary>
    /// Will get all stores in the database
    /// </summary>
    /// <returns></returns>
    List<StoreFront> GetALlStoreFronts();

    /// <summary>
    /// Will get the information of the store by their ID
    /// </summary>
    /// <param name="p_storeID"></param>
    /// <returns></returns>
    StoreFront GetStoreFrontInfoByID(string p_storeID);
  }

  public interface IProductBL
  {
    /// <summary>
    /// Will add new product to the database
    /// </summary>
    /// <param name="p_prod"></param>
    /// <returns></returns>
    Products AddProduct(Products p_prod);

    /// <summary>
    /// Will save the modified information of the product to the database
    /// </summary>
    /// <param name="p_prod"></param>
    /// <returns></returns>
    Products SaveProduct(Products p_prod);

    /// <summary>
    /// Will get all products in the database
    /// </summary>
    /// <returns></returns>
    List<Products> GetAllProducts();

    /// <summary>
    /// Will get all the products that belongs to the store
    /// </summary>
    /// <param name="p_storeID"></param>
    /// <returns></returns>
    List<Products> GetAllProductsFromStore(string p_storeID);

    /// <summary>
    /// Will get the product information by its ID
    /// </summary>
    /// <param name="p_prodId"></param>
    /// <returns></returns>
    Products GetProductDetail(string p_prodId);

    /// <summary>
    /// Will get all the product informations by the name
    /// </summary>
    /// <param name="p_prodName"></param>
    /// <returns></returns>
    List<Products> GetAllProductsByProductName(string p_prodName);

    /// <summary>
    /// Will get all of the stores that have the product in the inventory
    /// </summary>
    /// <param name="p_prodID"></param>
    /// <returns></returns>
    List<StoreFront> GetAllStoreFrontsByProductID(string p_prodID);
  }

  public interface IOrderBL
  {
    /// <summary>
    /// Will place a new order, add all the information to the database
    /// </summary>
    /// <param name="p_lineItems"></param>
    /// <param name="_storeID"></param>
    /// <param name="_customerID"></param>
    /// <param name="_totalPrice"></param>
    /// <returns></returns>
    Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID, int _totalPrice);

    /// <summary>
    /// Will get all the orders that belong to the customer by their ID
    /// </summary>
    /// <param name="p_cusID"></param>
    /// <returns></returns>
    List<Orders> GetAllOrdersByCustomerID(string p_cusID);

    /// <summary>
    /// Will get all the orders that belong to the store by their ID 
    /// </summary>
    /// <param name="p_storeID"></param>
    /// <returns></returns>
    List<Orders> GetAllOrdersByStoreID(string p_storeID);

    /// <summary>
    /// Will get the order details by its ID
    /// </summary>
    /// <param name="p_orderID"></param>
    /// <returns></returns>
    Orders GetOrderByOrderID(string p_orderID);

    /// <summary>
    /// Will filter all the orders of the customer
    /// </summary>
    /// <param name="p_cusID"></param>
    /// <param name="p_filter">Filter or order status should be in ["Order Placed", "Shipped", "Delivered", "Cancelled"]</param>
    /// <returns></returns>
    List<Orders> GetAllOrdersByCustomerIDWithFilter(string p_cusID, string p_filter);

    /// <summary>
    /// Will filter all the orders of that store
    /// </summary>
    /// <param name="p_storeID"></param>
    /// <param name="p_filter">Filter or order status should be in ["Order Placed", "Shipped", "Delivered", "Cancelled"]</param>
    /// <returns></returns>
    List<Orders> GetAllOrdersByStoreIDWithFilter(string p_storeID, string p_filter);

    /// <summary>
    /// Will get all orders in the database
    /// </summary>
    /// <returns></returns>
    List<Orders> GetAllOrders();

    /// <summary>
    /// Will update the information after got the trackingnumber
    /// Should be called when store added a tracking number to the order
    /// </summary>
    /// <param name="p_orderID"></param>
    /// <param name="p_status"></param>
    void UpdateOrderDetail(string p_orderID, string p_status);

    /// <summary>
    /// Will add a new tracking number to the order
    /// </summary>
    /// <param name="p_orderID"></param>
    /// <param name="p_trackingNo"></param>
    /// <returns></returns>
    Shipment AddNewTrackingNumber(string p_orderID, string p_trackingNo);

    /// <summary>
    /// Will remove all the tracking numbers of that order
    /// Use when store want to recall/return the order
    /// </summary>
    /// <param name="p_orderID"></param>
    void RemoveAllTrackingByOrderID(string p_orderID);
  }

  public interface IInventoryBL
  {
    /// <summary>
    /// Will import new product to their own inventory
    /// </summary>
    /// <param name="p_prod"></param>
    /// <returns></returns>
    Inventory ImportProduct(Inventory p_prod);

    /// <summary>
    /// Will get the product detail
    /// </summary>
    /// <param name="p_prodId"></param>
    /// <param name="p_storeID"></param>
    /// <returns></returns>
    Inventory GetProductDetail(string p_prodId, string p_storeID);

    /// <summary>
    /// Will get all the products from the store by their ID
    /// </summary>
    /// <param name="p_storeID"></param>
    /// <returns></returns>
    List<Inventory> GetAllProductsFromStore(string p_storeID);

    /// <summary>
    /// Will get all the instock products information from the store
    /// </summary>
    /// <param name="p_storeID"></param>
    /// <returns></returns>
    List<Products> GetAllInStockProductsDetailFromStore(string p_storeID);

    /// <summary>
    /// Will replenish the product
    /// </summary>
    /// <param name="p_invenID"></param>
    /// <param name="p_quantity"></param>
    void ReplenishProduct(string p_invenID, int p_quantity);
  }
}