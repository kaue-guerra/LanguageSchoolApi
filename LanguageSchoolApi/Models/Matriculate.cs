using System.ComponentModel.DataAnnotations;

namespace LanguageSchoolApi.Models
{
    public class Matriculate
    {
        public int Id { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        [StringLength(maximumLength: 11, ErrorMessage = "CPF inválido", MinimumLength = 11)]
        public string CpfStudent { get; set; }

        [Display(Name = "Número da turma")]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        public string NumberClass { get; set; }
    }
}
