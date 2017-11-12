using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Nomi.Nomi_WebApp.Models
{
    public class Config
    {
        [Key]
        public int Id { get; set; }
        public string HouseKeepingGenesFilePath { get; set; }
        public string HugoGeneNameFilePath { get; set; }
        public string ComprehensiveHumanExpressionMapFilePath { get; set; }
        public string ComprehensiveHumanExpressionMapSplitPath { get; set; }

        public string AffimetrixProbeIdsFilePath { get; set; }
        public string GeneAnnotationsFilePath { get; set; }
        public string HugoGeneNameSplitPath { get; set; }


    }
}