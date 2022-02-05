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
        if (_listProducts[i].Name == p_prod.Name)
        {
          throw new Exception("Cannot add new product due to this product is already in the store database!");
        }
      }

      return _repo.AddProduct(p_prod);
    }

    public List<Products> GetAllProducts()
    {
      return _repo.GetAllProducts();
    }

    public Products GetProductDetail(string p_prodId)
    {
      List<Products> _listProds = GetAllProducts();

      return _listProds.Where(p => p.ProductID == p_prodId).First();
    }

    public Products SaveProduct(Products p_prod)
    {
      List<Products> _listProd = GetAllProducts().Where(p => p.ProductID != p_prod.ProductID).ToList();
      if (_listProd.Any(p => p.Name == p_prod.Name))
      {
        throw new Exception("Cannot save product due to name of product is already in the store database!");
      }
      return _repo.SaveProduct(p_prod);
    }
  }
}