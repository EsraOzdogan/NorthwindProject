using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    //SOLID
    //Open Closed Principle:Yaptığın yazılım ayeni bir özellik ekliyorsan mevcuttaki hiçbir kodu değiştirmemek
    class Program
    {
        static void Main(string[] args)
        {
            //ProductManager productManager = new ProductManager(new InMermoryProductDal()); //memorydeki productlar gelir
            ProductManager productManager = new ProductManager(new EfProductDal()); //northwinddeki productlar

            /*foreach (var product in productManager.GetAll())
            {
                Console.WriteLine(product.ProductName);
            }*/
            /*foreach (var product in productManager.GetAllByCategoryId(2))  //2 numaralı categoryidye sahip ürünleri  ver
            {
                Console.WriteLine(product.ProductName);
            }*/
            foreach (var product in productManager.GetAllByUnitPrice(10,50))  //min 10 max50 fiyatasahip ürünleri  ver
            {
                Console.WriteLine(product.ProductName);
            }
            Console.WriteLine("Hello World!");
        }
    }
}
