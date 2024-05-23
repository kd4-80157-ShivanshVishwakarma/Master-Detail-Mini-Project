using MasterDetailProject.DTO;
using MasterDetailProject.Models;
using MasterDetailProject.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MasterDetailProject.Controllers
{
    public class OrderController : Controller
    {
        OrderRepository service;
        ProductRepository prodservice;
        public OrderController()
        {
            service= new OrderRepository();
            prodservice= new ProductRepository();
        }

        public IActionResult AddOrder()
        {
            return View();
        }

        public IActionResult Orders()
        {
            List<Order> orders = new List<Order>();
            orders = service.ListOrders();
            return View(orders);
        }

        public IActionResult Add(OrderDTO value) {
            Order ordr = new Order();
            ordr.CustomerID = value.CustomerID;
            ordr.OrderDate = DateTime.Now;
            ordr.TotalAmount = value.Quantity * value.UnitPrice;
            int orderID = service.Add(ordr);
            
            int prodID = prodservice.GetProductIdByName(value.ProductName);
            OrderDetail odetail = new OrderDetail();
            odetail.OrderID = orderID;
            odetail.ProductID = prodID;
            odetail.Quantity = value.Quantity;
            odetail.ProductName= value.ProductName;
            odetail.UnitPrice= value.UnitPrice;

            bool flag = service.AddOrderDetail(odetail);
            if (flag)
            {
                return RedirectToAction("Orders");
            }
            return RedirectToAction("AddOrder");
        }

        public IActionResult OrderDetail(int id) {
            OrderDetail od = service.GetOrderDetByOrdrId(id);
            if(od != null )
            {
                return View(od);
            }
            return RedirectToAction("Orders");
        }

    }
}
