using Core.Entities;

namespace Core.Specification
{
   public class ProductsWithFilterAndCountSpecification : BaseSpecification<Product>
   {
      public ProductsWithFilterAndCountSpecification(ProductSpecParams productParams)
           : base(x =>
          (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
          (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId) &&
          (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search))
          )
      {

      }
   }
}