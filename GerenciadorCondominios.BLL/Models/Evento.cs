using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominios.BLL.Models
{
    public class Evento
    {

        public int EventoId { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [StringLength(50, ErrorMessage = "O nome do veículo deve ter no máximo 20 caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        public DateTime Data { get; set; }
        public string UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
