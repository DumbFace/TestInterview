using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMS.Data.EFCore;
using Library.CMS.Core.Students;

namespace CMS.Services.StudentServices
{
    public interface IStudentService : IRepository<Student>
    {
        
    }
}