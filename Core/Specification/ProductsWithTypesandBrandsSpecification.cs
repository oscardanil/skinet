using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypesandBrandsSpecification: BaseSpecification<Product>
    {
        public ProductsWithTypesandBrandsSpecification()
        {
           AddInclude(x=>x.ProductType);
           AddInclude(x=>x.ProductBrand);
        }

        public ProductsWithTypesandBrandsSpecification(int id) : base(x=>x.Id==id)
        {
           AddInclude(x=>x.ProductType);
           AddInclude(x=>x.ProductBrand);
        }
    }
} 