using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.DTOS
{
    public class LoteDTO
    {
        const string _obrigatorio = "O campo {0} é obrigatório.";

        public int Id { get; set; }
        [Required (ErrorMessage = _obrigatorio)]
        public string Nome { get; set; }
        [Required (ErrorMessage = _obrigatorio)]
        public decimal Preco { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        [Required (ErrorMessage = _obrigatorio)]
        public int Quantidade { get; set; }
    }
}