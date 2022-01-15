using System;
using Core.Entities;

namespace Core.Specification
{
   public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
   {
      public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
         : base(x =>
            (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId) &&
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search))
         )
      {
         AddInclude(x => x.ProductType);
         AddInclude(x => x.ProductBrand);

         AddOrderBy(x => x.Name);

         ApplyPaging(productParams.PageSize * (productParams.PageNumber - 1), productParams.PageSize);

         if (!String.IsNullOrEmpty(productParams.Sort))
         {
            switch (productParams.Sort)
            {
               case "priceAsc":
                  AddOrderBy(x => x.Price);
                  break;
               case "priceDesc":
                  AddOrderByDescending(x => x.Price);
                  break;
               default:
                  AddOrderBy(x => x.Name);
                  break;
            }
         }

      }

      public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
      {
         AddInclude(x => x.ProductType);
         AddInclude(x => x.ProductBrand);
      }
   }
}