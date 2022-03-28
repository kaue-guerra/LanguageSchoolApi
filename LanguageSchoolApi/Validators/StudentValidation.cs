using LanguageSchoolApi.Data;
using LanguageSchoolApi.Models;

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

    }
}
