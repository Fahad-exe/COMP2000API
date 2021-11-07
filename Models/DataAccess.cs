using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace COMP2000API.Models
{
    public class DataAccess : DbContext
    {
        private readonly string _connection;

        public DataAccess(IConfiguration configuration)
        {
            _connection = configuration.GetConnectionString("COMP2001_DB");
        }

        public bool Create(StudentProject sp)
        {
            using (SqlConnection sql = new SqlConnection(_connection))
            {
                using (SqlCommand cmd = new SqlCommand("CreateStudentProject", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@StudentID", sp.StudentID));
                    cmd.Parameters.Add(new SqlParameter("@fName", sp.First_Name));
                    cmd.Parameters.Add(new SqlParameter("@lName", sp.Second_Name));
                    cmd.Parameters.Add(new SqlParameter("@Title", sp.Title));
                    cmd.Parameters.Add(new SqlParameter("@Description", sp.Description));
                    cmd.Parameters.Add(new SqlParameter("@Year", sp.Year));
                }
            }
            return false;
        }
    }
}
