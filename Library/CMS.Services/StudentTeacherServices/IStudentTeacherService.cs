using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.StudentTeachers;
using CMS.Data.EFCore;

namespace CMS.Services.StudentTeacherServices
{
    public interface IStudentTeacherService : IRepository<StudentTeacher>
    {
        
    }
}