using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nomi.Nomi_WebApp.Models;

namespace Nomi.Nomi_WebApp.Controllers
{
    public class SamplesController : Controller
    {
        private readonly NoMiDbContext ctx = new NoMiDbContext();
        // GET: Samples
        public ActionResult Index()
        {
            var x = GetAllSamples();
            return View();
        }

        private object GetAllSamples()
        {
            var list = new List<HKGene>();
            var config = ctx.Configurations.FirstOrDefault();
            if (config != null)
            {
                using (var reader = new StreamReader(config.ComprehensiveHumanExpressionMapFilePath))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split('\t');


                    }
                }
            }
            return list;
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