using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduMax.Models;

namespace EduMax.Repository
{
    public class LectureRepository : Repository<Lecture>
    {
        public List<Lecture> UserLearningCourseLectures(int courseId)
        {
            string sqlQuery = $@"select * from Lectures where CourseId  = {courseId}";

            List<Lecture> lectures = context.Lectures.SqlQuery(sqlQuery).ToList();            
            return lectures;
        }
    }
}