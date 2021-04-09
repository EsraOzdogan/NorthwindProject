using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface ICategoryDal:IEntityRepository<Category>
    {
        //IEntityRepository yaptık ortak olarak bunlara gerek yok
       /* List<Category> GetAll(); //hepsini getir  ürünleri listele
        void Add(Category category);
        void Update(Category category);
        void Delete(Category category);
        
        List<Category> GetAllByCategory(int categoryId);*/
    }
}

//Code Refactoring