using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Nomi.Nomi_WebApp.Models
{
    public class HKGene
    {
        [ForeignKey("EnsembleGeneId")]
        [Required]
        public Gene Gene { get; set; }
        [Key]
        [Required]
        public string EnsembleGeneId { get; set; }

    }
}