using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDbAjaxProject.DAL.Entities;
using MongoDbAjaxProject.DAL.Settings;

namespace MongoDbAjaxProject.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        public CategoryController(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
        }
        public async Task<IActionResult> Index()
        {
            var values = await _categoryCollection.Find(x=>true).ToListAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateCategory(Category category)
        {
            await _categoryCollection.InsertOneAsync(category);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> DeleteCategory(string id)
        {
            await _categoryCollection.DeleteOneAsync(x=>x.CategoryId == id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCategory(string id)
        {
          var values =  await _categoryCollection.Find(x=>x.CategoryId==id).FirstOrDefaultAsync() ;
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            await _categoryCollection.FindOneAndReplaceAsync(x => x.CategoryId == category.CategoryId,category);
            return RedirectToAction("Index");
        }
    }
}
