using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages //sürekli newlememek için static yazıyoruz
    {
        public static string ProductAddded = "Product added";
        public static string ProductNameInvalid = "Product name is invalid";
        public static string MaintenanceTime= "System is under maintenance";
        public static string ProductsListed = "Products listed";
        public static string ProductCountOfCategoryError= "There can be a maximum of 10 products in a category";
        public static string ProductUpdated="Product updated";
        public static string ProductNameAlreadyExist="Product name already exists";
        public static string CategoryLimitExceded="Category limit exceded";
    }
}
