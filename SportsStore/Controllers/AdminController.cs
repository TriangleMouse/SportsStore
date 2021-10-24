using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            this.repository = repo;
        }

        public ActionResult Index() => View(repository.Products);

        public ActionResult Edit(int productId) =>
            View(repository.Products.FirstOrDefault(p=>p.ProductID==productId));
        
        [HttpPost]
        public IActionResult Edit(Product product)
        {
            //Смог ли процесс привязки модели проверить достоверность отправленных пользователем данных
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["message"] = $"{product.Name} has been saved";
                return RedirectToAction("Index");

            }
            else
            {
                //Что-то не так со значениями данных
                return View(product);
            }
        }

        public ActionResult Create() => View("Edit", new Product());

        [HttpPost]
        public IActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = $"{deletedProduct.Name} was deleted"; 
            }
            return RedirectToAction("Index");
        }
    }
}
