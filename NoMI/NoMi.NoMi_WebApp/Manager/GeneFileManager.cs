using Nomi.Nomi_WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Nomi.Nomi_WebApp.Manager
{
    public class GeneFileManager : FileManager
    {
        public async Task ReadAndWriteAllGenesToDataBase(string filePath)
        {
            base.Initialize(filePath);
            using (var reader = new StreamReader(FilePath))
            {

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (line != null)
                    {
                        var values = line.Split('\t');
                        var isValid = (!string.IsNullOrEmpty(values[19]) && !string.IsNullOrEmpty(values[2]));

                        var isProteinCoding = (!string.IsNullOrEmpty(values[3]) &&
                                               values[3].Equals("protein-coding gene")) &&
                                              (!string.IsNullOrEmpty(values[5]) &&
                                               values[5].Equals("Approved"));
                        if (!isProteinCoding || !isValid)
                            continue;

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
                        DbContext.Genes.AddOrUpdate(g => g.EnsembleGeneId, gene);

                    }
                }
            }
            await DbContext.SaveChangesAsync();
            //}
            //catch (DbEntityValidationException dbEx)
            //{
            //    foreach (var validationErrors in dbEx.EntityValidationErrors)
            //    {
            //        foreach (var validationError in validationErrors.ValidationErrors)
            //        {
            //            Trace.TraceInformation("Property: {0} Error: {1}",
            //                validationError.PropertyName,
            //                validationError.ErrorMessage);
            //        }
            //    }
            //}
        }
    }
}