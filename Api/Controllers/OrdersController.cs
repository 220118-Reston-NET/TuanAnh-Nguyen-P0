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
  public class OrdersController : ControllerBase
  {
    private IOrderBL _orderBL;
    public OrdersController(IOrderBL p_orderBL)
    {
      _orderBL = p_orderBL;
    }
    // GET: api/Orders
    [HttpGet]
    public IActionResult GetAllOrders()
    {
      try
      {
        Log.Information("Get all orders information");
        return Ok(_orderBL.GetAllOrders());
      }
      catch (Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // GET: api/Orders/5
    [HttpGet("{id}", Name = "GetOrderByID")]
    public IActionResult GetOrderByID(string id)
    {
      try
      {
        Log.Information("Get order information");
        return Ok(_orderBL.GetOrderByOrderID(id));
      }
      catch (Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // POST: api/Orders
    [HttpPost]
    public IActionResult AddNewOrder(List<LineItems> p_LineItems, string p_storeID, string p_cusID, int totalPrice)
    {
      try
      {
        Log.Information("Added new order");
        return Ok(_orderBL.PlaceOrder(p_LineItems, p_storeID, p_cusID, totalPrice));
      }
      catch (System.Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // // PUT: api/Orders/5
    // [HttpPut("{id}")]
    // public void Put(int id, [FromBody] string value)
    // {
    // }

    // // DELETE: api/Orders/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
