using LearnFlow.Models;

public interface IEnrollmentRepo
{
    Task<Enrollment?> GetEnrollmentByStudentAndCourseAsync(int studentId, int courseId);
    Task EnrollStudentAsync(Enrollment enrollment);
}


