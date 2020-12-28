using AutoMapper;
using StudentManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManager.Services
{
    public class StudentService
    {
        static StudentService instance = null;

        Mapper mapper = new Mapper(MvcApplication.MapperConfig);

        SchoolDatabaseEntities context = new SchoolDatabaseEntities();

        private StudentService()
        {
            
        }

        public static StudentService GetInstance()
        {
            if (instance == null)
            {
                instance = new StudentService();
            }
            return instance;
        }

        public IEnumerable<Student> Filter(string searchName, string sortOrder)
        {
            var searchList = from m in context.Students
                             select m;

            if (!String.IsNullOrEmpty(searchName))
            {
                searchList = searchList.Where(s => s.FullName.Contains(searchName));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    searchList = searchList.OrderByDescending(s => s.FullName);
                    break;
                case "date":
                    searchList = searchList.OrderBy(s => s.FullName);
                    break;
                case "date_desc":
                    searchList = searchList.OrderByDescending(s => s.DateOfBirth);
                    break;

                default:
                    searchList = searchList.OrderBy(s => s.FullName);
                    break;
            }

            return searchList.ToList();
        }

        public bool UpdateStudent(StudentViewModel viewModel)
        {
            Student edittedStudent = context.Students.Where(s => s.StudentId == viewModel.StudentId).FirstOrDefault();
            if (edittedStudent != null)
            {
                mapper.Map<StudentViewModel, Student>(viewModel, edittedStudent);

                context.SaveChanges();

                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateStudent(StudentViewModel viewModel)
        {
            Student student = mapper.Map<Student>(viewModel);
            context.Students.Add(student);
            context.SaveChanges();
        }

        public Student GetById(int? id)
        {
            return context.Students.Where(s => s.StudentId == id).FirstOrDefault();
        }

        public void DeleteById(int? id)
        {
            Student student = context.Students.Where(s => s.StudentId == id).FirstOrDefault();
            context.Students.Remove(student);
            context.SaveChanges();
        }
    }
}