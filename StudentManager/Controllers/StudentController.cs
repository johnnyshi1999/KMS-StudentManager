using AutoMapper;
using NLog;
using StudentManager.Models;
using StudentManager.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace StudentManager.Controllers
{
    public class StudentController : Controller
    {
        private static Logger logger = NLog.LogManager.GetCurrentClassLogger();
        Mapper mapper = new Mapper(MvcApplication.MapperConfig);

        SchoolDatabaseEntities context = new SchoolDatabaseEntities();

        // GET: Student
        public ActionResult Index(string searchName, string sortOrder)
        {
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParam = sortOrder == "date" ? "date_desc" : "date";

            StudentService service = StudentService.GetInstance();
            var studentList = service.Filter(searchName, sortOrder);
            var studentViewModelList = new List<StudentViewModel>();

            foreach (var s in studentList)
            {
                studentViewModelList.Add(mapper.Map<StudentViewModel>(s));
            };
            return View(studentViewModelList);
        }

        public ActionResult Edit(int? id)
        {
            try
            {
                Student student = StudentService.GetInstance().GetById(id);
                return View(mapper.Map<StudentViewModel>(student));
            }
            catch (StudentNotFoundException e)
            {
                logger.Error(e);
                return View("Error");
            }




        }

        [HttpPost]
        public ActionResult Edit([Bind(Include = "StudentId, FullName, DateOfBirth, Address, PhoneNumber, Mathematics, Literatures, English")] StudentViewModel student)
        {
            if (ModelState.IsValid)
            {
                StudentService.GetInstance().UpdateStudent(student);

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
        public ActionResult Create([Bind(Include = "FullName, DateOfBirth, Address, PhoneNumber, Mathematics, Literatures, English")] StudentViewModel student)
        {
            //if (ModelState.IsValid)
            //{
            //    context.Students.Add(student);

            //    context.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //else
            //{
            //    return View(student);
            //}

            if (ModelState.IsValid)
            {
                StudentService.GetInstance().CreateStudent(student);
                return RedirectToAction("Index");
            }
            else
            {
                return View(student);
            }

        }

        public ActionResult Delete(int? id)
        {
            try
            {
                Student deletedStudent = StudentService.GetInstance().GetById(id);
                return View(mapper.Map<StudentViewModel>(deletedStudent));
            }
            catch (StudentNotFoundException e)
            {
                logger.Error(e);
                return View("Error");
            }


        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirm(int? id)
        {
            StudentService.GetInstance().DeleteById(id);
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            try
            {
                Student student = StudentService.GetInstance().GetById(id);
                return View(mapper.Map<StudentViewModel>(student));
            } catch(StudentNotFoundException e)
            {
                logger.Error(e);
                return View("Error");
            }


        }

    }
}