using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {
    [HttpGet("/items")]
    public ActionResult Index()
    {
      List<Item> allItems = Item.GetAll();
      return View(allItems);
    }

    [HttpGet("/items/new")]
    public ActionResult New()
    {
      return View();
    }

    // [HttpPost("/items")]
    // public ActionResult Create(string description, string details)
    // {
    //   Item myItem = new Item(description, details);
    //   return RedirectToAction("Index");
    // }

    // [HttpPost("/items/delete")]
    // public ActionResult DeleteAll()
    // {
    //   Item.ClearAll();
    //   return View();
    // }

   
  
  [HttpGet("/categories/{categoryId}/items/new")]
  public ActionResult New(int categoryId)
  {
     Category category = Category.Find(categoryId);
     return View(category);
  }
  }
}