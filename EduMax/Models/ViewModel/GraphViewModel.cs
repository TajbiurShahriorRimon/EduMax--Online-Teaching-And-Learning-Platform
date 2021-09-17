using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EduMax.Models.ViewModel
{
    [NotMapped]
    public class GraphViewModel
    {
        public string X_Axis { get; set; }
        public int Y_Axis { get; set; }
    }
}