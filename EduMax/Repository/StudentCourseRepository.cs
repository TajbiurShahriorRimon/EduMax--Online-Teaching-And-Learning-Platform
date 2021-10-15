using EduMax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduMax.Repository
{
    public class StudentCourseRepository : Repository<StudentCourse>
    {
        public StudentCourse CheckIfStudentHasCourse(int id, int courseId) //first parameter is the user_id, second is course_id
        {
            //Finding the data from StudentCourse, checking if the course belongs to the logged-in user
            string query = $"select * from StudentCourses where UserStudentId = {id} and CourseId = {courseId}";
            StudentCourse studentCourse = context.StudentCourses.SqlQuery(query).FirstOrDefault();

            return studentCourse;
        }
    }
}