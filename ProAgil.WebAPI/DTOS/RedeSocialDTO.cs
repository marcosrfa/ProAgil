using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.DTOS
{
    public class RedeSocialDTO
    {
        const string _obrigatorio = "O campo {0} é obrigatório.";
        public int Id { get; set; }
        [Required (ErrorMessage = _obrigatorio)]
        public string Nome { get; set; }
        [Required (ErrorMessage = _obrigatorio)]
        public string URL { get; set; }
    }
}