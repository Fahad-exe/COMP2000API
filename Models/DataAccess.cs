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
        public DbSet<StudentProject> StudentProjects { get; set; }

        public DataAccess(DbContextOptions<DataAccess> options) : base(options)
        {
            _connection = Database.GetConnectionString();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentProject>()
                .ToView(nameof(StudentProjects))
                .HasKey(t => t.ProjectID);
        }

        public void Create(StudentProject sp)
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

                    sql.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(StudentProject sp)
        {
            using (SqlConnection sql = new SqlConnection(_connection))
            {
                using (SqlCommand cmd = new SqlCommand("EditStudentProject", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ProjectID", sp.ProjectID));
                    cmd.Parameters.Add(new SqlParameter("@Title", string.IsNullOrEmpty(sp.Title) ? (object)DBNull.Value : sp.Title));
                    cmd.Parameters.Add(new SqlParameter("@Description", string.IsNullOrEmpty(sp.Description) ? (object)DBNull.Value : sp.Description));
                    cmd.Parameters.Add(new SqlParameter("@Year", sp.Year));
                    cmd.Parameters.Add(new SqlParameter("@thumbnailURL", string.IsNullOrEmpty(sp.ThumbnailURL) ? (object)DBNull.Value : sp.ThumbnailURL));
                    cmd.Parameters.Add(new SqlParameter("@posterURL", string.IsNullOrEmpty(sp.PosterURL) ? (object)DBNull.Value : sp.PosterURL));

                    sql.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection sql = new SqlConnection(_connection))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteProject", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@ProjectID", id));

                    sql.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void SaveStudentPhoto(int StudentID, Byte[] photo)
        {
            using (SqlConnection sql = new SqlConnection(_connection))
            {
                using (SqlCommand cmd = new SqlCommand("SaveStudentPhoto", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@StudentID", StudentID));
                    cmd.Parameters.Add(new SqlParameter("@photo", photo));

                    sql.Open();

                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}
