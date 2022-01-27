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
  }
}