using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]  //attribute
    public class ProductsController : ControllerBase
    {
        //Loosely coupled
        //naming convention
        //IoC Container--Inversion of Control
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public List<Product> Get()
        {
            /*return new List<Product>
            {
               new Product{ProductId=1, ProductName="Elma"},
               new Product{ProductId=2, ProductName="Armut"},

            };*/

            //Dependency chain


            /*IProductService productService = new ProductManager(new EfProductDal());
            var result = productService.GetAll();
            return result.Data;*/

            var result = _productService.GetAll();
            return result.Data;

        }
    }
}
