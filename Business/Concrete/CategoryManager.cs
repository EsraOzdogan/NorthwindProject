using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    //interfacler refrans tutucu
    public class CategoryManager : ICategoryService
    {
        //constructor injection
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }
        public IDataResult<List<Category>> GetAll()
        {
            //iş kodları
            //Yetkisi var mı?
            //return _categoryDal.GetAll();
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll());

        }

        //select * from Categories where CategoryId=3
        public IDataResult<List<Category>> GetByCategoryId(int categoryId)
        {
            //business:iş kuralları bunlar
            // return _categoryDal.GetAll(c => c.CategoryId == categoryId);
            return new SuccessDataResult<List<Category>>(_categoryDal.GetAll(c => c.CategoryId == categoryId));

        }

        public IDataResult<Category> GetById(int categoryId)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.CategoryId == categoryId));
        }
    }
}
