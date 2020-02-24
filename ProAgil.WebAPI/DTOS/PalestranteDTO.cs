
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.DTOS
{
    public class PalestranteDTO
    {
        const string _obrigatorio = "O campo {0} é obrigatório.";

        public int Id { get; set; }
        [Required (ErrorMessage = _obrigatorio)]
        [StringLength (100, MinimumLength = 3, ErrorMessage = "O campo {0} deve ter no mínimo 3 e no máximo 100 caracteres.")]
        public string Nome { get; set; }
        public string MiniCurriculo { get; set; }
        public string ImagemURL  { get; set; }
        [Required (ErrorMessage = _obrigatorio)]
        [EmailAddress (ErrorMessage = "Campo {0} com o formato de e-mail inválido.")]
        public string Email { get; set; }
        public string Telefone { get; set; }
        public List<RedeSocialDTO> RedesSociais { get; set; }
        public List<EventoDTO> Eventos { get; set; }
    }
}