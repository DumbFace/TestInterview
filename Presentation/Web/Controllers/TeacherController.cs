using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Students;
using CMS.Core.StudentTeachers;
using CMS.Core.Teachers;
using CMS.Services.StudentServices;
using CMS.Services.StudentTeacherServices;
using CMS.Services.TeacherServices;
using Factory;
using Library.CMS.Core.Students;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.CustomAttribute;

namespace Web.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentTeacherService _studentTeacherService;
        private readonly IContentFactory _contentFactory;

        public TeacherController(ILogger<StudentController> logger, IStudentService studentService, ITeacherService teacherService, IStudentTeacherService studentTeacherService, IContentFactory contentFactory)
        {
            _logger = logger;
            _studentService = studentService;
            _teacherService = teacherService;
            _studentTeacherService = studentTeacherService;
            _contentFactory = contentFactory;
        }


        public IActionResult Index()
        {
            return View();
        }

        [CheckCountTeacher]
        public IActionResult Create(List<TeacherViewModel> lstViewModel)
        {

            foreach (TeacherViewModel teacherViewModel in lstViewModel)
            {
                List<Student> students = new List<Student>();
                Teacher teacher = new Teacher
                {
                    FullName = teacherViewModel.FullName
                };
                _teacherService.Insert(teacher);
                _teacherService.Save();

                //Nếu có Student thì lưu vào bảng trung gian.
                if (teacherViewModel.IdStudents.Count > 0)
                {
                    foreach (int id in teacherViewModel.IdStudents)
                    {
                        Student student = _studentService.GetById(id);
                        students.Add(student);
                    }

                    foreach (Student student in students)
                    {
                        StudentTeacher studentTeacher = new StudentTeacher
                        {
                            StudentId = student.StudentId,
                            TeacherId = teacher.TeacherId
                        };

                        _studentTeacherService.Insert(studentTeacher);
                        _studentTeacherService.Save();
                    }
                }
            }

            return RedirectToAction("Index");
        }

        public IActionResult GetModal()
        {
            IEnumerable<StudentInputTags> studentInputTags = _contentFactory.GetStudentInputTags();
            return PartialView(studentInputTags);
        }
        
        [HttpGet]
        public JsonResult GetAll()
        {
            return Json(new { data = _teacherService.GetTable.ToList() });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}