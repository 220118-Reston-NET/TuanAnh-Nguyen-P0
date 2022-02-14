using System;
using System.Collections.Generic;
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
  public class ProductsController : ControllerBase
  {
    private IProductBL _prodBL;
    public ProductsController(IProductBL p_prodBL)
    {
      _prodBL = p_prodBL;
    }
    // GET: api/Products
    [HttpGet]
    public IActionResult GetAllProducts()
    {
      try
      {
        Log.Information("Get all products information");
        return Ok(_prodBL.GetAllProducts());
      }
      catch (Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // GET: api/Products/5
    [HttpGet("{id}", Name = "GetProductByID")]
    public IActionResult GetProductByID(string id)
    {
      try
      {
        Log.Information("Get product information by ID");
        return Ok(_prodBL.GetProductDetail(id));
      }
      catch (Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // PUT: api/Products
    [HttpPut]
    public IActionResult SaveProduct(Products p_prod)
    {
      try
      {
        Log.Information("Save product information: " + p_prod);
        return Ok(_prodBL.SaveProduct(p_prod));
      }
      catch (System.Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // // POST: api/Products
    [HttpPost]
    public IActionResult AddNewProduct(Products p_prod)
    {
      try
      {
        Log.Information("Add new product: " + p_prod);
        return Ok(_prodBL.AddProduct(p_prod));
      }
      catch (System.Exception e)
      {
        Log.Warning(e.Message);
        return StatusCode(500, e);
      }
    }

    // // PUT: api/Products/5
    // [HttpPut("{id}")]
    // public void Put(int id, [FromBody] string value)
    // {
    // }

    // // DELETE: api/Products/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }
  }
}
