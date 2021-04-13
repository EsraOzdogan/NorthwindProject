using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    //category il ilgili dış dünyaya neyi servis etmek istiyorsak
    public interface ICategoryService
    {
        IDataResult<List<Category>> GetAll();
        IDataResult<List<Category>> GetByCategoryId(int categoryId);//categoryıdye göre geitiryor, eticarette sol tarafta category ıdye göre getiriyor
        IDataResult<Category> GetById(int categoryId);
    }
}
