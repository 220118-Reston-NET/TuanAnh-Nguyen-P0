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
    public List<Orders> GetAllOrders()
    {
      string _sqlQuery = @"SELECT * FROM Orders";
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
            createdAt = reader.GetDateTime(4)
          });
        }
      }

      return _listOrders;
    }

    public Orders PlaceOrder(List<LineItems> p_lineItems, string _storeID, string _customerID, int _totalPrice)
    {
      //Add new Order query
      string _sqlQuery = @"INSERT INTO Orders
                          VALUES(@orderID, @totalPrice, @storeID, @cusID, @createdAt)";

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
  }
}