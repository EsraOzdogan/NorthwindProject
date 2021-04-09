using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public List<Product> GetAll()
        {
            //iş kodları
            //Yetkisi var mı?
            return _productDal.GetAll();
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
    }
}
