using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CMS.Services.StudentServices;
using CMS.Services.StudentTeacherServices;
using CMS.Services.TeacherServices;
using Factory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class OutPutFEController : Controller
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly IStudentTeacherService _studentTeacherService;
        private readonly IContentFactory _contentFactory;

        public OutPutFEController(ILogger<StudentController> logger, IStudentService studentService, ITeacherService teacherService, IStudentTeacherService studentTeacherService, IContentFactory contentFactory)
        {
            _logger = logger;
            _studentService = studentService;
            _teacherService = teacherService;
            _studentTeacherService = studentTeacherService;
            _contentFactory = contentFactory;
        }
        public IActionResult Index()
        {
            return View(_contentFactory.GetAllTeacherFEOutPut());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}