using DemoApp.Data;
using DemoApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Controllers
{
    public class BookController : Controller
    {

        private ApplicationDbContext context;
        public BookController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var books = context.Book.OrderByDescending(b=>b.Id).ToList();
            return View(books);
        }

        [Authorize(Roles = "Admin,Owner, Customer")]
        public IActionResult Store()
        {
            return View(context.Book.ToList());
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var book = context.Book.Find(id);
            context.Book.Remove(book);
            context.SaveChanges();
            TempData["Message"] = "Delete book successfully !";
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin, Customer")]
        public IActionResult Detail(int id)
        {
            var book = context.Book.Include(c => c.Category)  
                                   .FirstOrDefault(c => c.Id == id);
            return View(book);
        }


       
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Category = context.Category.ToList();
            return View();
        }

       
        [HttpPost]
        public IActionResult Create(Book book)
        {
            
            if (ModelState.IsValid)
            {
                
                context.Add(book);
                context.SaveChanges();
                TempData["Message"] = "Add book successfully !";
                return RedirectToAction(nameof(Index));
            }
            
            return View(book);
        }

        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Category = context.Category.ToList();
            return View(context.Book.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            
            if (ModelState.IsValid)
            {                
                context.Update(book);
                context.SaveChanges();
                TempData["Message"] = "Edit book successfully !";
                return RedirectToAction(nameof(Index));
            }
            
            return View(book);
        }

        public IActionResult PriceAsc()
        {
            var books = context.Book.OrderBy(b => b.Price).ToList();
            return View("Store", books);
        }

        public IActionResult PriceDesc()
        {
            var books = context.Book.OrderByDescending(b => b.Price).ToList();
            return View("Store", books);
        }

        [HttpPost]
        public IActionResult Search(string keyword)
        {
            var books = context.Book.Where(b => b.Name.Contains(keyword)).ToList();
            return View("Store", books);
        }
    }
}
