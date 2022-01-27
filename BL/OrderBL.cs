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