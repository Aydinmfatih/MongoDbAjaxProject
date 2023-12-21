using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDbAjaxProject.DAL.Entities;
using MongoDbAjaxProject.DAL.Settings;
using Newtonsoft.Json;

namespace MongoDbAjaxProject.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IMongoCollection<Employee> _employeeCollection;
        public EmployeeController(IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _employeeCollection = database.GetCollection<Employee>(_databaseSettings.EmployeeCollectionName);
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> EmployeeList()
        {
            var values = await _employeeCollection.Find(x => true).ToListAsync();
            var jsonEmployees = JsonConvert.SerializeObject(values);
            return Json(jsonEmployees);
        }
        [HttpPost]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            await _employeeCollection.InsertOneAsync(employee);
            var values = JsonConvert.SerializeObject(employee);
            return Json(values);

        }

        public async Task<IActionResult> GetEmployee(string employeeId)
        {
            var values = await _employeeCollection.Find(x => x.EmployeeId == employeeId).FirstOrDefaultAsync();
            var jsonValues = JsonConvert.SerializeObject(values);
            return Json(jsonValues);

        }
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var values = await _employeeCollection.DeleteOneAsync(x => x.EmployeeId == id);
            return NoContent();

        }
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            var values = await _employeeCollection.FindOneAndReplaceAsync(x => x.EmployeeId == employee.EmployeeId, employee);
            return NoContent();

        }


    }
}
