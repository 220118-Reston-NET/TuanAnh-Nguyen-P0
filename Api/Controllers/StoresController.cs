using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class StoresController : ControllerBase
  {
    private IStoreFrontBL _storefBL;
    public StoresController(IStoreFrontBL p_storeBL)
    {
      _storefBL = p_storeBL;
    }

    // GET: api/Stores
    [HttpGet]
    public IActionResult GetAllStoreFronts()
    {
      try
      {
        Log.Information("Get all store fronts information");
        return Ok(_storefBL.GetALlStoreFronts());
      }
      catch (Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // GET: api/Stores/5
    [HttpGet("{id}", Name = "GetStore")]
    public IActionResult GetStoreFront(string id)
    {
      try
      {
        Log.Information("Get store front information by ID:" + id);
        return Ok(_storefBL.GetStoreFrontInfoByID(id));
      }
      catch (Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // POST: api/Stores
    [HttpPost]
    public IActionResult AddNewStoreFront(StoreFront p_storeF)
    {
      try
      {
        Log.Information("Add new store front information: " + p_storeF);
        return Ok(_storefBL.AddStoreFront(p_storeF));
      }
      catch (System.Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // PUT: api/Stores
    [HttpPut]
    public IActionResult SaveStoreFront(StoreFront p_storeF)
    {
      try
      {
        Log.Information("Save store front information: " + p_storeF);
        return Ok(_storefBL.SaveStoreFront(p_storeF));
      }
      catch (System.Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // // DELETE: api/Stores/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
