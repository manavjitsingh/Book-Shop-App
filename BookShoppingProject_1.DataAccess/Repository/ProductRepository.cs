using BookShoppingProject_1.Data;
using BookShoppingProject_1.DataAccess.Repository.IRepository;
using BookShoppingProject_1.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShoppingProject_1.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public void Update(Product product)
        {
            //_context.Update(product);
            var productInDb = _context.Products.FirstOrDefault(p => p.Id == product.Id);
            if(productInDb!=null)
            {
                if (productInDb.ImageUrl != "")
                    productInDb.ImageUrl = product.ImageUrl;
                productInDb.Title = product.Title;
                productInDb.Description = product.Description;
                productInDb.ISBN = product.ISBN;
                productInDb.Author = product.Author;
                productInDb.ListPrice = product.ListPrice;
                productInDb.Price50 = product.Price50;
                productInDb.Price100 = product.Price100;
                productInDb.Price = product.Price;
                productInDb.CategoryId = product.CategoryId;
                productInDb.CoverTypeId = product.CoverTypeId;
            }
        }
    }
}
