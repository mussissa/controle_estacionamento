namespace estacionamento.Models.model1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class controle_estacionamento
    {
        [Key]
        [StringLength(8)]
        public string placa { get; set; }

        [Required]
        [StringLength(19)]
        public string horario_chegada { get; set; }

        [StringLength(19)]
        public string horario_saida { get; set; }

        [StringLength(25)]
        public string duracao { get; set; }

        public int tempo { get; set; }

        public decimal preco { get; set; }

        public decimal? valor_pagamento { get; set; }

        public bool liberado { get; set; }

    }
}
