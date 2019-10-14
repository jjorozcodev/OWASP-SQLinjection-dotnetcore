using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLinjection_exposed.Data;

namespace SQLinjection_exposed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesDataController : ControllerBase
    {
        private SqlConnection conexionSql = new SqlConnection("Server=localhost;Database=AdventureWorks2016;User Id=sa;Password = 123;");
        private SqlCommand comandoSql = new SqlCommand();

        [HttpGet]
        [Route("GetInfo")]
        public string GetEmployeeInfo()
        {
            string query = $"SELECT TOP 10 E.BusinessEntityID, P.FirstName, P.LastName, E.JobTitle, E.HireDate FROM HumanResources.Employee E INNER JOIN Person.Person P ON E.BusinessEntityID = P.BusinessEntityID WHERE E.BusinessEntityID = {Request.Query["id"]}";

            comandoSql.Connection = conexionSql;
            comandoSql.CommandType = CommandType.Text;
            comandoSql.CommandText = query;

            if (conexionSql.State == ConnectionState.Closed)
            {
                conexionSql.Open();
            }
            
            SqlDataReader reader = comandoSql.ExecuteReader();
            
            if (reader.Read())
            {
                string returnString = string.Empty;
                returnString += $"ID : {reader["BusinessEntityID"]} ";
                returnString += $"Name : {reader["FirstName"]} ";
                returnString += $"Lastname : {reader["LastName"]} ";
                returnString += $"JobTitle : {reader["JobTitle"]} ";
                returnString += $"HireDate : {reader["HireDate"]} ";
                return returnString;
            }
            else
            {
                return string.Empty;
            }
        }

        // GET: api/EmployeesData
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            EmployeesData eData = new EmployeesData();
            return eData.GetEmployees();
        }

        // GET: api/EmployeesData/5
        [HttpGet("{id}", Name = "Get")]
        public Employee Get(string id)
        {
            EmployeesData eData = new EmployeesData();
            return eData.GetEmployeeById(id);
        }

        // POST: api/EmployeesData
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/EmployeesData/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
