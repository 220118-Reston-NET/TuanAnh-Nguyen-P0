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
      return _repo.AddStoreFront(p_storef);
    }

    public List<StoreFront> GetALlStoreFronts()
    {
      return _repo.GetALlStoreFronts();
    }
  }
}