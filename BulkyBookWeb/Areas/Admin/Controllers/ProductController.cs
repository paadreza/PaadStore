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
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public ProductController(IUnitOfWork uow, IWebHostEnvironment webHostEnvironment)
        {

            _UnitOfWork = uow;
            _WebHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _UnitOfWork.Product.GetAll(includeProperties: "Category").ToList();
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
            if (id == null || id == 0)
            {
                return View(productviewmodel);
            }
            else
            {
                productviewmodel.Product = _UnitOfWork.Product.Get(u => u.Id == id);
                return View(productviewmodel);
            }

        }
        [HttpPost]
        public IActionResult Upsert(ProductViewModel obj, IFormFile? File)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _WebHostEnvironment.WebRootPath;
                if (File != null)
                {
                    string fileName = Guid.NewGuid().ToString()+Path.GetExtension(File.FileName);
                    string productPath = Path.Combine(wwwRootPath,@"images\product");
                    if (! string.IsNullOrEmpty(obj.Product.ImageUrl))
                    {
                        var oldImagePath=Path.Combine(wwwRootPath,obj.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath)) 
                        {
                            
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName),FileMode.Create))
                    {
                        File.CopyTo(fileStream);
                    }
                    obj.Product.ImageUrl = @"\images\product\" + fileName;
                }
                if (obj.Product.Id == 0)
                {
                    _UnitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _UnitOfWork.Product.Update(obj.Product);
                }
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
