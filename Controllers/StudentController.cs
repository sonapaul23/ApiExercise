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
        private static List<Courses> _courses = new List<Courses>();
        private static List<Students> _students = new List<Students>();


        // GET: api/courses
        [HttpGet("api/courses")]
        public IActionResult Getc()
        {
            return Ok(_courses/*.Select(x => x.CourseName)*/);
        }




        // GET: api/courses
        [HttpGet("api/students")]
        public IActionResult Gets()
        {
            return Ok(_students);
        }



        // GET: api/courses
        [HttpGet("api/students/namelist")]
        public IActionResult Getstudentname()
        {
            return Ok(_students.Select(x=>x.FirstName));
        }



        // GET: api/courses/Name
        [HttpGet("api/courses/{Name}")]
        public IActionResult Get(string Name)
        {
            var course = _courses.Where(x => x.CourseName == Name);
            if (course == null)
                return NotFound();

            return Ok(course);
        }



        // GET: api/coursesandcount
        [HttpGet("api/courses/count")]
        public IActionResult GetCount()
        {

            var coursedetail =_courses.GroupJoin(_students, x => x.CourseName, y => y.CourseName, (x, y) => new { courseName = x.CourseName, students = y.Count() });
            return Ok(coursedetail);

        




        }


        // POST: api/courses
        [HttpPost("api/courses")]
        public IActionResult Post(Courses c)
        {
            var CourseToBeAdded = new Courses
            {
                CourseName = c.CourseName,
                DurationInMonths = c.DurationInMonths,
                CourseHead = c.CourseHead,
            };
            _courses.Add(CourseToBeAdded);
            return Ok();
        }




        // POST: api/student
        [HttpPost("api/students")]
        public IActionResult Post(Students s)
        {
            bool flag = false;
            var qry = _students.OrderBy(x => x.Id).LastOrDefault();

            int id = qry == null ? 1 : qry.Id + 1;
            foreach (var course in _courses)
            {
                if (s.CourseName == course.CourseName)
                {
                    flag = true;
                }

            }
            if (flag == false)
            {
                return Conflict("Course is not in the list");
            }
            if (Convert.ToDateTime(s.DateOfBirth) > DateTime.Now)
            {
                return Conflict("enter a valid DateOfBirth");
            }
            if (Convert.ToDateTime(s.EnrolmentDate) > DateTime.Now)
            {
                return Conflict("enter a valid EnrolmentDate");
            }

            var studentToBeAdded = new Students
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
        public IActionResult Put(int id, Students s)
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
        public IActionResult Put(string Name, Courses s)
        {
            var edit = _courses.SingleOrDefault(x => x.CourseName == Name);
            if (edit == null)
            
                return NotFound();
            

            edit.CourseHead = s.CourseHead;
            edit.DurationInMonths = s.DurationInMonths;
            return Ok();

        }




        // DELETE: api/courses/Name
        [HttpDelete("api/courses/{Name}")]
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
