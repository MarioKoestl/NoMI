using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using Nomi.Nomi_WebApp.Models;
using NoMi.NoMi_Lib.Manager;
using hf = Hangfire;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Nomi.Nomi_WebApp.Controllers
{
    public class GenesController : Controller
    {
        private readonly NoMiDbContext ctx = new NoMiDbContext();
        // GET: HKGenes
        public ActionResult Index()
        {
            return View(ctx.Genes.Take(100));
        }

        public ActionResult RefreshAllGenes()
        {
            var jobId = hf.BackgroundJob.Enqueue(() => FillDbWithAllGenes());
            return View("Index", ctx.Genes.Take(100));
        }

        public ActionResult HKGenes()
        {
            return View(ctx.HKGenes.Include(g => g.Gene));
        }

        public ActionResult RefreshHKGenes()
        {
            var jobId = hf.BackgroundJob.Enqueue(()=> FillDbWithHKGenes());

            return View("HKGenes", ctx.HKGenes.Include(g => g.Gene));
        }

        [DisplayName("Get All HKGenes")]
        public void FillDbWithHKGenes()
        {
            ctx.Database.ExecuteSqlCommand("delete from HKGenes");
            var config = ctx.Configurations.FirstOrDefault();
            using (var reader = new StreamReader(config.HouseKeepingGenesFilePath))
            {
                while (!reader.EndOfStream)
                {
                    var symbol = reader.ReadLine();
                    var hkgene = new HKGene()
                    {
                        Gene = ctx.Genes.First(g => g.Symbol.ToLower().Equals(symbol.ToLower()))
                    };
                    ctx.HKGenes.Add(hkgene);
                }
            }
            ctx.SaveChanges();
        }

        [DisplayName("Get All Genes from Filesystem")]
        public void FillDbWithAllGenes()
        {
            ctx.Database.ExecuteSqlCommand("delete from Genes");
            var list = new List<Gene>();

            var config = ctx.Configurations.FirstOrDefault();
            if (config != null)
                using (var reader = new StreamReader(config.HugoGeneNameFilePaths))
                {
                    reader.ReadLine(); //ToDO make better, first line not needed
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split('\t');

                        var gene = new Gene()
                        {
                            DisplayName = values[2],
                            EntrezId = values[18],
                            PubmedId = values[26],
                            RefSeqAccession = values[23],
                            EnsembleGeneId = values[19],
                            Symbol = values[1]
                        };
                        ctx.Genes.Add(gene);
                    }
                }
            ctx.SaveChanges();
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ctx.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}