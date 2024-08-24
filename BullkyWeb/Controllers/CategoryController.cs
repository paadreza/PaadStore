using Bulky.DataAccess.Data;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
namespace BullkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)    
        {

            _context = context;

        }

        public IActionResult Index()
        {
         List<Category> objCategory = _context.Categories.ToList();

            return View(objCategory);
        }

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
           if (obj.Name == obj.DisplayOrder.ToString())
           {
               ModelState.AddModelError("name", "dotasham yeki zadi dowsh");
            }
           

            if (ModelState.IsValid) 
            {
            _context.Categories.Add(obj);
            _context.SaveChanges();
                TempData["Success"] = "Category Created SuccessFully ";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if(id==null|| id == 0)
            {
                return NotFound();
            }
          Category  catedit= _context.Categories.Find(id);
            if (catedit == null)
            {
                return NotFound();
            }

            return View(catedit);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            

            if (ModelState.IsValid)
            {
                _context.Categories.Update(obj);
                _context.SaveChanges();
                TempData["Success"] = "Category Update SuccessFully ";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category catedit = _context.Categories.Find(id);
            if (catedit == null)
            {
                return NotFound();
            }

            return View(catedit);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost (int? id)
        {
            Category? obj = _context.Categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(obj);
            _context.SaveChanges();
            TempData["Success"] = "Category Deleted SuccessFully ";
            return RedirectToAction("Index");
           
        }
    }
}
