using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Core.Teachers;
using CMS.Data.EFCore;

namespace CMS.Services.TeacherServices
{
    public interface ITeacherService : IRepository<Teacher>
    {

    }
}