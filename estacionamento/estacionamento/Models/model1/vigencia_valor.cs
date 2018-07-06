namespace estacionamento.Models.model1
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class vigencia_valor
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(10)]
        public string dt_inicio { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(10)]
        public string dt_fim { get; set; }

        public decimal preco { get; set; }

        public decimal preco_adcional { get; set; }

        public int? dt_inicio_int { get; set; }

        public int? dt_fim_int { get; set; }
    }
}
