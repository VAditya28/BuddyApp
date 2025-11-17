using Buddy.DataAccess.Repository.IRepository;
using Buddy.DataAccess.Data;
using Buddy.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BuddyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> Categories = _unitOfWork.Category.GetAll().ToList();
            return View(Categories);
        }

        // when you do not define anything its a get method by deafult
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Name and Display Order cannot be same");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category Created successfully";
                return RedirectToAction("Index", "Category");
            }

            return View();



        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categorydb1 = _unitOfWork.Category.Get(u => u.category_Id == id);
            // Category? categorydb = _db.Categories.FirstOrDefault(u=>u.category_Id==id);

            if (categorydb1 == null)
            {
                return NotFound();
            }

            return View(categorydb1);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category updated successfully";

                return RedirectToAction("Index", "Category");
            }

            return View();






        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categorydb1 = _unitOfWork.Category.Get(u => u.category_Id == id);
            // Category? categorydb = _db.Categories.FirstOrDefault(u => u.category_Id == id);

            if (categorydb1 == null)
            {
                return NotFound();
            }

            return View(categorydb1);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Category? categorydb = _unitOfWork.Category.Get(u => u.category_Id == id);
            if (categorydb == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(categorydb);
            _unitOfWork.Save();
            TempData["Success"] = "Category Deleted successfully";
            return RedirectToAction("Index", "Category");








        }


    }
}
