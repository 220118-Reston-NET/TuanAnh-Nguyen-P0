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

    public Shipment AddNewTrackingNumber(string p_orderID, string p_trackingNo)
    {
      return _repo.AddNewTrackingNumber(p_orderID, p_trackingNo);
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

    public List<Orders> GetAllOrdersByCustomerIDWithFilter(string p_cusID, string p_filter)
    {
      List<Orders> _listOrdersFilter = GetAllOrdersByCustomerID(p_cusID).Where(p => p.Status == p_filter).ToList();

      return _listOrdersFilter;
    }

    public List<Orders> GetAllOrdersByStoreID(string p_storeID)
    {
      List<Orders> _customerOrders = new List<Orders>();

      _customerOrders = _repo.GetAllOrders().Where(ord => ord.StoreID == p_storeID).ToList();

      return _customerOrders;
    }

    public List<Orders> GetAllOrdersByStoreIDWithFilter(string p_storeID, string p_filter)
    {
      List<Orders> _listOrdersFilter = GetAllOrdersByStoreID(p_storeID).Where(p => p.Status == p_filter).ToList();

      return _listOrdersFilter;
    }

    public Orders GetOrderByOrderID(string p_orderID)
    {
      Orders _orderDetail = GetAllOrders().Where(p => p.OrderID == p_orderID).First();

      return _orderDetail;
    }

    public Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID, int _totalPrice)
    {
      return _repo.PlaceOrder(p_lineItems, _storeID, _customerID, _totalPrice);
    }

    public void RemoveAllTrackingByOrderID(string p_orderID)
    {
      _repo.RemoveAllTrackingByOrderID(p_orderID);
    }

    public void UpdateOrderDetail(string p_orderID, string p_status)
    {
      _repo.UpdateOrderDetail(p_orderID, p_status);
    }
  }
}