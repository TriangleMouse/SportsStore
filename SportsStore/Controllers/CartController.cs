using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SportsStore.Models;
using SportsStore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repositrory;
        private Cart cart;
        public CartController(IProductRepository repo,Cart cartService)
        {
            this.repositrory = repo;
            this.cart = cartService;
        }

        public ActionResult Index(string returnUrl) => View(
                new CartIndexViewModel
                {
                    Cart = cart,
                    ReturnUrl = returnUrl
                }
            );

        public RedirectToActionResult AddToCart(int productId,string returnUrl)
        {
            Product product = repositrory.Products.FirstOrDefault(p=>p.ProductID==productId);
            if (product!=null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repositrory.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
    }
}
