using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> companies = _unitOfWork.CompanyRepository.GetAll().ToList();
            return View(companies);
        }

        public IActionResult Upsert(int? id)
        {
            Company company;

            if (id == null || id == 0)
            {
                company = new();
                return View(company);
            } else
            {
                company = _unitOfWork.CompanyRepository.GetFirstOrDefault(u => u.Id == id);
                return View(company);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if(company.Id != 0)
            {
                _unitOfWork.CompanyRepository.Update(company);
            } else
            {
                _unitOfWork.CompanyRepository.Add(company);
            }

            _unitOfWork.Save();
            TempData["success"] = "Company created successfully";
            return RedirectToAction("Index", "Company");
        }

        #region
        //API
        public IActionResult GetAll()
        {
            IEnumerable<Company> companyList = _unitOfWork.CompanyRepository.GetAll().ToList();
            return Json(new { data = companyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return Json(new {success = false, message = "Invalid Request."});

            Company company = _unitOfWork.CompanyRepository.GetFirstOrDefault(u => u.Id == id);
            if(company == null) return Json(new { success = false, message = "Company not found." });

            _unitOfWork.CompanyRepository.Remove(company);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Company deleted successfully." });
        }
        #endregion
    }
}
