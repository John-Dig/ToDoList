using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
  public class CategoriesController : Controller
  {
    [HttpGet("/categories")]
    public ActionResult Index()
    {
      List<Category> allCategories = Category.GetAll();
      return View(allCategories);
    }
    [HttpGet("/categories/new")]
    public ActionResult New()
    {
      return View();
    }
    // This one creates new Items within a given Category, not new Categories:

[HttpPost("/categories/{categoryId}/items")]
public ActionResult Create(int categoryId, string itemDescription)
{
  Dictionary<string, object> model = new Dictionary<string, object>();
  Category foundCategory = Category.Find(categoryId);
  Item newItem = new Item(itemDescription, "dang string");
  foundCategory.AddItem(newItem);
  List<Item> categoryItems = foundCategory.Items;
  model.Add("items", categoryItems);
  model.Add("category", foundCategory);
  return View("Show", model);
}

  }
}