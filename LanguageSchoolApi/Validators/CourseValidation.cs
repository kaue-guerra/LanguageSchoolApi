using LanguageSchoolApi.Data;

namespace LanguageSchoolApi.Validators
{
    public class CourseValidation
    {
            private readonly AppDbContext _context;

            public CourseValidation(AppDbContext appDbContext)
            {
                _context = appDbContext;
            }

            public bool CourseExists(int id)
            {
                return _context.Courses.Any(e => e.Id == id);
            }
            public bool CourseExistsNumberClass(string numberclass)
            {
            return _context.Courses.Any(e => e.NumberClass == numberclass);
            }

            public bool ExistStudentsNoCourse(string numberclass)
            {
                return _context.Matriculates.Any(e => e.NumberClass == numberclass);
            }
        
    }
}
