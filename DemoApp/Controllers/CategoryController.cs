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
    public class CategoryController : Controller
    {
        private ApplicationDbContext context;
        public CategoryController(ApplicationDbContext applicationDbContext)
        {
            context = applicationDbContext;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View(context.Category.ToList());
        }
        public IActionResult Delete(int id)
        {
            var category = context.Category.Find(id);
            context.Category.Remove(category);
            context.SaveChanges();
            TempData["Message"] = "Delete category successfully !";
            return RedirectToAction("Index");
        }

        public IActionResult Detail(int id)
        {
            var category = context.Category.Include(b => b.Books)
                                           .FirstOrDefault(b => b.Id == id); 
                                     
            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

       
        [HttpPost]
        public IActionResult Create(Category category)
        {
        
            if (ModelState.IsValid)
            {
                context.Add(category);
                context.SaveChanges();
                TempData["Message"] = "Add category successfully !";
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(context.Category.Find(id));
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
             
            if (ModelState.IsValid)
            {                
                context.Update(category);
                context.SaveChanges();
                TempData["Message"] = "Edit category successfully !";
                return RedirectToAction(nameof(Index));
            }            
            return View(category);
        }
    }
}
