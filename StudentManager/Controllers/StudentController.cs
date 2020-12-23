using StudentManager.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace StudentManager.Controllers
{
    public class StudentController : Controller
    {

        SchoolDatabaseEntities context = new SchoolDatabaseEntities();

        // GET: Student
        public ActionResult Index(string SearchName, string SortOrder)
        {
            var searchList = from m in context.Students
                         select m;

            if (!String.IsNullOrEmpty(SearchName))
            {
                searchList = searchList.Where(s => s.FullName.Contains(SearchName));
            }

            switch (SortOrder)
            {
                case "name_desc":
                    searchList = searchList.OrderByDescending(s => s.FullName);
                    break;
                case "date_desc":
                    searchList = searchList.OrderByDescending(s => s.DateOfBirth);
                    break;
                default:
                    break;
            }

            return View(searchList.ToList());
        }

        public ActionResult Edit(int? id)
        {
            Student student = context.Students.Where(s => s.StudentId == id).FirstOrDefault();

            return View(student);

        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "StudentId, FullName, DateOfBirth, PhoneNumber, Mathematics, Literatures, English")] Student student)
        {
            Student edittedStudent = context.Students.Where(s => s.StudentId == student.StudentId).FirstOrDefault();

            if (ModelState.IsValid)
            {
                edittedStudent.FullName = student.FullName;
                edittedStudent.DateOfBirth = student.DateOfBirth;
                edittedStudent.PhoneNumber = student.PhoneNumber;
                edittedStudent.Mathematics = student.Mathematics;
                edittedStudent.Literatures = student.Literatures;
                edittedStudent.English = student.English;

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }

        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "FullName, DateOfBirth, PhoneNumber, Mathematics, Literatures, English")] Student student)
        {
            if (ModelState.IsValid)
            {
                context.Students.Add(student);

                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }

        }

        public ActionResult Delete(int? id)
        {
            Student deletedStudent = context.Students.Where(s => s.StudentId == id).FirstOrDefault();
            return View(deletedStudent);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {
            Student deletedStudent = context.Students.Where(s => s.StudentId == id).FirstOrDefault();
            context.Students.Remove(deletedStudent);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            Student student = context.Students.Where(s => s.StudentId == id).FirstOrDefault();
            return View(student);
        }

    }
}