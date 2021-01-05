using AutoMapper;
using NLog;
using StudentManager.Logs;
using StudentManager.Logs.CustomExceptions;
using StudentManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace StudentManager.Services
{
    public class StudentService
    {
        private static StudentService instance = null;
        private static readonly object padLock = new object();

        Mapper mapper = new Mapper(MvcApplication.MapperConfig);

        SchoolDatabaseEntities context = new SchoolDatabaseEntities();

        private StudentService()
        {
            
        }

        public static StudentService GetInstance()
        {
            if (instance == null)
            {
                lock (padLock)
                {
                    if (instance == null)
                    {
                        instance = new StudentService();
                    }
                }
                
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

        public void UpdateStudent(StudentViewModel viewModel)
        {
            try
            {
                Student edittedStudent = context.Students.Where(s => s.StudentId == viewModel.StudentId).FirstOrDefault();
                if (edittedStudent == null)
                {
                    throw new StudentNotFoundException(string.Format("Cannot found student with this id: {0}", viewModel.StudentId));
                }
                mapper.Map<StudentViewModel, Student>(viewModel, edittedStudent);
                context.SaveChanges();
            } catch (Exception e)
            {
                throw;
            }
            
        }

        public void CreateStudent(StudentViewModel viewModel)
        {
            try
            {
                Student student = mapper.Map<Student>(viewModel);
                context.Students.Add(student);
                context.SaveChanges();
            } catch (Exception e)
            {
                throw;
            }
           
        }

        public Student GetById(int? id)
        {
            try
            {
                Student student = context.Students.Where(s => s.StudentId == id).FirstOrDefault();

                if (student == null)
                {
                    throw new StudentNotFoundException(string.Format("Cannot found student with this id: {0}", id));
                }
                return student;
            } catch (Exception e)
            {
                throw;
            }
            
        }

        public void DeleteById(int? id)
        {
            try
            {
                Student student = context.Students.Where(s => s.StudentId == id).FirstOrDefault();
                if (student == null)
                {
                    throw new StudentNotFoundException(string.Format("Cannot found student with this id: {0}", id));
                }
                context.Students.Remove(student);
                context.SaveChanges();
            } catch (Exception e)
            {
                throw;
            }
            
        }
    }
    
}