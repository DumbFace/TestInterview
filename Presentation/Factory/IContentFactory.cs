using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Students;
using CMS.Core.Teachers;

namespace Factory
{
    public interface IContentFactory
    {
        IEnumerable<TeacherOutputViewModel> GetAllTeacher();
        IEnumerable<TeacherOutputViewModel> GetAllTeacherFEOutPut();
        IEnumerable<StudentViewModel> GetAllStudentViewModel();
        IEnumerable<TeacherInputTags> GetTeacherInputTags();
        IEnumerable<StudentInputTags> GetStudentInputTags();

    }
}