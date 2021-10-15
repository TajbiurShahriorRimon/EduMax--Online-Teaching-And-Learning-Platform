using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduMax.Models;
using EduMax.Repository;

namespace EduMax.Controllers
{
    public class ShoppingCartController : Controller
    {
        public ActionResult Index()
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //If shopping cart contains items then following line will be executed.
            if (Session["shoppingCart"] != null)
            {
                //Assigning the cart items to list of Course.
                List<Course> courses = (List<Course>)Session["shoppingCart"];

                double sumAmount = TotalPrice(courses);
                ViewBag.TotalPrice = sumAmount;

                //Since item exists in the cart, we will send the cart object to the view.
                return View(courses);
            }
            //The following line executes only if user did not add any course into the shopping cart
            else if(Session["shoppingCart"] == null)
            {
                //So if no product is added in the cart the shopping cart becomes null and thereore if the user wants to
                //check or see his cart, we have to make the course list to null.
                List<Course> courses = null;
                return View(courses);
            }

            //The following line executes if the shopping cart has no item.
            return View();
        }

        //This metod is used to add course to shopping cartt 
        public ActionResult AddToShoppingCart(int id)
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            /*First we have to check whether the course is already added to the shopping cart or not which is selected*/
            bool checkIfCourseExistInCart = IfCourseAdded(id);

            //If course is already added to the shopping cart, then we will redirected to the shopping cart view,
            //meaning that no action will be taken for the particular course.
            if (checkIfCourseExistInCart)
            {
                return RedirectToAction("Index");
            }
            
            List<Course> courses = new List<Course>();

            //Getting the course object with the help of id.
            Course course = new CourseRepository().Get(id);

            /*If the shopping cart does not contain any course then following line will be executed.*/
            if (Session["shoppingCart"] == null)
            {
                //Since no course is added, int generic list of course, the course will be added which is selected.
                courses.Add(course);
                Session["shoppingCart"] = courses;
            }
            /*If the shopping cart contains any course then following line will be executed.*/
            else if (Session["shoppingCart"] != null) {
                courses = (List<Course>)Session["shoppingCart"];
                courses.Add(course);

                Session["shoppingCart"] = courses;
            }

            return RedirectToAction("Index");
        }

        /*This method is to check if the course is already added to the cart. If the course added to the cart, then false will be return
        meaning that the selected course will be added to the cart.
        However, if the cart already contains the selected course to the cart the we will return true, meaning the course
        cannot be added to the cart since it already exists.*/
        public bool IfCourseAdded(int id)
        {
            //If no item is added in the shopping cart then we will return false, meaning that selected course can be added
            //to the cart since no course even exists in the cart.
            if (Session["shoppingCart"] == null)
            {
                return false;
            }

            List<Course> courses = (List<Course>)Session["shoppingCart"];

            /*Follwing loop is used to find the course id. From the generic course list, if from any index it is found that 
             the id exists, then true will be returned meaning the course cannot be added to the cart since it already exists.*/
            for (int i = 0; i < courses.Count; i++)
            {
                if(courses[i].CourseId == id)
                {
                    return true;
                }
            }

            return false;
        }

        //This method is used to remove course from the cart.
        public ActionResult RemoveFromShoppingCart(int id)
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            List<Course> courses = (List<Course>)Session["shoppingCart"];

            /*We have to check index by index using loop from the generic course list to find the id given in the method parameter.
             The id which will be found from the index, that particular index will be removed.*/
            for(int i = 0; i < courses.Count; i++)
            {
                if (courses[i].CourseId == id)
                {
                    courses.RemoveAt(i);

                    //After removing the particular index from the list, the list will then be assigned to the shopping cart session.
                    Session["shoppingCart"] = courses;
                    break;
                }
            }

            return RedirectToAction("Index");
        }

        public static double TotalPrice(List<Course> courses)
        {
            double sumAmount = 0;
            foreach (Course course in courses)
            {
                sumAmount += course.Price;
            }

            return sumAmount;
        }

        public ActionResult Checkout()
        {
            /*If no session for Login is set the user will be redirected to the log-in page*/
            if (Session["user_email"] == null)
            {
                return RedirectToAction("Login", "Home");
            }

            //Assigning the cart items to list of Course.
            List<Course> courses = (List<Course>)Session["shoppingCart"];

            double sumAmount = TotalPrice(courses);
            ViewBag.TotalPrice = sumAmount;

            return View();
        }
    }
}