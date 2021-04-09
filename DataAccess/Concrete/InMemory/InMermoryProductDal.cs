using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMermoryProductDal : IProductDal
    {
        List<Product> _products;  //Global değişkenler _ ile tanımlanır
        //constructor void vs yok direkt class isimli
        public InMermoryProductDal()
        {
            //Oracle,Sql Server, Postgres , MongoDb bunlardan geliyomuş gibi simulation edildi.
            _products = new List<Product>
            {
                new Product{ProductId=1, CategoryId=1, ProductName="Bardak", UnitPrice=15, UnitsInStock=15},
                new Product{ProductId=2, CategoryId=1, ProductName="Kamera", UnitPrice=500, UnitsInStock=3},
                new Product{ProductId=3, CategoryId=2, ProductName="Telefon", UnitPrice=1500, UnitsInStock=2},
                new Product{ProductId=4, CategoryId=2, ProductName="Klavye", UnitPrice=150, UnitsInStock=65},
                new Product{ProductId=5, CategoryId=2, ProductName="Fare", UnitPrice=85, UnitsInStock=1}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            // _products.Remove(product); silinmez aynı referans değil
            /*Product productToDelete=null; // linq
            foreach (var p in _products)
            {
                if (product.ProductId==p.ProductId)
                {
                    productToDelete = p;
                }
            }*/
            //LINQ - Language Integrated Query
            //Lambda
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
                                                    //foreach(takma isim, if)
            _products.Remove(productToDelete);

        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        //veritabanındaki datayı businees a gönderme. yani ürün listesi yolluyor
        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)//categorye göre listeleme
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList(); //where içinde bulunan şarta uyan tüm elemanları liste yapıp döndürür
            //where=foreach
        }

        public void Update(Product product)
        {
            //gönderdiğim ürünıdsine sahip olan listedeki ürünü bul
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
        }
    }
}
