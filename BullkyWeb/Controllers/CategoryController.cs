using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;
namespace BullkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _CategoryRepo;

        public CategoryController(ICategoryRepository context)    
        {

            _CategoryRepo = context;

        }

        public IActionResult Index()
        {
         List<Category> objCategory = _CategoryRepo.GetAll().ToList();

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
            _CategoryRepo.Add(obj);
                _CategoryRepo.Save();
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
          Category  catedit=_CategoryRepo.Get(i=> i.Id==id);
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
                _CategoryRepo.Update(obj);
                _CategoryRepo.Save();
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
            Category catedit = _CategoryRepo.Get(i => i.Id == id); 
            if (catedit == null)
            {
                return NotFound();
            }

            return View(catedit);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost (int? id)
        {
            Category? obj = _CategoryRepo.Get(i => i.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
           _CategoryRepo.Remove(obj);
            _CategoryRepo.Save();
            TempData["Success"] = "Category Deleted SuccessFully ";
            return RedirectToAction("Index");
           
        }
    }
}
