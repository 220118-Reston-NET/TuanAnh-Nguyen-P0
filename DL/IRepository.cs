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
    Products GetProductDetailByProductId(string p_prodID);
  }
  public interface IInventoryRepository
  {
    Inventory ImportProduct(Inventory p_inven);
    List<Inventory> GetAllProductsFromStore(string p_storeID);
    List<Inventory> GetAllProducts();
    List<Products> GetAllInStockProductsDetailFromStore(string p_storeID);
    void SubtractProduct(string p_pID, string p_storeID, int p_quantity);
    void ReplenishProduct(string p_invenID, int p_quantity);
  }

  public interface IOrderRepository
  {
    Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID, int _totalPrice);
    List<Orders> GetAllOrders();
    List<LineItems> GetAllLineItemsById(string p_orderID);
  }
}