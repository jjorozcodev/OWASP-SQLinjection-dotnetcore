using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace SQLinjection_exposed.Data
{
    public class EmployeesData
    {
        private SqlConnection conexionSql = new SqlConnection("Server=localhost;Database=AdventureWorks2016;User Id=sa;Password = 123;");
        private SqlCommand comandoSql = new SqlCommand();

        public List<Employee> GetEmployees()
        {
            string query = @"SELECT TOP 10 E.BusinessEntityID, P.FirstName, P.LastName, E.JobTitle, E.HireDate FROM HumanResources.Employee E INNER JOIN Person.Person P ON E.BusinessEntityID = P.BusinessEntityID";

            comandoSql.Connection = conexionSql;
            comandoSql.CommandType = CommandType.Text;
            comandoSql.CommandText = query;

            if (conexionSql.State == ConnectionState.Closed)
            {
                conexionSql.Open();
            }

            SqlDataReader reader = comandoSql.ExecuteReader();

            List<Employee> employeesList = new List<Employee>();

            while (reader.Read())
            {
                Employee emp = new Employee();
                emp.BusinessEntityID = int.Parse(reader["BusinessEntityID"].ToString());
                emp.FirstName = reader["FirstName"].ToString();
                emp.LastName = reader["LastName"].ToString();
                emp.JobTitle = reader["JobTitle"].ToString();
                emp.HireDate = DateTime.Parse(reader["HireDate"].ToString());
                employeesList.Add(emp);
            }

            reader.Close();

            conexionSql.Close();

            return employeesList;
        }

        public Employee GetEmployeeById(string id)
        {
            string query = string.Format("SELECT E.BusinessEntityID, P.FirstName, P.LastName, E.JobTitle, E.HireDate FROM HumanResources.Employee E INNER JOIN Person.Person P ON E.BusinessEntityID = P.BusinessEntityID WHERE E.BusinessEntityID = {0}", id);

            comandoSql.Connection = conexionSql;
            comandoSql.CommandType = CommandType.Text;
            comandoSql.CommandText = query;

            if (conexionSql.State == ConnectionState.Closed)
            {
                conexionSql.Open();
            }

            SqlDataReader reader = comandoSql.ExecuteReader();

            Employee emp = new Employee();

            while (reader.Read())
            {
                emp.BusinessEntityID = int.Parse(reader["BusinessEntityID"].ToString());
                emp.FirstName = reader["FirstName"].ToString();
                emp.LastName = reader["LastName"].ToString();
                emp.JobTitle = reader["JobTitle"].ToString();
                emp.HireDate = DateTime.Parse(reader["HireDate"].ToString());
            }

            reader.Close();

            conexionSql.Close();

            return emp;
        }
    }
}
