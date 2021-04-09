using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Product :IEntity
    {
        public int ProductId { get; set; } //Primary Key
        public int CategoryId { get; set; } //Foreign Key
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
