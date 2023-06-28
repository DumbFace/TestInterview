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
    public class StudentController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentTeacherService _studentTeacherService;
        private readonly IContentFactory _contentFactory;

        public StudentController(ILogger<StudentController> logger, IStudentService studentService, ITeacherService teacherService, IStudentTeacherService studentTeacherService, IContentFactory contentFactory)
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

        public IActionResult GetModal()
        {
            IEnumerable<TeacherInputTags> teacherInputTags = _contentFactory.GetTeacherInputTags();
            return PartialView(teacherInputTags);
        }

        [CheckStudentCount]
        public IActionResult Create(List<StudentViewModel> lstViewModel)
        {

            foreach (StudentViewModel studentViewModel in lstViewModel)
            {
                DateTime toDay = DateTime.Now;
                int age = toDay.Year - studentViewModel.DoB.Year;
                List<Teacher> teachers = new List<Teacher>();
                Student student = new Student
                {
                    FullName = studentViewModel.FullName,
                    DOB = studentViewModel.DoB,
                    Age = age
                };
                _studentService.Insert(student);
                _studentService.Save();

                //Nếu có Teacher thì lưu vào bảng trung gian
                if (studentViewModel.idTeachers.Count > 0)
                {
                    foreach (int id in studentViewModel.idTeachers)
                    {
                        Teacher teacher = _teacherService.GetById(id);
                        teachers.Add(teacher);
                    }

                    foreach (Teacher teacher in teachers)
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

        public IActionResult Filter(string name = "")
        {

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult LoadData()
        {
            return PartialView(_contentFactory.GetAllStudentViewModel());
        }

        [HttpGet]
        public JsonResult GetAll()
        {
            return Json(new { data = _studentService.GetTable.ToList() });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}