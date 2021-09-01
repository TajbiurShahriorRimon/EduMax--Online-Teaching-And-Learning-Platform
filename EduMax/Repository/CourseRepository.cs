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
    }
}