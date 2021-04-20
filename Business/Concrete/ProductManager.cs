using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    //aynı anda birden fazla şey döndürmek için encapsulation,IResult
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        //ILogger _logger;

        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal/*, ILogger logger*/, ICategoryService categoryService)
        {
            _productDal = productDal;

            // _logger = logger;

            _categoryService = categoryService;
        }

        [CacheAspect] // key,value //yakın zamanda çağırılırsa ve veri değiştiyse veritabanına gitmeden cacheden alınıcak
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
        [CacheAspect]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max)
        {
             //if (min<0)
            //{
            //    return new ErrorDataResult<List<Car>>(Messages.CarDailyPriceInvalid);

            //}
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


        //[]method çalışmadan burdan geçicek
        //Add methodunu ProductValidatora göre doğrula
        //Claim
        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]//içinde Get olanlar
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

            //ValidationTool.Validate(new ProductValidator(),product);


            //loglama
            //cacheremove
            //performance
            //transaction
            //yetkilendirme hepsi burada karışıcak yukarıda [validate]

            //business codes

            /* Şu aşamada hata var desen vs çalışır ama karışık.Bu loglama, cache vs AOP ile tek noltaya alınıyor
             _logger.Log();
             try
             {
                 _productDal.Add(product);
                 return new SuccessResult(Messages.ProductAddded);
             }
             catch (Exception)
             {

                 _logger.Log();
             }
             return new ErrorResult();*/

            //bir kategoride en fazla 10 ürün olabilir
            //var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count; //business code
            //if (result>=10)
            //{
            //    return new ErrorResult(Messages.ProductCountOfCategoryError);
            //}

            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            //{
            //    if (CheckIfProductNameExists(product.ProductName).Success)
            //    {
            //        _productDal.Add(product);
            //        return new SuccessResult(Messages.ProductAddded);
            //    }

            //}
            //return new ErrorResult();


            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName),
                CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfCategoryLimitExceded());
            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);

            return new SuccessResult(Messages.ProductAddded);
        }

        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }


        //business code parçacığı
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //Select count(*) from products where CategoryId=1
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count; //business code
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        //Aynı isimde product,ürün olamaz
        private IResult CheckIfProductNameExists(string productName)
        {
            //Select count(*) from products where CategoryId=1
            var result = _productDal.GetAll(p => p.ProductName == productName).Any(); //business code
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExist);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count>15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {

            Add(product);
            if (product.UnitPrice < 10)
            {
                throw new Exception("");
            }

            Add(product);

            return null;
        }
    }
}
