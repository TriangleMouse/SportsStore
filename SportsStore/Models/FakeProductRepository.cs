using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models;
namespace SportsStore.Models
{
    //фиктивная реализацию интерфейса, которая будет замещать хранилище данных до тех пор пока я им не займусь
    public class FakeProductRepository /*: IProductRepository*/
    {
        public IQueryable<Product> Products => new List<Product>
        {
            new Product{Name="Football",Price=25},
            new Product{Name="Surf board",Price=179},
            new Product{Name="Running shoes",Price=95}
        }.AsQueryable<Product>();//AsQueryable() применяется для преобразования фиксированной коллекции объектов в IQueryable<Product>, чтобы реализовать интерфейс IProductRepository
    }
}
