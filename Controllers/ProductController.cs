using MasterDetailProject.DTO;
using MasterDetailProject.Models;
using MasterDetailProject.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MasterDetailProject.Controllers
{
    public class ProductController : Controller
    {
        ProductRepository service;
        public ProductController()
        {
               service = new ProductRepository();
        }

        public IActionResult GetList()
        {
            List<ProductDTO> prods = service.GetList();
            return View(prods);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult Add(Products prod) {
            bool flag = service.Add(prod);
            if(flag)
            {
                return RedirectToAction("GetList");
            }
            return RedirectToAction("AddProduct");
        }
    }
}
