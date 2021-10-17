using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models;
using SportsStore.Models.ViewModels;
namespace SportsStore.Controllers
{
    public class ProductController : Controller
    {
        public int PageSize = 3;//для разбиения на страницы

        private IProductRepository repository;

        public ProductController(IProductRepository repo)
        {
            this.repository = repo;
        }

         
        public ActionResult List(string category, int productPage = 1) =>
            View(new ProductsListViewModel {
                Products = repository.Products.Where(p => category == null || p.Category == category)
                .OrderBy(p => p.ProductID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems =category==null ?repository.Products.Count() : repository.Products.Where(p=>p.Category==category).Count()//если категория была выбрана, тогда возвращаем кол позиций иначе общее кол товаров.
                },
                CurrentCategory = category

            });
    }
}
