using Microsoft.AspNetCore.Mvc;
using LearnFlow.Interfaces;
using LearnFlow.Models;
using System.Security.Claims;

namespace LearnFlow.Controllers
{
    public class CourseController : Controller
    {
        private readonly IRepo<Course> courseRepo;
        private readonly IEnrollmentRepo enrollmentRepo;

        public CourseController(IRepo<Course> courseRepo, IEnrollmentRepo enrollmentRepo)
        {
            this.courseRepo = courseRepo;
            this.enrollmentRepo = enrollmentRepo;
        }

        // GET: CourseController
        public async Task<ActionResult> Index()
        {
            var courses = await courseRepo.GetAllAsync();
            return View(courses);
        }

        // GET: CourseController/Details/3

        public async Task<ActionResult> Details(int id)
        {
            var course = await courseRepo.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound(); 
            }
            return View(course);
        }

        // GET: CourseController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Course course)
        {
            if (ModelState.IsValid)
            {
                await courseRepo.CreateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: CourseController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var course = await courseRepo.GetByIdAsync(id);
            return View(course);
        }

        // POST: CourseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Course course)
        {
            if (id != course.CourseId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await courseRepo.UpdateAsync(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: CourseController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var course = await courseRepo.GetByIdAsync(id);
            return View(course);
        }

        // POST: CourseController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await courseRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

       
  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Enroll(int id) 
        {
        
            int studentId = 1;


            var existingEnrollment = await enrollmentRepo.GetEnrollmentByStudentAndCourseAsync(studentId, id);

            if (existingEnrollment == null)
            {

                var enrollment = new Enrollment
                {
                    CourseId = id,
                    StudentId = studentId,
                    EnrollmentDate = DateTime.Now,
                    Progress = 0
                };

                await enrollmentRepo.EnrollStudentAsync(enrollment);


                TempData["SuccessMessage"] = "You have successfully enrolled in the course!";
            }
            else
            {
               
                TempData["SuccessMessage"] = "You are already enrolled in this course.";
            }

            return RedirectToAction(nameof(Details), new { id });

        }
    }
}


