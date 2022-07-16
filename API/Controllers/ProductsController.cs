using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
     
      public ProductsController(IProductRepository repo)
      {
            _repo = repo;
           
      }

      [HttpGet]
      public async Task<ActionResult<List<Product>>>GetProducts(){
        var Products = await _repo.GetProductAsync();
        return Ok(Products);
      }

      [HttpGet("{id}")]
      public async Task<ActionResult<Product>>GetProduct(int id){
        return Ok(await _repo.GetProductByIdAsync(id));
      }
      
       [HttpGet("brands")]
      public async Task<ActionResult<IReadOnlyList<ProductBrand>>>GetProductBrands(){
        var Brands = await _repo.GetProductBrandAsync();
        return Ok(Brands);
      }

       [HttpGet("types")]
      public async Task<ActionResult<IReadOnlyList<Product>>>GetProductTypess(){
        var Types = await _repo.GetProductTypeAsync();
        return Ok(Types);
      }
    }
}