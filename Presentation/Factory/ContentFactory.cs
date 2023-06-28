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
        private readonly IRepository<Student> _repoStudent;

        Expression<Func<Teacher, TeacherOutputViewModel>> projectionTeacher = s => new TeacherOutputViewModel()
        {
            FullName = s.FullName,
            Students = s.StudentTeachers.Select(q => new StudentOutputViewModel
            {
                FullName = q.Student.FullName,
                Age = q.Student.Age
            }).ToList()
        };

        Expression<Func<Student, StudentViewModel>> projectionStudent = s => new StudentViewModel()
        {
            FullName = s.FullName,
            lstTeacherFullName = s.StudentTeachers.Select(q => q.Teacher.FullName).ToList(),
            DoB = s.DOB
        };

        Expression<Func<Teacher, TeacherInputTags>> projectTeacherInputTag = s => new TeacherInputTags()
        {
            Value = s.TeacherId.ToString(),
            Name = s.FullName
        };

        Expression<Func<Student, StudentInputTags>> projectStudentInputTag = s => new StudentInputTags()
        {
            Value = s.StudentId.ToString(),
            Name = s.FullName
        };

        public ContentFactory(IRepository<Teacher> repoTeacher, IRepository<Student> repoStudent)
        {
            _repoTeacher = repoTeacher;
            _repoStudent = repoStudent;
        }

        public IEnumerable<StudentViewModel> GetAllStudentViewModel()
        {
            // string select = "select FullName, Age from Student";
            // var students = _repoStudent.GetTable.Select(s => new Student
            // {
            //     FullName = s.FullName,
            //     Age = s.Age
            // });

            return _repoStudent.GetAllFilter(null, projectionStudent);
        }

        public IEnumerable<TeacherOutputViewModel> GetAllTeacher()
        {
            return _repoTeacher.GetAllFilter(null, projectionTeacher);
        }

        public IEnumerable<TeacherOutputViewModel> GetAllTeacherFEOutPut()
        {
            IEnumerable<TeacherOutputViewModel> teachers = _repoTeacher.GetTable
                    .GroupBy(t => t.FullName)
                    .Select(g => new TeacherOutputViewModel
                    {
                        FullName = g.Key,
                        Students = g.SelectMany(t => t.StudentTeachers.Select(ts => ts.Student))
                                    .Select(s => new StudentOutputViewModel
                                    {
                                        FullName = s.FullName,
                                        Age = s.Age
                                    })
                                    .OrderByDescending(s => s.Age)
                                    .ToList()
                    });
            return teachers;
        }

        public IEnumerable<StudentInputTags> GetStudentInputTags()
        {
            return _repoStudent.GetAllFilter(null, projectStudentInputTag);
        }

        public IEnumerable<TeacherInputTags> GetTeacherInputTags()
        {
            return _repoTeacher.GetAllFilter(null, projectTeacherInputTag);
        }
    }
}