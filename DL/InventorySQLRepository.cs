using System.Data.SqlClient;
using Model;

namespace DL
{
  public class InventorySQLRepository : IInventoryRepository
  {
    private readonly string _connectionString;
    public InventorySQLRepository(string p_connectionString)
    {
      _connectionString = p_connectionString;
    }
    public List<Products> GetAllInStockProductsDetailFromStore(string p_storeID)
    {
      string _sqlQuery = @"SELECT p.productID, p.productName, p.productPrice, p.productDesc
                          FROM Inventory i, Products p
                          WHERE i.productID = p.productID
                            AND i.storeID = @storeID
                            AND i.quantity > 0";
      List<Products> _listProds = new List<Products>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@storeID", p_storeID);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listProds.Add(new Products()
          {
            ProductID = reader.GetString(0),
            Name = reader.GetString(1),
            Price = reader.GetInt32(2),
            Desc = reader.GetString(3)
          });
        }
      }

      return _listProds;
    }

    public List<Inventory> GetAllProducts()
    {
      List<Inventory> _listInven = new List<Inventory>();

      string _sqlQuery = @"SELECT * FROM Inventory";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listInven.Add(new Inventory()
          {
            InventoryID = reader.GetString(0),
            ProductID = reader.GetString(1),
            StoreID = reader.GetString(2),
            Quantity = reader.GetInt32(3)
          });
        }
      }

      return _listInven;
    }

    public List<Inventory> GetAllProductsFromStore(string p_storeID)
    {
      List<Inventory> _listInven = GetAllProducts().Where(p => p.StoreID == p_storeID).ToList();

      return _listInven;
    }

    public Inventory ImportProduct(Inventory p_inven)
    {
      string _sqlQuery = @"INSERT INTO Inventory
                          VALUES(@inventoryID, @productID, @storeID, @quantity)";

      p_inven.InventoryID = Guid.NewGuid().ToString();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@inventoryID", p_inven.InventoryID);
        command.Parameters.AddWithValue("@productID", p_inven.ProductID);
        command.Parameters.AddWithValue("@storeID", p_inven.StoreID);
        command.Parameters.AddWithValue("@quantity", p_inven.Quantity);

        command.ExecuteNonQuery();
      }

      return p_inven;
    }

    public void ReplenishProduct(string p_invenID, int p_quantity)
    {
      string _sqlQuery = @"UPDATE Inventory
                          SET quantity = quantity + @quantity
                          WHERE inventoryID = @inventoryID";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@quantity", p_quantity);
        command.Parameters.AddWithValue("@inventoryID", p_invenID);

        command.ExecuteNonQuery();
      }
    }

    public void SubtractProduct(string p_pID, string p_storeID, int p_quantity)
    {
      string _sqlQuery = @"UPDATE Inventory
                          SET quantity = quantity - @quantity
                          WHERE productID = @productID
                            AND storeID = @storeID";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@quantity", p_quantity);
        command.Parameters.AddWithValue("@productID", p_pID);
        command.Parameters.AddWithValue("@storeID", p_storeID);

        command.ExecuteNonQuery();
      }
    }
  }
}