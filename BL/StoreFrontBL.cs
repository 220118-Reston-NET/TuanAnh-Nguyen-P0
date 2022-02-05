using DL;
using Model;

namespace BL
{
  public class StoreFrontBL : IStoreFrontBL
  {
    private IStoreFrontRepository _repo;
    public StoreFrontBL(IStoreFrontRepository p_repo)
    {
      _repo = p_repo;
    }
    public StoreFront AddStoreFront(StoreFront p_storef)
    {
      List<StoreFront> _listStores = _repo.GetALlStoreFronts();

      for (int i = 0; i < _listStores.Count(); i++)
      {
        if (_listStores[i].Name == p_storef.Name)
        {
          throw new Exception("Cannot add new StoreFront due to this name is already in the database!");
        }
      }

      return _repo.AddStoreFront(p_storef);
    }

    public List<StoreFront> GetALlStoreFronts()
    {
      return _repo.GetALlStoreFronts();
    }

    public StoreFront GetStoreFrontInfoByID(string p_storeID)
    {
      StoreFront _storeFrontInfo = GetALlStoreFronts().Where(p => p.StoreID == p_storeID).First();

      return _storeFrontInfo;
    }

    public StoreFront SaveStoreFront(StoreFront p_storef)
    {
      List<StoreFront> _listStore = GetALlStoreFronts().Where(p => p.StoreID != p_storef.StoreID).ToList();
      if (_listStore.Any(p => p.Name == p_storef.Name))
      {
        throw new Exception("Cannot save store front due to name of store front is already in the store database!");
      }
      return _repo.SaveStoreFront(p_storef);
    }
  }
}