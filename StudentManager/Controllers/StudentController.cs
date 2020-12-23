using StudentManager;
using StudentManager.Services;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using StudentManager.Models;
using System.Collections.Generic;

namespace StudentManager.Controllers
{
    public class StudentController : Controller
    {

        SchoolDatabaseEntities context = new SchoolDatabaseEntities();

        // GET: Student
        public ActionResult Index(string searchName, string sortOrder)
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParam = sortOrder == "date" ? "date_desc" : "date";

            StudentService service = StudentService.GetInstance();
            var studentList = service.Filter(searchName, sortOrder);
            var studentViewModelList = new List<StudentViewModel>();

            var mapper = new Mapper(MvcApplication.MapperConfig);

            foreach (var s in studentList)
            {
                studentViewModelList.Add(mapper.Map<StudentViewModel>(s));
            };
            return View(service.Filter(searchName, sortOrder));
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