using Bullky_webRazor_Temp.DataRazor;
using Bullky_webRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bullky_webRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public Category? Category { get; set; }
        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }
         public void OnGet(int? id)
        {
            if(id!=null && id != 0)
            {
                Category = _context.Categories.Find(id);
 
            }
        }
        public IActionResult OnPost()
        {
            _context.Categories.Remove(Category);
            _context.SaveChanges();
            TempData["Success"] = "Category Deleted SuccessFully ";
            return RedirectToPage("Index");
        }

    }
}
