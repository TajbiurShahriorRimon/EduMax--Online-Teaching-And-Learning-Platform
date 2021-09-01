using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMax.Repository;
using EduMax.Models;
using System.IO;

namespace EduMax.Controllers
{
    public class LectureController : Controller
    {
        // GET: Lecture
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            /*If no lecture is assigned when creating a course, the following line will be executed*/
            if (Session["addLectureForCreatingCourse"] != null)
            {
                List<Lecture> lectures = (List<Lecture>)Session["addLectureForCreatingCourse"];
                ViewData["sessionLectureList"] = Session["addLectureForCreatingCourse"];
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(Lecture lecture)
        {
            /*In the form there are two buttons named "addOneLectureBtn" and "createCourseWithLecturesBtn". If "addOneLectureBtn is clicked"
              the the follwong line will be executed.*/            
            if (Request.Form["addOneLectureBtn"] != null)
            {
                lecture.Status = "1";

                /*After first lecture for a course is added following line will be executed*/
                if (Session["addLectureForCreatingCourse"] == null) //This means no lecture is added yet for the particular course
                {
                    List<Lecture> lectures = new List<Lecture>();
                    lectures.Add(lecture);

                    //Now the firt lecture is assigned for the particular course, which is stored in a session variable
                    Session["addLectureForCreatingCourse"] = lectures;
                }

                //This means atleast one lecture is added for the particular course
                else
                {
                    List<Lecture> lectures = (List<Lecture>)Session["addLectureForCreatingCourse"];
                    lectures.Add(lecture);

                    //Now the more than one lecture is assigned for the particular course, which is stored in a session variable
                    Session["addLectureForCreatingCourse"] = lectures;
                }

                //So aftter creating a lecture for the course when button "addOneLectureBtn" is clicked, again a new form for creating
                //lecture page will be redirected, meaning more leactures will be assigned to the course.
                return RedirectToAction("Create");
            }

            /*If button "createCourseWithLecturesBtn" is clicked, this means the course creation is completed by assigning lectures to it.
             Therefore it will redirect for inserting the course into database*/
            return RedirectToAction("Insert", "Course");
        }

        public ActionResult InsertLectures()
        {
            //After inserting the course into the database, it is time to store lectures into the database.

            List<Lecture> courseLectures = (List<Lecture>)Session["addLectureForCreatingCourse"];

            //Following line is a loop. Since multiple lectures are assigned for the course, therefore a loop is required to insert
            //lectures one by one
            int i;
            for (i = 0; i < courseLectures.Count; i++)
            {
                courseLectures[i].Date = DateTime.Now;
                courseLectures[i].Status = "1";

                string filePath = Server.MapPath("~/Files/Lectures/");
                //string fileName = Path.GetFileName(concatFileName);
                string fileName = Path.GetFileName(courseLectures[i].File.FileName);

                //string fullFilePath = Path.Combine(filePath, concatFileName);
                string fullFilePath = Path.Combine(filePath, fileName);

                //Following line will save the file to the given path in the server (local machine)
                courseLectures[i].File.SaveAs(fullFilePath);

                //The path will be saved in the database so that it can retrieve the file in the view when needed.
                courseLectures[i].FileLocation = "~/Files/Lectures/" + courseLectures[i].File.FileName;

                //Following line is used to get the courseId which is just inserted in the database. So after getting the courseId
                //it is assigned the the courseLecutures[indexNum] courseId property, which is used as foreign key.
                courseLectures[i].CourseId = new CourseRepository().FindCourseIdForLectureInsert();

                //And now the lectures are inserted one by one. 
                new LectureRepository().Insert(courseLectures[i]);
            }

            //After inserting the lectures, the following session variable in not required. Therefore it is set to null.
            Session["addLectureForCreatingCourse"] = null;

            return RedirectToAction("Index", "User");
        }
    }
}