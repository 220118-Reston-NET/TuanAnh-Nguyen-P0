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
      return _repo.AddProduct(p_prod);
    }

    public List<Products> GetAllProducts()
    {
      return _repo.GetAllProducts();
    }

    public List<Products> GetAllProductsFromStore(string _storeID)
    {
      return _repo.GetAllProductsFromStore(_storeID);
    }

    public Products SaveProduct(Products p_prod)
    {
      return _repo.SaveProduct(p_prod);
    }

    public void SubtractProduct(string p_pID, int p_quantity, string _storeID) { }
  }
}