using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //NuGet
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDısposable pattern implementation of c#
            using (NorthwindContext context= new NorthwindContext())
            {
                var addedEntity = context.Entry(entity); //addedEntity değişken,var duruma göre herhangi bir veri tipi olur, referans yakala
                addedEntity.State = EntityState.Added; //ekleme yapılcak
                context.SaveChanges();
            }
        }

        public void Delete(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(entity); //addedEntity değişken,var duruma göre herhangi bir veri tipi olur, referans yakala
                deletedEntity.State = EntityState.Deleted; //silme yapılcak
                context.SaveChanges();
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter) //tek data getiricek
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return filter == null
                    ? context.Set<Product>().ToList() //evet ise    //select * from products yapıp listeye çeviriyo
                    : context.Set<Product>().Where(filter).ToList(); //hayır ise
            }
        }

        public void Update(Product entity)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(entity); //addedEntity değişken,var duruma göre herhangi bir veri tipi olur, referans yakala
                updatedEntity.State = EntityState.Modified; //güncelleme yapılcak
                context.SaveChanges();
            }
        }
    }
}
