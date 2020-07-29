using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GerenciadorCondominios.BLL.Models
{
    public class Apartamento
    {
        public int ApartamentoId { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [Range(0, 10000, ErrorMessage = "Valor inválido")]
        [Display(Name = "Número")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Este campo {0} é obrigatório")]
        [Range(0, 100, ErrorMessage = "Valor inválido")]
        public int Andar { get; set; }
        public string Foto { get; set; }

        public int MoradorId { get; set; }
        public virtual Usuario Morador { get; set; }

        public int PropietarioId { get; set; }
        public virtual Usuario Proprietario { get; set; }

    }
}
