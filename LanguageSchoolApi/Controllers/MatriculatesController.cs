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
using LanguageSchoolApi.Validators;

namespace LanguageSchoolApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatriculatesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly MatriculateValidation _matriculateValidation;

        public MatriculatesController(AppDbContext context, MatriculateValidation matriculateValidation )
        {
            _context = context;
            _matriculateValidation = matriculateValidation;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Matriculate>>> GetMatriculates()
        {
            return await _context.Matriculates.ToListAsync();
        }

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
        [HttpPost]
        public async Task<ActionResult<Matriculate>> CreateMatriculate(Matriculate matriculate)
        {
            if (!_matriculateValidation.CourseExists(matriculate.NumberClass))
            {
                return BadRequest("O curso da matricula não existe.");
            }else if(!_matriculateValidation.StudentExists(matriculate.CpfStudent))
            {
                return BadRequest("O Aluno da matricula não existe.");
            }else if(_matriculateValidation.StudentAlreadyEnrolled(matriculate.CpfStudent, matriculate.NumberClass))
            {
                return BadRequest("O Aluno ja está matriculado nessa turma.");
            }else if (_matriculateValidation.MaximumLimitOfStudents(matriculate.NumberClass))
            {
                return BadRequest("Número maximo de alunos nessa turma atingido. Selecione outra turma e tente novamente.");
            }


            _context.Matriculates.Add(matriculate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreatedMatriculate", new { id = matriculate.Id }, matriculate);
        }

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
    }
}
