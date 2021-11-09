using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduMax.Models;
using EduMax.Repository;

namespace EduMax.Repository
{
    public class UserFavoriteCourseRepository : Repository<UserFavoriteCourse>
    {
        public List<UserFavoriteCourse> IfCourseIsAddedInFavorite(int courseId, int userId) //Course Id
        {
            string query = "select * from UserFavoriteCourses where CourseId = " + courseId + " and UserId = " + userId;
            List<UserFavoriteCourse> userFavoriteCourses = this.context.UserFavoriteCourses.SqlQuery(query).ToList();
            return userFavoriteCourses;
        }
    }
}