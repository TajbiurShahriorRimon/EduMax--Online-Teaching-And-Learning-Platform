using EduMax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduMax.Repository
{
    public class CourseRepository : Repository<Course>
    {
        public int FindCourseIdForLectureInsert()
        {
            Course course = context.Courses.SqlQuery("select Top 1 * from courses order by courseId desc").FirstOrDefault();
            return course.CourseId;
        }

        /*When user searches for a course the course will be retrieved by searching the course name*/
        public List<Course> SearchCourseByString(string searchCourse)
        {
            string query = $"select * from courses where status = '1' and courseName Like '%{searchCourse}%'";
            List<Course> courses = context.Courses.SqlQuery(query).ToList();
            return courses;
        }

        public List<Course> UserCreatedCourseList(int userId)
        {
            string sqlQuery = $"select * from Courses where UserId = {userId}";

            List<Course> courses = context.Courses.SqlQuery(sqlQuery).ToList();
            return courses;
        }

        public List<Course> UserLearningCourseList(int userId)
        {
            string sqlQuery = $@"select * from courses where CourseId in 
                                (select CourseId from StudentCourses where UserStudentId = {userId})";

            List<Course> courses = context.Courses.SqlQuery(sqlQuery).ToList();
            return courses;
        }

        public Course GetCourseData(int courseId)
        {
            return new CourseRepository().Get(courseId);
        }
    }
}