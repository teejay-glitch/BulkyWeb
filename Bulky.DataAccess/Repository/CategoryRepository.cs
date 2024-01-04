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
    public class CategoryRepository : Repository<Category>, ICategoryRepository  // here Repository<Category> is a base class
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db) // by doing this we can pass values to constructors of base classes
        {
            _db = db;
        }

        //public void Save()  we no longer need this because we have moved this global method to unitOfWork
        //{
        //    _db.SaveChanges();
        //}
        public void Update(Category category)
        {
            _db.Update(category);
        }
    }
}
