using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using StudentExcersisesMVC.Models;

namespace StudentExcersisesMVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IConfiguration _config;

public StudentsController(IConfiguration config)
{
    _config = config;
}

public SqlConnection Connection
{
    get
    {
        return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    }
}
        // GET: Students
        public ActionResult Index()
        {
using (SqlConnection conn = Connection)
{
    conn.Open();
    using (SqlCommand cmd = conn.CreateCommand())
    {
        cmd.CommandText = @"
            SELECT s.Id,
                s.FirstName,
                s.LastName,
                s.SlackHandle,
                s.CohortId
            FROM Student s
        ";
        SqlDataReader reader = cmd.ExecuteReader();

        List<Student> students = new List<Student>();
        while (reader.Read())
        {
            Student student = new Student
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                CohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
            };

            students.Add(student);
        }

        reader.Close();

        return View(students);
    }
}
        }

        // GET: Students/Details/5
        public ActionResult Details(int id)
        {
using (SqlConnection conn = Connection)
{
    conn.Open();
    using (SqlCommand cmd = conn.CreateCommand())
    {
        cmd.CommandText = @"
            SELECT s.Id,
                s.FirstName,
                s.LastName,
                s.SlackHandle,
                s.CohortId
            FROM Student s
        ";
        SqlDataReader reader = cmd.ExecuteReader();

        Student student = null;
        while (reader.Read())
        {


            student = new Student
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                SlackHandle = reader.GetString(reader.GetOrdinal("SlackHandle")),
                CohortId = reader.GetInt32(reader.GetOrdinal("CohortId"))
            };


        }

        reader.Close();

        return View(student);
    } 
}
}

        // GET: Students/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Student student) 
        {
                try
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();

                    using (SqlCommand cmd = conn.CreateCommand())

                    {

                        cmd.CommandText = @"UPDATE Student
                                            SET FirstName = @firstName, LastName = @lastName, SlackHandle = @slackHandle, CohortId = @cohortId
                                            WHERE Id = @id";

                        cmd.Parameters.Add(new SqlParameter("@firstName", student.FirstName));
                        cmd.Parameters.Add(new SqlParameter("@lastName", student.LastName));
                        cmd.Parameters.Add(new SqlParameter("@slackHandle", student.SlackHandle));
                        cmd.Parameters.Add(new SqlParameter("@cohortId", student.CohortId));
                        cmd.Parameters.Add(new SqlParameter("@id", id));

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)

                        {
                            return new StatusCodeResult(StatusCodes.Status204NoContent);
                        }
                        throw new Exception("No rows affected");
                    }
                }
            }

            catch (Exception)

            {
                if (!StudentExists(id))

                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            bool StudentExists(int id)

        {

            using (SqlConnection conn = Connection)

            {

                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())

                {

                    cmd.CommandText = @"Select Id, FirstName, LastName, SlackHandle, CohortId
                                        FROM Student
                                        Where Id = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    SqlDataReader reader = cmd.ExecuteReader();

                    return reader.Read();

                } 
            }
        }
    }

        // GET: Students/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Students/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
} 



            