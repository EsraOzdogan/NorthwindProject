﻿using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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

        [HttpGet("getall")]//alians
        public IActionResult GetAll()
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

            //Swagger

            Thread.Sleep(1000);

            var result = _productService.GetAll();
            if (result.Success)//if(result.Success==true) aynı şey
            {
                return Ok(result);
            }
            return BadRequest(result);

        }


        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpGet("getbycategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            var result = _productService.GetAllByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpGet("getproductdetails")]
        public IActionResult GetProductDetails()
        {
            var result = _productService.GetProductDetails();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        [HttpPost("add")]
        public IActionResult Add(Product product)
        {    
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }
    }
}
