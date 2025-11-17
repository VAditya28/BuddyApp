using Buddy.DataAccess.Repository.IRepository;
using Buddy.Models.Models;
using Buddy.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;

namespace BuddyApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
                _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Product> Products= _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
           
            return View(Products);
        }

        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category
            //   .GetAll().Select(u => new SelectListItem
            //   {
            //       Text = u.Name,
            //       Value = u.category_Id.ToString()
            //   });
            // ViewBag.CategoryList = CategoryList;
            //ViewData["CategoryList"] = CategoryList;
            //   ViewData[nameof(CategoryList)] = CategoryList;  


            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.category_Id.ToString()
                }),

                Product = new Product()
            };

            //create
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id==id);
                return View(productVM);
            }
            


           
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file)
        {
            if ((obj.Product.ISBN.Length) > 15)
            {
                ModelState.AddModelError("Name","ISBN cannot exceed 15 characters");
            }

            if (ModelState.IsValid) {

                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null) { 
                    string filename= Guid.NewGuid().ToString()+ Path.GetExtension(file.FileName);
                    string productpath= Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(obj.Product.Imageurl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, obj.Product.Imageurl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath)) 
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using(var filestream= new FileStream(Path.Combine(productpath, filename), FileMode.Create))
                    {
                        file.CopyTo(filestream);
                    }
                
                    obj.Product.Imageurl = @"\images\product\" +filename;
                }

                if (obj.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(obj.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(obj.Product);
                }


               
                _unitOfWork.Save();
                TempData["Success"] = "Book Added Successfully!";
                return RedirectToAction("Index", "Product");
            }
            else
            {
                obj.CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.category_Id.ToString()
                });
                

            }
            return View(obj);
        }


        //public IActionResult Edit(int? id) { 
        //    if(id==null|| id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Product? productdb = _unitOfWork.Product.Get(u => u.Id == id);
        //    if (productdb == null) { 
            
        //        return NotFound();
        //    }

        //    return View(productdb);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product product) {

        //    if (ModelState.IsValid)
        //    {
        //       _unitOfWork.Product.Update(product);
        //        _unitOfWork.Save();
        //        TempData["Success"] = "Book Updated Successfully!";
        //        return RedirectToAction("Index");
               
        //    }
        //    return View();



        //}


        public IActionResult Delete(int? id) { 
        
            if(id==null|| id == 0)
            {

                return NotFound();
            }

            Product? productdb = _unitOfWork.Product.Get(u=>u.Id==id);
            if (productdb == null)
            {
                return NotFound();
            }
            return View(productdb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int ? id) 
        {


            Product? productdb = _unitOfWork.Product.Get(u => u.Id == id);
            if (productdb == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(productdb);
            _unitOfWork.Save();
            TempData["Success"] = "Book Deleted Successfully!";
            return RedirectToAction("Index", "Product");

        }






        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() {

            List<Product> Products = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return Json(new {data= Products});
        }

        #endregion

    }
}
