using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVCapp.Models;

namespace MVCapp.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            Student s1 = new Student(1,"vibin",20,90);
            Student s2 = new Student(2, "gowtham", 15, 70);
            Student s3 = new Student(3, "raj", 18, 75);
            ICollection<Student> students = new List<Student>();
            students.Add(s1);
            students.Add(s2);
            students.Add(s3);
            return View(students);
        }
    }
}