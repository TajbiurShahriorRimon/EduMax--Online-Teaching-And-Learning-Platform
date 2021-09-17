using EduMax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduMax.Models.ViewModel;

namespace EduMax.Repository
{
    public class CategoryRepository : Repository<Category>
    {
        public List<GraphViewModel> NumberOfCourseInCategory()
        {
            string sqlQuery = @"select CategoryName as X_Axis, COUNT(Courses.CategoryId) as Y_Axis
                                from Courses, Categories
                                where Courses.CategoryId = Categories.CategoryId
                                and Categories.CategoryId in 
                                (select CategoryId from Courses group by CategoryId)
                                group by Categories.CategoryName";

            List<GraphViewModel> list = context.Database.SqlQuery<GraphViewModel>(sqlQuery).ToList();

            return list;
        }
    }
}