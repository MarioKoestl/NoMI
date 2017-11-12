using System.IO;
using System.Linq;
using System.Web.Mvc;
using Nomi.Nomi_WebApp.Models;
using Nomi.Nomi_WebApp.HangfireJobs;
using System.Data.Entity;
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

        public ActionResult Index()
        {
            return View(DBContext.Genes);
        }
        public ActionResult HKGenes()
        {
            return View(DBContext.HKGenes
                .Include(g=>g.Gene));
        }

        [ActionName("RefreshAllHkGenes")]
        public ActionResult RefreshAllHkGenes()
        {
            using (var refreshJob = new RefreshAllHkGenesJob())
            {
                refreshJob.Create(Config.HouseKeepingGenesFilePath);
            }
            return RedirectToAction("HKGenes", "Genes", DBContext.Genes);
        }

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