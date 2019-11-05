using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace studentmanagement.Controllers
{

    [ApiController]
    public class StudentController : ControllerBase
    {
        private static List<Courses> _courses = new List<Courses>();       /*create a list with courses class attributes*/
        private static List<Students> _students = new List<Students>();    /*create a list with Students class attributes*/


        // GET: api/courses
        [HttpGet("api/courses")]                                          /*view the list _courses */
        public IActionResult Getc()
        {
            return Ok(_courses);
        }




        // GET: api/courses                                               /*view the list _students*/
        [HttpGet("api/students")]
        public IActionResult Gets()
        {
            return Ok(_students);
        }



        // GET: api/courses                                              /*to view the name of students*/
        [HttpGet("api/students/namelist")]
        public IActionResult Getstudentname()
        {
            return Ok(_students.Select(x=>x.FirstName));
        }



        // GET: api/courses/Name                                        /*view course details by specific coursename*/
        [HttpGet("api/courses/{Name}")]
        public IActionResult Get(string Name)
        {
            var course = _courses.Where(x => x.CourseName == Name);     /*check whether the course is in list*/
            if (course == null)                                         /*if course is not present return notfound*/
                return NotFound();

            return Ok(course);                                          /*else return corresponding course details*/
        }



        // GET: api/coursesandcount
        [HttpGet("api/courses/count")]                                  /*api to get the courses and count of students enrolled in the course*/        
        public IActionResult GetCount()
        {

            var coursedetail =_courses.GroupJoin(_students, x => x.CourseName, y => y.CourseName, (x, y) => new { courseName = x.CourseName, students = y.Count() });       /*join the coursename entity in _students list and _courses list and group by coursename and assign coursename and count to coursedetail*/

            return Ok(coursedetail);

        




        }


        // POST: api/courses
        [HttpPost("api/courses")]
        public IActionResult Post(Courses c)                     /*api to create new courses*/
        {
            var CourseToBeAdded = new Courses                    /*create an object to add details*/
            {
                CourseName = c.CourseName,
                DurationInMonths = c.DurationInMonths,
                CourseHead = c.CourseHead,
            };
            _courses.Add(CourseToBeAdded);                       /*add details to the _courses list*/
            return Ok();
        }




        // POST: api/student
        [HttpPost("api/students")]
        public IActionResult Post(Students s)                      /*api to create student dtails*/
        {
            
            var qry = _students.OrderBy(x => x.Id).LastOrDefault();  /*assigning last student id to qry*/

            int id = qry == null ? 1 : qry.Id + 1;                   /*if id is null assign 1 else increment it by one and assign to id*/

            if (Convert.ToDateTime(s.DateOfBirth) > DateTime.Now)    /*check whether the date is future date and return conflict*/
            {
                return Conflict("enter a valid DateOfBirth");
            }
            if (Convert.ToDateTime(s.EnrolmentDate) > DateTime.Now)
            {
                return Conflict("enter a valid EnrolmentDate");
            }

            var studentToBeAdded = new Students                       /*add student details to list _students*/
            {
                Id = id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                DateOfBirth = s.DateOfBirth,
                Address = s.Address,
                PhoneNumber = s.PhoneNumber,
                EnrolmentDate = s.EnrolmentDate,
                CourseName = s.CourseName


            };
            _students.Add(studentToBeAdded);
            return Ok(_students);
        }



        // PUT: api/students/id
        [HttpPut("api/students/{id}")]
        public IActionResult Put(int id, Students s)                     /*edit the student details by passing id*/
        {
            var edit = _students.SingleOrDefault(x => x.Id == id);
            if (edit == null)

                return NotFound();


            edit.FirstName = s.FirstName;
            edit.LastName = s.LastName;
            edit.DateOfBirth = s.DateOfBirth;
            edit.Address = s.Address;
            edit.PhoneNumber = s.PhoneNumber;
            edit.EnrolmentDate = s.EnrolmentDate;
            edit.CourseName = s.CourseName;
            return Ok();

        }




        // PUT: api/courses/Name
        [HttpPut("api/courses/{Name}")]
        public IActionResult Put(string Name, Courses s)                      /*edit the course details by passing course name*/
        {
            var edit = _courses.SingleOrDefault(x => x.CourseName == Name);
            if (edit == null)
            
                return NotFound();
            

            edit.CourseHead = s.CourseHead;
            edit.DurationInMonths = s.DurationInMonths;
            return Ok();

        }




        // DELETE: api/courses/Name
        [HttpDelete("api/courses/{Name}")]                                        /*delete course details by name*/
        public IActionResult Delete(string Name)
        {
            var delete = _courses.Where(x => x.CourseName == Name);
            foreach (var item in delete)
            {
                _courses.Remove(item);
                return Ok();

            }
                
            return NotFound();
        }
    }
}
