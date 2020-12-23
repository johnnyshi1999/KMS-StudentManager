using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManager.Services
{
    public class StudentService
    {
        static StudentService instance = null;

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

        
    }
}