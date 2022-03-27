using System.ComponentModel.DataAnnotations;

namespace LanguageSchoolApi.Models
{
    public class Student
    {

        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        public string Name { get; set; }


        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        [StringLength(maximumLength: 11, ErrorMessage = "CPF inválido", MinimumLength = 11)]
        public string Cpf { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        [StringLength(maximumLength: 20, ErrorMessage = "O Campo {0} deve ter entre {2} e {1} caracteres", MinimumLength = 2)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido")]
        public string Email { get; set; }

        [Display(Name = "Turma")]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        public List<Matriculate> CoursesMatriculates { get; set; }
        
    }
}
