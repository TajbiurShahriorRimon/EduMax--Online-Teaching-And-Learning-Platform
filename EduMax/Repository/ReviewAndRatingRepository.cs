using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EduMax.Repository;
using EduMax.Models;

namespace EduMax.Repository
{
    public class ReviewAndRatingRepository : Repository<ReviewAndRating>
    {
        public ReviewAndRating GetReview(int userId, int courseId)
        {
            return GetAll().Where(x => x.UserId == userId).Where(x => x.CourseId == courseId)
                            .Where(x => x.Review != null).FirstOrDefault();
        }

        public void InsertReviewsAndRatings(ReviewAndRating reviewAndRating)
        {
            this.Insert(reviewAndRating);
        }

        public void UpdateReviewsAndRatings(ReviewAndRating reviewAndRating)
        {
            this.Update(reviewAndRating);
        }
    }
}