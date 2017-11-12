using System.IO;
using System.Linq;
using System.Web.Mvc;
using Nomi.Nomi_WebApp.Models;
using Nomi.Nomi_WebApp.HangfireJobs;

// ReSharper disable InconsistentNaming

namespace Nomi.Nomi_WebApp.Controllers
{
    public class GenesController : Controller
    {
        private readonly NoMiDbContext DBContext = new NoMiDbContext();
        private readonly Config Config;

        public GenesController()
        {
            Config = DBContext.Configurations.FirstOrDefault();
        }

        // GET: HKGenes
        public ActionResult Index()
        {
            return View(DBContext.Genes);
        }


        //[DisplayName("Get All HKGenes")]
        //public void FillDbWithHKGenes()
        //{
        //    DBContext.Database.ExecuteSqlCommand("delete from HKGenes");
        //    var config = DBContext.Configurations.FirstOrDefault();
        //    using (var reader = new StreamReader(config.HouseKeepingGenesFilePath))
        //    {
        //        while (!reader.EndOfStream)
        //        {
        //            var symbol = reader.ReadLine();
        //            var hkgene = new HKGene()
        //            {
        //                Gene = DBContext.Genes.First(g => g.Symbol.ToLower().Equals(symbol.ToLower()))
        //            };
        //            DBContext.HKGenes.Add(hkgene);
        //        }
        //    }
        //    DBContext.SaveChanges();
        //}

        [ActionName("RefreshAllGenes")]
        public ActionResult RefreshAllGenes()
        {
            using (var refreshJob = new RefreshAllGenesJob())
            {
                refreshJob.Create(Config.HugoGeneNameSplitPath);
            }
            return RedirectToAction("Index", "Genes", DBContext.Genes);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DBContext.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}