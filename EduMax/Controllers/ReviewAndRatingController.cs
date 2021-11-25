using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMax.Models;
using EduMax.Repository;

namespace EduMax.Controllers
{
    public class ReviewAndRatingController : Controller
    {
        [HttpPost]
        public ActionResult PostReview(string review, int courseId)
        {
            /*If no session for Login is set the user will be redirected to the landing page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (review == null)
            {

            }

            int userId = (int)Session["credential_id"];
            ReviewAndRating reviewAndRating = new ReviewAndRatingRepository().GetReview(userId, courseId);            

            if(reviewAndRating == null)
            {
                reviewAndRating = new ReviewAndRating();
                reviewAndRating.CourseId = courseId;
                reviewAndRating.UserId = userId;
                reviewAndRating.Review = review;

                new ReviewAndRatingRepository().InsertReviewsAndRatings(reviewAndRating);
            }
            reviewAndRating.Review = review;
            return RedirectToAction("CourseLectureList", "Course", new { id = courseId});
        }
    }
}