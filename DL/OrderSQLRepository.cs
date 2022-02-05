using System.Data.SqlClient;
using Model;

namespace DL
{
  public class OrderSQLRepository : IOrderRepository
  {
    private readonly string _connectionString;
    public OrderSQLRepository(string p_connectionString)
    {
      _connectionString = p_connectionString;
    }
    private InventorySQLRepository _invenDL;
    private ProductSQLRepository _prodDL;
    public List<Orders> GetAllOrders()
    {
      string _sqlQuery = @"SELECT * FROM Orders
                          ORDER BY createdAt";
      List<Orders> _listOrders = new List<Orders>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listOrders.Add(new Orders()
          {
            OrderID = reader.GetString(0),
            TotalPrice = reader.GetInt32(1),
            StoreID = reader.GetString(2),
            CustomerID = reader.GetString(3),
            createdAt = reader.GetDateTime(4),
            Status = reader.GetString(5),
            ListLineItems = GetAllLineItemsById(reader.GetString(0)),
            ListTrackings = GetAllShipmentById(reader.GetString(0))
          });
        }
      }

      return _listOrders;
    }

    public Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID, int _totalPrice)
    {
      //Add new Order query
      string _sqlQuery = @"INSERT INTO Orders
                          VALUES(@orderID, @totalPrice, @storeID, @cusID, @createdAt, @orderStatus)";

      //Add new LineItems query
      string _sqlQuery2 = @"INSERT INTO LineItems
                          VALUES(@productID, @orderID, @quantity)";

      Orders _newOrder = new Orders();
      _invenDL = new InventorySQLRepository(_connectionString);

      _newOrder.OrderID = Guid.NewGuid().ToString();
      _newOrder.createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        //Add New Order To Database
        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@orderID", _newOrder.OrderID);
        command.Parameters.AddWithValue("@totalPrice", _totalPrice);
        command.Parameters.AddWithValue("storeID", _storeID);
        command.Parameters.AddWithValue("@cusID", _customerID);
        command.Parameters.AddWithValue("@createdAt", _newOrder.createdAt);
        command.Parameters.AddWithValue("@orderStatus", "Order Placed");

        command.ExecuteNonQuery();

        //Add To Line Items & Substract Inventory
        foreach (var item in p_lineItems)
        {
          //Add To Line Items
          command = new SqlCommand(_sqlQuery2, conn);
          command.Parameters.AddWithValue("@productID", item.ProductID);
          command.Parameters.AddWithValue("@orderID", _newOrder.OrderID);
          command.Parameters.AddWithValue("@quantity", item.Quantity);

          command.ExecuteNonQuery();

          //Substract Inventory
          _invenDL.SubtractProduct(item.ProductID, _storeID, item.Quantity);
        }
      }

      return _newOrder;
    }

    public List<LineItems> GetAllLineItemsById(string p_orderID)
    {
      string _sqlQuery = @"SELECT * FROM LineItems
                          WHERE orderID = @orderID";
      List<LineItems> _listLineItems = new List<LineItems>();
      _prodDL = new ProductSQLRepository(_connectionString);

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@orderID", p_orderID);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listLineItems.Add(new LineItems()
          {
            ProductID = reader.GetString(0),
            OrderID = reader.GetString(1),
            Quantity = reader.GetInt32(2),
            ProductName = _prodDL.GetProductDetailByProductId(reader.GetString(0)).Name
          });
        }
      }

      return _listLineItems;
    }

    public List<Shipment> GetAllShipmentById(string p_orderID)
    {
      string _sqlQuery = @"SELECT * FROM Shipment
                          WHERE orderID = @orderID";
      List<Shipment> _listShipment = new List<Shipment>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@orderID", p_orderID);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listShipment.Add(new Shipment()
          {
            ShipmentID = reader.GetString(0),
            OrderID = reader.GetString(1),
            TrackingNumber = reader.GetString(2)
          });
        }
      }

      return _listShipment;
    }

    public void UpdateOrderDetail(string p_orderID, string p_status)
    {
      string _sqlQuery = @"UPDATE Orders 
                          SET orderStatus = @orderStatus
                          WHERE orderID = @orderID";
      Orders _orderDetail = new Orders();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@orderStatus", p_status);
        command.Parameters.AddWithValue("@orderID", p_orderID);

        command.ExecuteNonQuery();
      }
    }

    public Shipment AddNewTrackingNumber(string p_orderID, string p_trackingNo)
    {
      string _sqlQuery = @"INSERT INTO Shipment
                        VALUES(@shipmentID, @orderID, @trackingNumber)";
      Shipment _shipment = new Shipment();
      _shipment.ShipmentID = Guid.NewGuid().ToString();
      _shipment.OrderID = p_orderID;
      _shipment.TrackingNumber = p_trackingNo;

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@shipmentID", _shipment.ShipmentID);
        command.Parameters.AddWithValue("@orderID", p_orderID);
        command.Parameters.AddWithValue("@trackingNumber", p_trackingNo);

        command.ExecuteNonQuery();
      }

      return _shipment;
    }

    public void RemoveAllTrackingByOrderID(string p_orderID)
    {
      string _sqlQuery = @"DELETE FROM Shipment 
                          WHERE orderID = @orderID";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@orderID", p_orderID);

        command.ExecuteNonQuery();
      }
    }
  }
}