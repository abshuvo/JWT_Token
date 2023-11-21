using DemoApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace DemoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }


        /*   private string GenerateCustomId()
           {
               // Get the current year and month
               var now = DateTime.Now;
               var year = now.Year % 100; // Last two digits of the year
               var month = now.Month;

               // Determine the highest custom ID for the current month
               var maxCustomIdForMonth = _context.Students
                   .Where(s => s.StudentId.StartsWith($"{year:D2}{month:D2}"))
                   .Select(s => s.CustomId)
                   .Max();

               // Increment the bill part
               int bill;
               if (maxCustomIdForMonth != null && int.TryParse(maxCustomIdForMonth.Substring(4), out bill))
               {
                   bill++; // Increment the bill part
               }
               else
               {
                   bill = 0; // Start from 0 for the new month
               }

               return $"{year:D2}{month:D2}{bill:D5}";
           }*/


        //POST
        /*  [HttpPost]
          public IActionResult CreateStudent(Student student)
          {
              int currentYear = DateTime.Now.Year % 100;
              int currentMonth = 11;// DateTime.Now.Month;


              string nextStudentId = $"{currentYear:D2}{currentMonth:D2}0000";


              int maxStudentId = _context.Students
                  .Where(s => s.StudentId >= int.Parse($"{currentYear:D2}010000")
                           && s.StudentId <= int.Parse($"{currentYear:D2}129999"))
                  .Select(s => (int?)s.StudentId)
                  .DefaultIfEmpty()
                  .Max() ?? 0;


              if (nextStudentId.Equals(maxStudentId.ToString("D8")))
              {
                  int last4Digits = int.Parse(nextStudentId) % 10000;
                  last4Digits++;
                  nextStudentId = $"{currentYear:D2}{currentMonth:D2}{last4Digits:D4}";
              }

              student.StudentId = int.Parse(nextStudentId);
              _context.Students.Add(student);
              _context.SaveChanges();
              return Ok(student);
          }*/



        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            int currentYear = DateTime.Now.Year % 100;
            int currentMonth = 5;//DateTime.Now.Month;

            // Set the initial nextStudentId based on the current year and month
            string nextStudentId = $"{currentYear:D2}{currentMonth:D2}0000";

            // Query the database to get the maximum StudentId for the current month
            int maxStudentId = _context.Students
                .Where(s => s.StudentId >= int.Parse($"{currentYear:D2}{currentMonth:D2}0000")
                         && s.StudentId <= int.Parse($"{currentYear:D2}{currentMonth:D2}9999"))
                .Select(s => (int?)s.StudentId)
                .DefaultIfEmpty()
                .Max() ?? int.Parse(nextStudentId);

            
            int last4Digits = maxStudentId % 10000 + 1;

            if (last4Digits > 9999)
            {
                
                last4Digits = 0;
                currentMonth++;
                if (currentMonth > 12)
                {
                    currentMonth = 1;
                    currentYear++;
                }
            }

            nextStudentId = $"{currentYear:D2}{currentMonth:D2}{last4Digits:D4}";

            student.StudentId = int.Parse(nextStudentId);
            _context.Students.Add(student);
            _context.SaveChanges();
            return Ok(student);
        }





        /* [HttpPost]
         public IActionResult CreateStudent(Student student)
         {

             int currentYear = DateTime.Now.Year % 100; 
             int currentMonth = 11; //DateTime.Now.Month;


             string nextStudentId = $"{currentYear:D2}{currentMonth:D2}0000";


             if (currentMonth != 10 && currentMonth != 11)
             {

                 currentMonth = 10;
                 nextStudentId = $"{currentYear:D2}{currentMonth:D2}0000";
             }


             int maxStudentId = _context.Students
                 .Where(s => s.StudentId >= int.Parse($"{currentYear:D2}{currentMonth:D2}0000")
                          && s.StudentId <= int.Parse($"{currentYear:D2}{currentMonth:D2}9999"))
                 .Select(s => (int?)s.StudentId) 
                 .DefaultIfEmpty()
                 .Max() ?? 0; 
             if (nextStudentId.Equals(maxStudentId.ToString("D8")))
             {
                 int last4Digits = int.Parse(nextStudentId) % 10000;
                 last4Digits++;
                 nextStudentId = $"{currentYear:D2}{currentMonth:D2}{last4Digits:D4}";
             }

             student.StudentId = int.Parse(nextStudentId);
             _context.Students.Add(student);
             _context.SaveChanges();
             return Ok(student);
         }
 */






        //GET

        [HttpGet]
        public IActionResult GetStudents()
        {
            var students = _context.Students.ToList();
            return Ok(students);
        }


        //Get by Id

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            var student = _context.Students.FirstOrDefault(s=>s.StudentId==id);
            if (student == null)
            {
                return NotFound();
            }
           
            return Ok(student);
        }

        //PUT
        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id,Student updateStudent)
        {
            var student = _context.Students.FirstOrDefault(s=>s.StudentId==id);
            if(student == null)
                return NotFound();

            student.FirstName = updateStudent.FirstName;
            student.LastName = updateStudent.LastName;
            student.age = updateStudent.age;
            student.number = updateStudent.number;

            _context.SaveChanges();
            return Ok(student);
        }

        //Delete
        [HttpDelete("{delete}")]
        public IActionResult DeleteStudent(int id)
        {
            var student = _context.Students.FirstOrDefault(x=>x.StudentId==id);
            if(student == null)
                return NotFound();

            _context.Students.Remove(student);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
