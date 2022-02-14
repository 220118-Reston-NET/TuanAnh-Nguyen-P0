using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using BL;
using Model;
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

    // POST: api/Customers
    [HttpPost]
    public IActionResult AddNewCustomer(Customer p_cus)
    {
      try
      {
        Log.Information("Add new customer: " + p_cus);
        return Ok(_cusBL.AddCustomer(p_cus));
      }
      catch (System.Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // PUT: api/Customers
    [HttpPut]
    public IActionResult SaveCustomer(Customer p_cus)
    {
      try
      {
        Log.Information("Save customer: " + p_cus);
        return Ok(_cusBL.SaveCustomer(p_cus));
      }
      catch (System.Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // // DELETE: api/Customers/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
