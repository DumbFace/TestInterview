using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Students;
using CMS.Core.Teachers;
using CMS.Services.StudentServices;
using CMS.Services.TeacherServices;
using Library.CMS.Core.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    [Route("[controller]")]
    public class StudentTeacher : Controller
    {
        private readonly ILogger<StudentTeacher> _logger;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;

        public StudentTeacher(ILogger<StudentTeacher> logger, IStudentService studentService, ITeacherService teacherService)
        {
            _logger = logger;
            _studentService = studentService;
            _teacherService = teacherService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetModalAdd()
        {
            return PartialView();
        }

        public IActionResult Create(List<StudentViewModel> lstStudentViewMode)
        {
            foreach (StudentViewModel studentViewModel in lstStudentViewMode)
            {
                ICollection<Teacher> teachers = new List<Teacher>();
                List<string> ids = studentViewModel.IdTeacher.Split(",").ToList();
                foreach (string id in ids)
                {
                    var teacher = _teacherService.GetById(id);
                    teachers.Add(teacher);
                }

                Student student = new Student
                {
                    FullName = studentViewModel.FullName,
                    DOB = studentViewModel.DoB,
                    Teachers = teachers
                };
                _studentService.Save();
            }

            return RedirectToAction("Index");
        }

        public IActionResult Filter(string name = "")
        {

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}