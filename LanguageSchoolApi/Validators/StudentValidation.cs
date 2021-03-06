using LanguageSchoolApi.Data;

namespace LanguageSchoolApi.Validators
{
    public class StudentValidation
    {
        private readonly AppDbContext _context;

        public StudentValidation(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public bool StudentExists(string cpf)
        {
            return _context.Students.Any(e => e.Cpf == cpf);
        }


        public bool CourseExists(string numberClass)
        {
            return _context.Courses.Any(e => e.NumberClass == numberClass);
        }

        public bool StudentAlreadyEnrolled(string cpfStudent, string numberClass)
        {
            return _context.Matriculates.Any(e => e.CpfStudent == cpfStudent && e.NumberClass == numberClass);
        }
        public bool MaximumLimitOfStudents(string numberclass)
        {
            var coursesCount = _context.Matriculates.LongCount(e => e.NumberClass == numberclass);
            bool maximumLimitReached = false;

            if (coursesCount >= 5)
            {
                maximumLimitReached = true;
            }
            return maximumLimitReached;

        }
    }
}
