using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Teachers;

namespace Factory
{
    public interface IContentFactory
    {
        IEnumerable<TeacherViewModel> GetAllTeacher();
        IEnumerable<TeacherViewModel> Filter(string name = "");

    }
}