using Builky.Utility;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public CategoryController(IUnitOfWork uow)
        {

            _UnitOfWork = uow;

        }

        public IActionResult Index()
        {
            List<Category> objCategory = _UnitOfWork.Category.GetAll().ToList();

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
                _UnitOfWork.Category.Add(obj);
                _UnitOfWork.Save();
                TempData["Success"] = "Category Created SuccessFully ";
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category catedit = _UnitOfWork.Category.Get(i => i.Id == id);
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
                _UnitOfWork.Category.Update(obj);
                _UnitOfWork.Save();
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
            Category catedit = _UnitOfWork.Category.Get(i => i.Id == id);
            if (catedit == null)
            {
                return NotFound();
            }

            return View(catedit);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _UnitOfWork.Category.Get(i => i.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Category.Remove(obj);
            _UnitOfWork.Save();
            TempData["Success"] = "Category Deleted SuccessFully ";
            return RedirectToAction("Index");

        }
    }
}
