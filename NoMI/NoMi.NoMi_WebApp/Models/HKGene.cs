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
        [Key]
        public int Id { get; set; }

        [ForeignKey("GeneId")]
        [Required]
        public Gene Gene { get; set; }
        [Required]
        public int GeneId { get; set; }

    }
}