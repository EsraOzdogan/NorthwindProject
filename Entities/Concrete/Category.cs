using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //çıplak class kalmasın
    public class Category:IEntity  //Ientityden implementasyonuna sahip oluğu için yani category bit veritabanı tablosu demektir
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
