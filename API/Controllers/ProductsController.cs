using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using Core.Specification;
using API.Dtos;
using AutoMapper;
using API.Errors;
using Microsoft.AspNetCore.Http;
using API.Helpers;

namespace API.Controllers
{
   public class ProductsController : BaseController
   {
      private readonly IGenericRepository<Product> _productsRepo;
      private readonly IGenericRepository<ProductType> _ProductTypesRepo;
      private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
      private readonly IMapper _mapper;
      public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandsRepo, IGenericRepository<ProductType> ProductTypesRepo, IMapper mapper)
      {
         _mapper = mapper;
         _productBrandsRepo = productBrandsRepo;
         _ProductTypesRepo = ProductTypesRepo;
         _productsRepo = productsRepo;
      }

      [HttpGet]
      public async Task<Pagination<ProductToReturnDto>> GetProducts([FromQuery] ProductSpecParams productParams)
      {
         var spec = new ProductsWithTypesAndBrandsSpecification(productParams);

         var countSpec = new ProductsWithFilterAndCountSpecification(productParams);

         var totalItems = await _productsRepo.CountAsync(countSpec);

         var products = await _productsRepo.ListAsync(spec);

         var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

         return new Pagination<ProductToReturnDto>(productParams.PageSize, productParams.PageNumber, totalItems, data);
      }

      [HttpGet("{id}")]
      [ProducesResponseType(StatusCodes.Status200OK)]
      [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
      public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
      {
         var spec = new ProductsWithTypesAndBrandsSpecification(id);

         var product = await _productsRepo.GetEntityWithSpec(spec);

         if (product == null) return NotFound(new ApiResponse(404));

         return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
      }

      [HttpGet("brands")]
      public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
      {
         IReadOnlyList<ProductBrand> productBrand = await _productBrandsRepo.ListAllAsync();

         return Ok(productBrand);
      }

      [HttpGet("types")]
      public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
      {
         IReadOnlyList<ProductType> productType = await _ProductTypesRepo.ListAllAsync();

         return Ok(productType);
      }
   }
}