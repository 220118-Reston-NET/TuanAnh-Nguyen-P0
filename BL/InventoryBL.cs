using DL;
using Model;

namespace BL
{
  public class InventoryBL : IInventoryBL
  {
    private IInventoryRepository _repo;
    public InventoryBL(IInventoryRepository p_repo)
    {
      _repo = p_repo;
    }

    public List<Products> GetAllInStockProductsDetailFromStore(string p_storeID)
    {
      return _repo.GetAllInStockProductsDetailFromStore(p_storeID);
    }

    public List<Inventory> GetAllProductsFromStore(string p_storeID)
    {
      return _repo.GetAllProductsFromStore(p_storeID);
    }

    public Inventory GetProductDetail(string p_prodId, string p_storeID)
    {
      List<Inventory> _listInven = GetAllProductsFromStore(p_storeID);
      Inventory _selectedInven = new Inventory();

      // _selectedInven = _listInven.Where(p => p.ProductID == p_prodId).First();

      for (int i = 0; i < _listInven.Count(); i++)
      {
        if (_listInven[i].ProductID == p_prodId)
        {
          _selectedInven = _listInven[i];
        }
      }

      return _selectedInven;
    }

    public Inventory ImportProduct(Inventory p_inven)
    {
      return _repo.ImportProduct(p_inven);
    }

    public void ReplenishProduct(string p_invenID, int p_quantity)
    {
      _repo.ReplenishProduct(p_invenID, p_quantity);
    }

  }
}