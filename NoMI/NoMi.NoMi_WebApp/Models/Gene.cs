using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nomi.Nomi_WebApp.Models
{
    public class Gene
    {
        [Key]
        public int Id { get; set; }
        public string EnsembleGeneId { get; set; }
        [Index]
        [Required]
        [MaxLength(255)]
        public string DisplayName { get; set; }
        public string Symbol { get; set; }
        public string EntrezId { get; set; }
        public string Location { get; set; }
        public string AliasSymbol { get; set; }
        public string AliasName { get; set; }
        public string RefSeqAccession{ get; set; }

    }
}