using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            //ProductManager productManager = new ProductManager(new InMermoryProductDal()); //memorydeki productlar gelir
            ProductManager productManager = new ProductManager(new EfProductDal()); //northwinddeki productlar

            foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
