using DemoApp.Data;
using DemoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Controllers
{
    public class OrderController : Controller
    {
        //khai báo ApplicationDbContext để truy xuất và thay đổi dữ liệu của bảng
        private ApplicationDbContext context;
        public OrderController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        [Authorize(Roles = "Admin,Owner,Customer")]
        public IActionResult Index()
        {
            var order = context.Order.ToList();
            return View(order);
        }

        public IActionResult Delete(int id, int quantity)
        {
            var order = context.Order.Find(id);
            var book = context.Book.Find(id);
            context.Order.Remove(order);
            order.OrderQuantity = quantity;
            book.Quantity += quantity;
            context.SaveChanges();
            TempData["Message"] = "Delete successfully !";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Make(int id, int quantity)
        {
           
            var order = new Order();
            var book = context.Book.Find(id);
            order.Books = book;
            order.BookId = id;
            order.OrderQuantity = quantity;
            order.OrderPrice = book.Price * quantity;
            order.OrderDate = DateTime.Now;
            order.UserEmail = User.Identity.Name;
            context.Order.Add(order);
            book.Quantity -= quantity;
            context.Book.Update(book);
            context.SaveChanges();
            TempData["Success"] = "Order book successfully !";
            return RedirectToAction("Store", "Book");
        }
    }
}
