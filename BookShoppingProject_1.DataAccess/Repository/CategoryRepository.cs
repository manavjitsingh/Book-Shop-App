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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;
        public CategoryRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }
        public void Update(Category category)
        {
            _context.Update(category);
        }
    }
}
