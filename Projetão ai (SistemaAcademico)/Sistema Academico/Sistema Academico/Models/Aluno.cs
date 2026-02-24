using System.ComponentModel.DataAnnotations;

namespace Sistema_Academico.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        [Display(Name = "RA")]
        [Required(ErrorMessage = "O RA é obrigatório")]
        [StringLength(10,MinimumLength =4, ErrorMessage = "O RA deve ter entre 4 a 10 caracteres")]
        public string? Ra {  get; set; }
        public int UsuarioID { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
