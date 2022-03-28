using System.ComponentModel.DataAnnotations;

namespace LanguageSchoolApi.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        public string Name { get; set; }

        [Display(Name = "Número da turma")]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        public string NumberClass { get; set; }
        
        [Display(Name = "Ano Letivo")]
        [Required(ErrorMessage = "O Campo {0} é obrigatorio")]
        public DateTime Year { get; set; }
    }
}
