using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduMax.Models
{
    //Specifically defined the table name. Therefore the class name will not be the table name. So the table name is "ReviewsAndRatings"
    [Table("ReviewsAndRatings")]
    public class ReviewAndRating
    {
        public int ReviewAndRatingId { get; set; }
        public float Rating { get; set; }
        public string Review { get; set; }

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}