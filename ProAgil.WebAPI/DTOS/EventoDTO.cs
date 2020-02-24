using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.WebAPI.DTOS
{
    public class EventoDTO
    {
        const string _obrigatorio = "O campo {0} é obrigatório.";

        public int Id { get; set; }
        [Required (ErrorMessage = _obrigatorio)]
        [StringLength (100, MinimumLength = 3, ErrorMessage="O campo {0} deve ter entre 3 e 100 caracteres.")]
        public string Local { get; set; }
        [Required (ErrorMessage = _obrigatorio)]
        public string DataEvento { get; set; }
        [Required (ErrorMessage = _obrigatorio)]
        [StringLength (100, MinimumLength = 5, ErrorMessage="O campo {0} deve ter entre 5 e 100 caracteres.")]
        public string Tema { get; set; }
        [Required (ErrorMessage = _obrigatorio)]
        [Range (1, 120000, ErrorMessage = "Mínimo 1 e máximo 120000 participantes")]
        public int QtdPessoas { get; set; }
        public string ImagemURL { get; set; }
        [EmailAddress (ErrorMessage="Campo {0} com o formato de e-mail inválido.")]
        [Required (ErrorMessage = _obrigatorio)]
        public string Email { get; set; }
        [Required (ErrorMessage = _obrigatorio)]
        public string Telefone { get; set; }
        public List<LoteDTO> Lotes { get; set; }
        public List<RedeSocialDTO> RedesSociais { get; set; }   
        public List<PalestranteDTO> Palestrantes { get; set; } 
    }
}