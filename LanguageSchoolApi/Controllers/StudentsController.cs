#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanguageSchoolApi.Data;
using LanguageSchoolApi.Models;
using LanguageSchoolApi.Validators;

namespace LanguageSchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly StudentValidation _studentValidation;
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context, StudentValidation studentValidation)
        {
            _studentValidation = studentValidation;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);

            if (student == null)
            {
                return NotFound("Aluno não encontrado.");
            }

            return student;
        }
       
        [HttpPut("{id}")]
        public async Task<IActionResult> EditStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }


            _context.Entry(student).State = EntityState.Modified;
            _context.Entry(student).Property(c => c.CoursesMatriculates).IsModified = false;

            try
            {
                if (_studentValidation.StudentExists(student.Cpf))
                {
                    return BadRequest("CPF ja cadastrado para outro aluno.");
                }
                    await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_studentValidation.StudentExists(student.Cpf))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
       
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {

            if (_studentValidation.StudentExists(student.Cpf))
            {
                return BadRequest("CPF ja cadastrado para outro aluno.");
            }
            else if (!_studentValidation.CourseExists(student.CoursesMatriculates[0].NumberClass))
            {
                return BadRequest("Turma da matricula não existe, por favor digitar o numero da turma correto.");
            }


            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            
            return CreatedAtAction("CreateStudent", new { id = student.Id }, student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
