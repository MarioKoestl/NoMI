using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Nomi.Nomi_WebApp.Models;

namespace Nomi.Nomi_WebApp.Manager
{
    public class HKGeneFileManager : FileManager
    {
        public async Task ReadAndWriteAllHKGenesToDataBase(string filePath)
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
                        var hkGene = new HKGene()
                        {
                            Gene = gene,
                            EnsembleGeneId = gene.EnsembleGeneId
                        };
                        DbContext.HKGenes.AddOrUpdate(h=>h.EnsembleGeneId, hkGene);
                    }
                }
            }
            await DbContext.SaveChangesAsync();
        }
    }
}