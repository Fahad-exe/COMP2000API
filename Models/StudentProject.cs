using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COMP2000API.Models
{
    public class StudentProject
    {
        public int ProjectID { get; set; }
        public int StudentID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string ThumbnailURL { get; set; }
        public string PosterURL { get; set; }
        public string First_Name { get; set; }
        public string Second_Name { get; set; }
        public string PictureURL { get; set; }
    }
}
