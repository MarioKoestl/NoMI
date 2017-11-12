using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using Nomi.Nomi_WebApp.Models;

namespace Nomi.Nomi_WebApp.Manager
{
    public class AllHKGeneFileManager : FileManager
    {
        public void ReadAndWriteAllHKGenesToDataBase(string filePath)
        {
            base.Initialize(filePath);
            using (var reader = new StreamReader(FilePath))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var gene = DbContext.Genes.FirstOrDefault(g => g.Symbol.Equals(line));
                        if(gene == null)
                            continue;
                        var hkGenes = DbContext.HKGenes;
                        if(hkGenes.fin)
                        var hkGene = new HKGene
                        {
                            Gene = gene,
                            Symbol = gene.Symbol
                        };

                        //Check if HKGene is already in DBContext
                        DbContext.HKGenes.AddOrUpdate(h=>h.Symbol, hkGene);
                    }
                }
            }
            DbContext.SaveChanges();
        }
    }
}