using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository :Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Product obj)
        {
            //_context.Products.Update(obj);
                var objFormDb=_context.Products.FirstOrDefault(U=>U.Id==obj.Id);
            if (objFormDb != null) 
            {
                objFormDb.Title = obj.Title;
                objFormDb.Description = obj.Description;
                objFormDb.Price = obj.Price;
                objFormDb.ISBN = obj.ISBN;
                objFormDb.Price100= obj.Price100;
                objFormDb.Price50 = obj.Price50;
                objFormDb.ListPrice = obj.ListPrice;
                objFormDb.Author= obj.Author;
                objFormDb.CategoryId = obj.CategoryId;
                if (obj.ImageUrl != null) 
                {
                    objFormDb.ImageUrl = obj.ImageUrl;
                }
                
            }

        }
    }
}
