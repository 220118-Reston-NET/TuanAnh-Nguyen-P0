using System.Data.SqlClient;
using Model;

namespace DL
{
  public class ProductSQLRepository : IProductRepository
  {
    private readonly string _connectionString;
    public ProductSQLRepository(string p_connectionString)
    {
      _connectionString = p_connectionString;
    }
    public Products AddProduct(Products p_prod)
    {
      string _sqlQuery = @"INSERT INTO Products
                  VALUES(@productID, @productName, @productPrice, @productDesc, @createdAt, @minimumAge)";

      p_prod.ProductID = Guid.NewGuid().ToString();
      p_prod.createdAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@productID", p_prod.ProductID);
        command.Parameters.AddWithValue("@productName", p_prod.Name);
        command.Parameters.AddWithValue("@productPrice", p_prod.Price);
        command.Parameters.AddWithValue("@productDesc", p_prod.Desc);
        command.Parameters.AddWithValue("@createdAt", p_prod.createdAt);
        command.Parameters.AddWithValue("@minimumAge", p_prod.MinimumAge);

        command.ExecuteNonQuery();
      }

      return p_prod;
    }

    public List<Products> GetAllProducts()
    {
      string _sqlQuery = @"SELECT * FROM Products";
      List<Products> _listProds = new List<Products>();

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listProds.Add(new Products()
          {
            ProductID = reader.GetString(0),
            Name = reader.GetString(1),
            Price = reader.GetInt32(2),
            Desc = reader.GetString(3),
            createdAt = reader.GetDateTime(4),
            MinimumAge = reader.GetInt32(5)
          });
        }
      }

      return _listProds;
    }

    public List<Products> GetAllProductsFromStore(string p_storeID)
    {
      List<Products> _listOfProducts = new List<Products>();
      string _sqlQuery = @"SELECT p.productID, p.productName, p.productPrice, p.productDesc, p.createdAt, p.minimumAge FROM Products p, StoreFronts sf, Inventory i
                          WHERE p.productID = i.productID 
                            AND sf.storeID = i.storeID
                            AND sf.storeID = @storeID";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@storeID", p_storeID);

        SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          _listOfProducts.Add(new Products()
          {
            ProductID = reader.GetString(0),
            Name = reader.GetString(1),
            Price = reader.GetInt32(2),
            Desc = reader.GetString(3),
            createdAt = reader.GetDateTime(4),
            MinimumAge = reader.GetInt32(5)
          });
        }
      }

      return _listOfProducts;
    }

    public List<StoreFront> GetAllStoreFrontsByProductID(string p_prodID)
    {
      List<StoreFront> _listStores = new List<StoreFront>();

      string _sqlQuery = @"SELECT sf.storeID, sf.storeName, sf.storeAddress, sf.createdAt FROM StoreFronts sf, Inventory i, Products p 
                          WHERE sf.storeID = i.storeID 
                            AND i.productID = p.productID
                            AND p.productID = @productID";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);
        command.Parameters.AddWithValue("@productID", p_prodID);

        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          _listStores.Add(new StoreFront()
          {
            StoreID = reader.GetString(0),
            Name = reader.GetString(1),
            Address = reader.GetString(2),
            createdAt = reader.GetDateTime(3)
          });
        }
      }

      return _listStores;
    }

    public Products GetProductDetailByProductId(string p_prodID)
    {
      return GetAllProducts().Where(p => p.ProductID == p_prodID).First();
    }

    public Products SaveProduct(Products p_prod)
    {
      string _sqlQuery = @"UPDATE Products 
                          SET productName = @productName,
                            productPrice = @productPrice,
                            productDesc = @productDesc,
                            minimumAge = @minimumAge
                          WHERE productID = @productID";

      using (SqlConnection conn = new SqlConnection(_connectionString))
      {
        conn.Open();

        SqlCommand command = new SqlCommand(_sqlQuery, conn);

        command.Parameters.AddWithValue("@productName", p_prod.Name);
        command.Parameters.AddWithValue("@productPrice", p_prod.Price);
        command.Parameters.AddWithValue("@productDesc", p_prod.Desc);
        command.Parameters.AddWithValue("@minimumAge", p_prod.MinimumAge);
        command.Parameters.AddWithValue("@productID", p_prod.ProductID);

        command.ExecuteNonQuery();
      }

      return p_prod;
    }
  }
}