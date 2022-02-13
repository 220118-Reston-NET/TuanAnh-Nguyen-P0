using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CustomersController : ControllerBase
  {
    private ICustomerBL _cusBL;
    public CustomersController(ICustomerBL p_cusBL)
    {
      _cusBL = p_cusBL;
    }
    // GET: api/Customers
    [HttpGet]
    public IActionResult GetAllCustomers()
    {
      try
      {
        Log.Information("Get all customers information");
        return Ok(_cusBL.GetALlCustomers());
      }
      catch (Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // GET: api/Customers/5
    [HttpGet("{id}", Name = "GetCustomer")]
    public IActionResult GetCustomer(string id)
    {
      try
      {
        Log.Information("Get customer information by ID:" + id);
        return Ok(_cusBL.GetCustomerInfoByID(id));
      }
      catch (Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // // POST: api/Customers
    // [HttpPost]
    // public void Post([FromBody] string value)
    // {
    // }

    // // PUT: api/Customers/5
    // [HttpPut("{id}")]
    // public void Put(int id, [FromBody] string value)
    // {
    // }

    // // DELETE: api/Customers/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
