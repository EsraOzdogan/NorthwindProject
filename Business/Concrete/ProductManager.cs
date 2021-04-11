using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
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

        public IResult Add(Product product)
        {
            //business code
            /*_productDal.Add(product);
            /*bu şekilde yapmıyoruz
             Result result = new Result();
            result.Success(result);
            return result;*/
            //return new SuccessResult(); //add işlemini geçti true döndericek ve mesaj yanında
            if (product.ProductName.Length<2)
            {
                //magic strings, hep aybı string
                return new ErrorResult(Messages.ProductNameInvalid);
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAddded);
        }

        public IDataResult<List<Product>> GetAll()
        {
            //iş kodları
            //Yetkisi var mı?
            if (DataTime.Now.Hour==22)
            {
                return new ErrorDataResult();
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),true,"ürünler listelendi");
        }

        public List<Product> GetAllByCategoryId(int ıd)
        {
            //business:iş kuralları bunlar
            return _productDal.GetAll(p=> p.CategoryId==ıd);// her p için pnin categorydsi benım categoryıdme(ıd) eşitse filtrele
        }

        public List<Product> GetAllByUnitPrice(decimal min, decimal max)
        {
            return _productDal.GetAll(p => p.UnitPrice>=min && p.UnitPrice<=max);
        }

        public Product GetById(int productId)
        {
            return _productDal.Get(p=> p.ProductId==productId);
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            return _productDal.GetProductDetails();
        }
    }
}
