using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using COMP2000API.Models;
using Microsoft.EntityFrameworkCore;

namespace COMP2000API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly DataAccess _database;

        public StudentsController(DataAccess database)
        {
            _database = database;
        }

        //GET api/students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentProject>>> GetStudentProject()
        {
            return await _database.StudentProjects.ToListAsync();
        }

        // GET: api/Requests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentProject>> GetStudentProject(int id)
        {
            var shRequest = await _database.StudentProjects.FindAsync(id);

            if (shRequest == null)
            {
                return NotFound();
            }

            return shRequest;
        }

        [HttpPost]
        public IActionResult Post([FromBody] StudentProject sp)
        {
            string responseMessage = "";

            try
            {
                _database.Create(sp);
            }
            catch(Exception e)
            {
                responseMessage = e.Message;
            }

            return Ok(new string[] {"Error", responseMessage });
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] StudentProject sp)
        {
            _database.Update(sp);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _database.Delete(id);
            return NoContent();
        }
    }
}