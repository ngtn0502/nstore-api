using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductType> _ProductTypesRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandsRepo;
        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandsRepo, IGenericRepository<ProductType> ProductTypesRepo)
        {
            _productBrandsRepo = productBrandsRepo;
            _ProductTypesRepo = ProductTypesRepo;
            _productsRepo = productsRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            IReadOnlyList<Product> product = await _productsRepo.ListAllAsync();
            return Ok(product);
        }

        [HttpGet("{id}")]
        public async Task<Product> GetProduct(int id)
        {
            Product product = await _productsRepo.GetByIdAsync(id);
            return product;
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