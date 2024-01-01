
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Microsoft.AspNetCore.Mvc;

namespace Bulky.Controllers
{
    public class CategoryController : Controller
    {
        //private readonly ApplicationDbContext _db;

        //public CategoryController(ApplicationDbContext db)
        //{
        //    _db = db;
        //}
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            //List<Category> categoryList = _db.Categories.ToList();
            List<Category> categoryList = _categoryRepository.GetAll().ToList();
            return View(categoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match with Name");
            };

            //if (category.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value");
            //};

            if (ModelState.IsValid)
            {
                //_db.Categories.Add(category);
                //_db.SaveChanges();
                _categoryRepository.Add(category);
                _categoryRepository.Save();
                TempData["success"] = "Category created successfully.";
                return RedirectToAction("Index", "Category");

            }

            return View();
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            //Category? category1 = _db.Categories.Find(id); // this will work only on primary key
            //Category? category2 = _db.Categories.FirstOrDefault(u => u.Id == id); // this looks for records and returns them, if not null
            //Category? category3 = _db.Categories.Where(c => c.Id == id).FirstOrDefault();

            Category category1 = _categoryRepository.GetFirstOrDefault(u => u.Id == id);

            if (category1 == null)
            {
                return NotFound();
            }            

            return View(category1);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            //if (category.Name == category.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The Display Order cannot exactly match with Name");
            //};

            //if (category.Name.ToLower() == "test")
            //{
            //    ModelState.AddModelError("", "Test is an invalid value");
            //};

            if (ModelState.IsValid)
            {
                //_db.Categories.Update(category);
                //_db.SaveChanges();
                _categoryRepository.Update(category);
                _categoryRepository.Save();
                TempData["success"] = "Category updated successfully.";
                return RedirectToAction("Index", "Category");
            }

            return View();
        }


        public IActionResult Delete(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            //Category? category = _db.Categories.FirstOrDefault(u => u.Id == id);
            Category category = _categoryRepository.GetFirstOrDefault(u => u.Id == id);

            if(category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int id)
        {
            //Category? category = _db.Categories.FirstOrDefault(u => u.Id == id);
            Category category = _categoryRepository.GetFirstOrDefault(u => u.Id == id);

            if (category == null)
            {
                return NotFound();
            }

            //_db.Categories.Remove(category);
            //_db.SaveChanges();
            _categoryRepository.Remove(category);
            _categoryRepository.Save();
            TempData["success"] = "Category deleted successfully.";

            return RedirectToAction("Index", "Category");                    
        }

    }
}
