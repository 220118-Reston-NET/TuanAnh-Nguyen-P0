using DL;
using Model;

namespace BL
{
  public class ProductBL : IProductBL
  {
    private IProductRepository _repo;
    public ProductBL(IProductRepository p_repo)
    {
      _repo = p_repo;
    }
    public Products AddProduct(Products p_prod)
    {
      List<Products> _listProducts = _repo.GetAllProducts();
      for (int i = 0; i < _listProducts.Count(); i++)
      {
        if (_listProducts[i].Name == p_prod.Name && _listProducts[i].StoreID == p_prod.StoreID)
        {
          throw new Exception("Cannot add new produt due to this product is already in the store database!");
        }
      }

      return _repo.AddProduct(p_prod);
    }

    public List<Products> GetAllInStockProductsFromStore(string _storeID)
    {
      List<Products> _filterList = new List<Products>();
      _filterList = _repo.GetAllProducts().Where(prod => prod.Quantity > 0 && prod.StoreID == _storeID).ToList();

      return _filterList;
    }

    public List<Products> GetAllProducts()
    {
      return _repo.GetAllProducts();
    }

    public List<Products> GetAllProductsFromStore(string _storeID)
    {
      List<Products> _filterList = new List<Products>();
      _filterList = _repo.GetAllProducts().Where(prod => prod.StoreID == _storeID).ToList();

      return _filterList;
    }

    public Products SaveProduct(Products p_prod)
    {
      return _repo.SaveProduct(p_prod);
    }

    public void SubtractProduct(string p_pID, int p_quantity, string _storeID) { }
  }
}