using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using COMP2000API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace COMP2000API.Controllers
{
    [Route("students/{id}/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly DataAccess _database;

        public ImageController(DataAccess database)
        {
            _database = database;
        }

        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file, int StudentID )
        {
            var extension = Path.GetExtension(file.FileName);
            var size = file.Length;
            //Only proceed if we have a jpg object and it is below a certain size
            if ((extension.ToLower().Equals(".jpg")) && (size < (5 * 1024 * 1024)))
            {
                if (file.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);

                        _database.SaveStudentPhoto(StudentID, memoryStream.ToArray());
                    }
                }
                return StatusCode(201);
            }
            else
                return BadRequest();
        }
    }
}