using DL;
using Model;

namespace BL
{
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
      List<Orders> _customerOrders = new List<Orders>();

      _customerOrders = _repo.GetAllOrders().Where(ord => ord.CustomerID == p_cusID).ToList();

      return _customerOrders;
    }

    public List<Orders> GetAllOrdersByStoreID(string p_storeID)
    {
      List<Orders> _customerOrders = new List<Orders>();

      _customerOrders = _repo.GetAllOrders().Where(ord => ord.StoreID == p_storeID).ToList();

      return _customerOrders;
    }

    public Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID)
    {
      return _repo.PlaceOrder(p_lineItems, _storeID, _customerID);
    }
  }
}