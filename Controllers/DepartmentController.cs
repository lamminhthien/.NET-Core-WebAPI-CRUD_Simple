using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Get all department
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
            select DepartmentId, DepartmentName from dbo.Department";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using(SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using(SqlCommand sqlCommand = new SqlCommand(query,myConn))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }

            }

            return new JsonResult(table);
        }


        //Add new Department
        [HttpPost]
        public JsonResult Post(Department department)
        {
            string query = @"
                insert into dbo.Department values
                ('" + department.DepartmentName + "')";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myConn))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }

            }
            return new JsonResult("Added Successfully");
        }   
    
        // Update DepartmentName
        [HttpPut]
        public JsonResult Put(Department department)
        {
            string query = @" update dbo.Department set DepartmentName = '" + department.DepartmentName + @"'
                    where DepartmentID = " + department.DepartmentId + ";";
                
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myConn))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }

            }
            return new JsonResult("Update Successfully");
        } // Delete DepartmentName
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @" delete from  dbo.Department " + @"
                    where DepartmentID = " + id + ";";
                
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            SqlDataReader myReader;
            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myConn))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myConn.Close();
                }

            }
            return new JsonResult("Delete Successfully");
        }
    }
}
