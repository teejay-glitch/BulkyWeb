using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Product product)
        {
            //_db.Update(product);
            Product productObj = _db.Products.FirstOrDefault(u => u.Id == product.Id);

            if(productObj != null)
            {
                productObj.Title = product.Title;
                productObj.Description = product.Description;
                productObj.Author = product.Author;
                productObj.ISBN = product.ISBN;
                productObj.ListPrice = product.ListPrice;
                productObj.Price = product.Price;
                productObj.Price50 = product.Price50;
                productObj.Price100 = product.Price100;
                productObj.Category = product.Category;
                productObj.ProductImages = product.ProductImages;
            }
        }
    }
}
