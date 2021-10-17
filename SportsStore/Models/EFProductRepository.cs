using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models;
namespace SportsStore.Models
{
    public class EFProductRepository:IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext contxt)
        {
            this.context = contxt;
        }
        public IQueryable<Product> Products => context.Products;
    }
}
