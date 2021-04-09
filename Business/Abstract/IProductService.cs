using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        List<Product> GetAll();
        List<Product> GetAllByCategoryId(int ıd);//categoryıdye göre geitiryor, eticarette sol tarafta category ıdye göre getiriyor
        List<Product> GetAllByUnitPrice(decimal min, decimal max);  //arabada fiyat belirlerken max şu min şu olsun
    }

}
