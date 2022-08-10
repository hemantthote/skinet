using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<Product> _productsRepo;
     
      public ProductsController(IGenericRepository<Product> productsRepo,
      IGenericRepository<ProductBrand> productBrandRepo,
      IGenericRepository<ProductType> productTypeRepo,
      IMapper mapper)
      {
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper = mapper;
        }

      [HttpGet]
      public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>>GetProducts(string sort,
      int? brandId, int? typeId)
      {
        var spec = new ProductWithTypesAndBrandsSpecification(sort,brandId,typeId);
        var Products = await _productsRepo.ListAsync(spec);
       return Ok(_mapper
                  .Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(Products));
      }

      [HttpGet("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
      public async Task<ActionResult<ProductToReturnDto>>GetProduct(int id){
         var spec = new ProductWithTypesAndBrandsSpecification(id);
         var product = await _productsRepo.GetEntityWithSpec(spec);
         if(product == null){
          return NotFound(new ApiResponse(404));
         }
        return _mapper.Map<Product,ProductToReturnDto>(product);
      }
      
       [HttpGet("brands")]
      public async Task<ActionResult<IReadOnlyList<ProductBrand>>>GetProductBrands(){
        var Brands = await _productBrandRepo.ListAllAsync();
        return Ok(Brands);
      }

       [HttpGet("types")]
      public async Task<ActionResult<IReadOnlyList<Product>>>GetProductTypess(){
        var Types = await _productTypeRepo.ListAllAsync();
        return Ok(Types);
      }
    }
}