using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CMS.Core.Students;
using CMS.Core.Teachers;
using CMS.Data.EFCore;
using Library.CMS.Core.Students;
using LinqKit;

namespace Factory
{
    public class ContentFactory : IContentFactory
    {
        private readonly IRepository<Teacher> _repoTeacher;

        Expression<Func<Teacher, TeacherViewModel>> projectionTeacher = s => new TeacherViewModel()
        {
            FullName = s.FullName,
            StudentTeachers = s.StudentTeachers
        };

        public IEnumerable<TeacherViewModel> Filter(string name = "")
        {
            var predicate = PredicateBuilder.New<Teacher>(true);
            if (!string.IsNullOrEmpty(name))
            {
                predicate = predicate.And(s => s.Students.All(s => s.FullName.Contains(name)));
            }

            return _repoTeacher.GetAllFilter(predicate, projectionTeacher);
            // return _repoTeacher.GetAll().Where(s=>s.Students.Any(s=>s.FullName.Contains(name))).Select(s=> new TeacherViewModel{
            //     FullName = s.FullName,
            //     StudentTeachers = s.StudentTeachers
            // });
        }

        public IEnumerable<TeacherViewModel> GetAllTeacher()
        {
            return _repoTeacher.GetAllFilter(null, projectionTeacher);
        }
    }
}