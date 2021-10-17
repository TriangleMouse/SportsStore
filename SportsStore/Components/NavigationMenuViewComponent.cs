using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Components
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private IProductRepository repositry;

        public NavigationMenuViewComponent(IProductRepository repo)
        {
            this.repositry = repo;
        }

        public IViewComponentResult Invoke()
        {
            //генерируем категории. Также череез вьюбаг передаем выбранную категорию
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repositry.Products.Select(x=>x.Category).Distinct().OrderBy(x=>x));
        }
    }
}
