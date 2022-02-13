using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class InventoryController : ControllerBase
  {
    private IInventoryBL _invenBL;
    public InventoryController(IInventoryBL p_invenBL)
    {
      _invenBL = p_invenBL;
    }
    // // GET: api/Inventory
    // [HttpGet]
    // public IEnumerable<string> Get()
    // {
    //   return new string[] { "value1", "value2" };
    // }

    // GET: api/Inventory/5
    [HttpGet("{id}", Name = "GetAllInventoryFromStore")]
    public IActionResult GetAllInventoryByStoreID(string id)
    {
      try
      {
        Log.Information("Get all inventory information by store ID:" + id);
        return Ok(_invenBL.GetAllProductsFromStore(id));
      }
      catch (Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // // POST: api/Inventory
    // [HttpPost]
    // public void Post([FromBody] string value)
    // {
    // }

    // // PUT: api/Inventory/5
    // [HttpPut("{id}")]
    // public void Put(int id, [FromBody] string value)
    // {
    // }

    // // DELETE: api/Inventory/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
