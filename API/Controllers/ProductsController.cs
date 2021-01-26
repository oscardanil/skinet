using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController: ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IGenericRepository<ProductBrand> _productbrandRepo;
        private readonly IGenericRepository<ProductType> _producttypeRepo;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product>productRepo, IGenericRepository<ProductBrand>productBrand,
            IGenericRepository<ProductType> productType,IMapper mapper)
            {
            _productRepo = productRepo;
            _productbrandRepo = productBrand;
            _producttypeRepo = productType;
            _mapper = mapper;
        }
            [HttpGet]
            public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
            {
              var spec= new ProductsWithTypesandBrandsSpecification();

              var products=await _productRepo.ListAsync(spec);
              return Ok(_mapper.Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products));
            }
             [HttpGet("{id}")]
             public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
             {
              var spec= new ProductsWithTypesandBrandsSpecification(id);
               var product= await _productRepo.GetEntityWithSpec(spec);
               return _mapper.Map<Product,ProductToReturnDto>(product);
             }

             [HttpGet("brands")]
             public async Task<ActionResult<IReadOnlyList<ProductBrand>>>GetProductBrands()
             {
               var productbrand=await _productbrandRepo.ListAllAsync();
               return Ok(productbrand);
             }
             [HttpGet("types")]
             public async Task<ActionResult<IReadOnlyList<ProductType>>>GetProductTypes()
             {
               var producttypes= await _producttypeRepo.ListAllAsync();
               return Ok(producttypes);
             }
    }
}