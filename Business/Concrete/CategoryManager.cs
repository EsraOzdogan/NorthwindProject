using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        //constructor injection
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal productDal)
        {
            _categoryDal = productDal;
        }
        public List<Category> GetAll()
        {
            //iş kodları
            //Yetkisi var mı?
            return _categoryDal.GetAll();
        }

        //select * from Categories where CategoryId=3
        public List<Category> GetByCategoryId(int categoryId)
        {
            //business:iş kuralları bunlar
            return _categoryDal.GetAll(c => c.CategoryId == categoryId);
        }

       
    
    }
}
