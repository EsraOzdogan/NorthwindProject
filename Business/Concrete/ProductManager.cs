using Business.Abstract;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    //aynı anda birden fazla şey döndürmek için encapsulation,IResult
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları
            //Yetkisi var mı?
            //return _productDal.GetAll();
            if (DateTime.Now.Hour==22)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int ıd)
        {
            //business:iş kuralları bunlar
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=> p.CategoryId==ıd));// her p için pnin categorydsi benım categoryıdme(ıd) eşitse filtrele
        }
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice>=min && p.UnitPrice<=max));
        }

        
        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
           /* if (DateTime.Now.Hour == 16)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }*/
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }
        public IResult Add(Product product)
        {
            //business code
            /*_productDal.Add(product);
            /*bu şekilde yapmıyoruz
             Result result = new Result();
            result.Success(result);
            return result;*/
            //return new SuccessResult(); //add işlemini geçti true döndericek ve mesaj yanında


            //validation--fluent validation ile bu aşağıdaki doğrulama kodlarından kurtulucaz
            //if (product.ProductName.Length < 2)
            //{
            //    //magic strings, hep aybı string
            //    return new ErrorResult(Messages.ProductNameInvalid);
            //}

            //var context = new ValidationContext<Product>(product);
            //ProductValidator productValidator = new ProductValidator();
            //var result = productValidator.Validate(context);
            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}



            //loglama
            //cacheremove
            //performance
            //transaction
            //yetkilendirme 
            //[Validate]
            ValidationTool.Validate(new ProductValidator(),product);
            //loglama
            //cacheremove
            //performance
            //transaction
            //yetkilendirme hepsi burada karışıcak yukarıda [validate]

            //business codes


            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAddded);
        }

    }
}
