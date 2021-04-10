using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
        List<ProductDetailDto> GetProductDetails();
        //IEntityRepository yaptık ortak olarak bunlara gerek yok
        /* List<Product> GetAll(); //hepsini getir  ürünleri listele
         void Add(Product product);
         void Update(Product product);
         void Delete(Product product);

         List<Product> GetAllByCategory(int categoryId);*/

    }
}
