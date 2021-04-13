using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    //SOLID,I:Kullanmayacağın şeyi yazma
    public interface IProductService
    {
        // List<Product> GetAll();
        IDataResult<List<Product>> GetAll();

        IDataResult<List<Product>> GetAllByCategoryId(int ıd);               //categoryıdye göre geitiryor, eticarette sol tarafta category ıdye göre getiriyor
        IDataResult<List<Product>> GetAllByUnitPrice(decimal min, decimal max);  //arabada fiyat belirlerken max şu min şu olsun
        IDataResult<List<ProductDetailDto>> GetProductDetails();
        IResult Add(Product product);
        IDataResult<Product> GetById(int productId);             //sadece product dönderiyor,sitede tek ürünün özelliklerini döndürmek için
    }

}
