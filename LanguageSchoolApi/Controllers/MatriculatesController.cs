#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LanguageSchoolApi.Data;
using LanguageSchoolApi.Models;

namespace LanguageSchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculatesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MatriculatesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Matriculates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matriculate>>> GetMatriculates()
        {
            return await _context.Matriculates.ToListAsync();
        }

        // GET: api/Matriculates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Matriculate>> GetMatriculate(int id)
        {
            var matriculate = await _context.Matriculates.FindAsync(id);

            if (matriculate == null)
            {
                return NotFound();
            }

            return matriculate;
        }
        // POST: api/Matriculates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Matriculate>> CreateMatriculate(Matriculate matriculate)
        {
            if (!CourseExists(matriculate.NumberClass))
            {
                return BadRequest("O curso da matricula não existe.");
            }else if(!StudentExists(matriculate.CpfStudent))
            {
                return BadRequest("O Aluno da matricula não existe.");
            }else if(!StudentAlreadyEnrolled(matriculate.CpfStudent, matriculate.CpfStudent))
            {
                return BadRequest("O Aluno ja está matriculado nessa turma.");
            }


            _context.Matriculates.Add(matriculate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatriculate", new { id = matriculate.Id }, matriculate);
        }

        // DELETE: api/Matriculates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatriculate(int id)
        {
            var matriculate = await _context.Matriculates.FindAsync(id);
            if (matriculate == null)
            {
                return NotFound();
            }

            _context.Matriculates.Remove(matriculate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(string numberclass)
        {
            return _context.Courses.Any(e => e.NumberClass == numberclass);
        }

        private bool StudentExists(string cpf)
        {
            return _context.Students.Any(e => e.Cpf == cpf);
        }

        private bool StudentAlreadyEnrolled(string cpfStudent , string numberClass)
        {
            return _context.Matriculates.Any(e => e.CpfStudent == cpfStudent && e.NumberClass == numberClass);
        }
    }
}
