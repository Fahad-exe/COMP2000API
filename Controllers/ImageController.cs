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
        public async Task<IActionResult> Post(int id, IFormFile file)
        {
            var responseMessage = "ID coming in is "+id.ToString();

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
                        try
                        {
                            responseMessage += "; Inside for ProjectID " + id.ToString();

                            responseMessage += _database.SaveStudentPhoto(id, memoryStream.ToArray());
                           
                            return Ok(new string[] { "output: ", responseMessage });
                            //return StatusCode(201);
                        }
                        catch (Exception e)
                        {
                            responseMessage = e.ToString();
                            return Ok(new string[] { "Error", responseMessage });
                        }
                    }
                }
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }
    }
}