using Nomi.Nomi_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Nomi.Nomi_WebApp.Manager
{
    public class AllGeneFileManager : FileManager
    {
        public async Task ReadAndWriteAllGenesToDataBase(string filePath)
        {
            base.Initialize(filePath);
            using (var reader = new StreamReader(FilePath))
            {
                int count = 0;
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        
                        var values = line.Split('\t');
                        var isProteinCoding = (!string.IsNullOrEmpty(values[3]) &&
                                              values[3].Equals("protein-coding gene")) &&
                                              (!string.IsNullOrEmpty(values[5]) &&
                                              values[5].Equals("Approved"));
                        if(!isProteinCoding)
                            continue;

                        count++;
                        var gene = new Gene
                        {
                            DisplayName = values[2],
                            EntrezId = values[18],
                            RefSeqAccession = values[23],
                            EnsembleGeneId = values[19],
                            Symbol = values[1],
                            AliasSymbol = string.IsNullOrEmpty(values[8]) ? "Not defined yet" : values[8],
                            AliasName = string.IsNullOrEmpty(values[9]) ? "Not defined yet" : values[9],
                            Location = string.IsNullOrEmpty(values[6]) ? "Not defined yet" : values[6],
                        };

                        //ToDO why does it not work
                        //Check if Gene is already in DBContext
                        //DbContext.Genes.AddOrUpdate(g=>g.EnsembleGeneId,gene);
                        DbContext.Genes.Add(gene);
                    }
                }
            }
            await DbContext.SaveChangesAsync();
        }
    }
}