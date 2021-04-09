using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal:IEntityRepository<Product>
    {
        //IEntityRepository yaptık ortak olarak bunlara gerek yok
       /* List<Product> GetAll(); //hepsini getir  ürünleri listele
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);

        List<Product> GetAllByCategory(int categoryId);*/

    }
}
