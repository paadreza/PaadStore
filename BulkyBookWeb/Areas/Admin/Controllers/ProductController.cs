using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;

        public ProductController(IUnitOfWork uow)
        {

            _UnitOfWork = uow;

        }

        public IActionResult Index()
        {
            List<Product> objProductList = _UnitOfWork.Product.GetAll().ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _UnitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }
                );
            ProductViewModel productviewmodel = new()
            {
                CategoryList = CategoryList,
                Product = new Product()
            };
            if(id==null || id == 0)
            {
                return View(productviewmodel);
            }
            else
            {
                productviewmodel.Product=_UnitOfWork.Product.Get(u=>u.Id==id);
                return View(productviewmodel);
            }
            
        }
        [HttpPost]
        public IActionResult Upsert(ProductViewModel obj,IFormFile? File )
        {
            if (ModelState.IsValid)
            {
                _UnitOfWork.Product.Add(obj.Product);
                _UnitOfWork.Save();
                TempData["Success"] = "Product Created SuccessFully ";
                return RedirectToAction("Index");
            }
            else
            {
                obj.CategoryList = _UnitOfWork.Category.GetAll().Select(
                u => new SelectListItem
                 {
                     Text = u.Name,
                    Value = u.Id.ToString()
                }
                );

                return View(obj.CategoryList);
            }
            //return View();
        }
        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? prodedit = _UnitOfWork.Product.Get(i => i.Id == id);
        //    if (prodedit == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(prodedit);
        //}
        //[HttpPost]
        //public IActionResult Edit(Product obj)
        //{


        //    if (ModelState.IsValid)
        //    {
        //        _UnitOfWork.Product.Update(obj);
        //        _UnitOfWork.Save();
        //        TempData["Success"] = "Product Update SuccessFully ";
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Product prodedit = _UnitOfWork.Product.Get(i => i.Id == id);
            if (prodedit == null)
            {
                return NotFound();
            }

            return View(prodedit);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Product? obj = _UnitOfWork.Product.Get(i => i.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Product.Remove(obj);
            _UnitOfWork.Save();
            TempData["Success"] = "Product Deleted SuccessFully ";
            return RedirectToAction("Index");

        }
    }
}
