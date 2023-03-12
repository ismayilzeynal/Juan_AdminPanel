using JuanProject.DAL;
using JuanProject.Models;
using JuanProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JuanProject.Areas.AdminArea.Controllers
{
    [Area("AdminArea")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public CategoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            List<CategoryVM> categoryVM = new();
            foreach (var item in _appDbContext.Categories.ToList())
            {
                CategoryVM category = new();
                category.Name = item.Name;
                categoryVM.Add(category);
            }
            return View(categoryVM);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CategoryCreateVM categoryVM)
        {
            if (!ModelState.IsValid) return View();

            bool isExist = _appDbContext.Categories.Any(c => c.Name.ToLower().Trim() == categoryVM.Name.ToLower().Trim());
            if (isExist)
            {
                ModelState.AddModelError("Name", "This name is already taken");
                return View();
            }

            Category category = new()
            {
                Name = categoryVM.Name
            };
            _appDbContext.Categories.Add(category);
            _appDbContext.SaveChanges();


            return RedirectToAction("Index");
        }
        public IActionResult Detail(string name)
        {
            Category category = _appDbContext.Categories.FirstOrDefault(c => c.Name == name);
            if (category == null) return NotFound();
            CategoryVM categoryVM = new()
            {
                Name = category.Name
            };
            return View(categoryVM);
        }

        public IActionResult Delete(string name)
        {
            Category category = _appDbContext.Categories.FirstOrDefault(c => c.Name == name);
            if (category == null) return NotFound();
            _appDbContext.Categories.Remove(category);
            _appDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        //public IActionResult Edit(string name)
        //{
        //    Category category = _appDbContext.Categories.FirstOrDefault(c => c.Name == name);
        //    if (category == null) return NotFound();
        //    CategoryVM categoryVM = new()
        //    {
        //        Name = category.Name
        //    };
        //    return View(categoryVM);
        //}
        public IActionResult Edit(string name)
        {
            return View(_appDbContext.Categories.FirstOrDefault(c => c.Name == name));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int id, CategoryCreateVM category)
        {
            if (id == null) return NotFound();
            Category ExistCategory = _appDbContext.Categories.Find(id);
            if (!ModelState.IsValid) return View();
            bool isExist = _appDbContext.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (isExist)
            {
                ModelState.AddModelError("Name", "This name is already taken");
                return View();
            }
            if (ExistCategory == null) return NotFound();
            ExistCategory.Name = category.Name;
            _appDbContext.SaveChanges();

            return RedirectToAction("Index");
        }




    }
}
