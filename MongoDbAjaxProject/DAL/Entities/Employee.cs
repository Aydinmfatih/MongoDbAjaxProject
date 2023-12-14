using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MongoDbAjaxProject.DAL.Entities
{
    public class Employee
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeSurname { get; set; }
        public decimal EmployeeSalary { get; set; }
    }
}
