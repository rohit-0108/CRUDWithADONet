using CRUDWithADONet.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CRUDWithADONet.DAL
{
    public class Employee_DAL
    {
        SqlConnection _sqlConnection = null;
        SqlCommand _command = null;
        public static IConfiguration configuration { get; set; }

        private string GetConnectionString()
        {
            var bauilder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).
                AddJsonFile("appsettings.json");
            configuration = bauilder.Build();
            return configuration.GetConnectionString("DefaultConnection");
        }

        public List<EmployeeModel> GetAll()
        {

            List<EmployeeModel> employeesList = new List<EmployeeModel>();
            using ( _sqlConnection= new SqlConnection(GetConnectionString()))
            {
                _command = _sqlConnection.CreateCommand();
                _command.CommandType=CommandType.StoredProcedure;
                _command.CommandText = "usp_Get_Employees";
                _sqlConnection.Open();
                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    EmployeeModel employee = new EmployeeModel();
                    employee.Id = Convert.ToInt32(reader["Id"]);
                    employee.FirstName=reader["FirstName"].ToString();
                    employee.LastName=reader["LastName"].ToString();
                    employee.DateOfBirth =Convert.ToDateTime(reader["DateOfBirth"]);
                    employee.Email=reader["Email"].ToString();
                    employee.Salary=Convert.ToDouble(reader["Salary"]);
                    employeesList.Add(employee);

                }
                _sqlConnection.Close();
            }
            return employeesList;
        }

        public bool Insert(EmployeeModel model)
        {
            int id = 0;
            using(_sqlConnection= new SqlConnection(GetConnectionString()))
            {
                _command= _sqlConnection.CreateCommand();
                _command.CommandType= CommandType.StoredProcedure;
                _command.CommandText = "usp_Insert_Employees";
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);
                _sqlConnection.Open();
              id= _command.ExecuteNonQuery();
                _sqlConnection.Close() ;
            }
            return id > 0 ? true : false;
        }

        public EmployeeModel GetById(int id)
        {

            EmployeeModel employeeModel = new EmployeeModel();
            using (_sqlConnection = new SqlConnection(GetConnectionString()))
            {
                _command = _sqlConnection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_GetById_Employees";
                _command.Parameters.AddWithValue("@Id", id);
                _sqlConnection.Open();
                SqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                   
                    employeeModel.Id = Convert.ToInt32(reader["Id"]);
                    employeeModel.FirstName = reader["FirstName"].ToString();
                    employeeModel.LastName = reader["LastName"].ToString();
                    employeeModel.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    employeeModel.Email = reader["Email"].ToString();
                    employeeModel.Salary = Convert.ToDouble(reader["Salary"]);
                   

                }
                _sqlConnection.Close();
            }
            return employeeModel;
        }


        public bool Update(EmployeeModel model)
        {
            int id = 0;
            using (_sqlConnection = new SqlConnection(GetConnectionString()))
            {
                _command = _sqlConnection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_update_Employees";
                _command.Parameters.AddWithValue("@Id", model.Id);
                _command.Parameters.AddWithValue("@FirstName", model.FirstName);
                _command.Parameters.AddWithValue("@LastName", model.LastName);
                _command.Parameters.AddWithValue("@DateOfBirth", model.DateOfBirth);
                _command.Parameters.AddWithValue("@Email", model.Email);
                _command.Parameters.AddWithValue("@Salary", model.Salary);
                _sqlConnection.Open();
                id = _command.ExecuteNonQuery();
                _sqlConnection.Close();
            }
            return id > 0 ? true : false;
        }


        public bool Delete(int id)
        {
            int result = 0;
            using (_sqlConnection = new SqlConnection(GetConnectionString()))
            {
                _command = _sqlConnection.CreateCommand();
                _command.CommandType = CommandType.StoredProcedure;
                _command.CommandText = "usp_delete_Employees";
                _command.Parameters.AddWithValue("@Id",id);
                _sqlConnection.Open();
                result = _command.ExecuteNonQuery();
                _sqlConnection.Close();
            }
            return result > 0 ? true : false;
        }
    }
}
