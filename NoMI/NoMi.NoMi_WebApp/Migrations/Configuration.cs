using Nomi.Nomi_WebApp.Models;

namespace Nomi.Nomi_WebApp.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Nomi.Nomi_WebApp.Models.NoMiDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Nomi.Nomi_WebApp.Models.NoMiDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Configurations.AddOrUpdate(new Config()
            {
                Id = 1,
                HouseKeepingGenesFilePath = @"D:\Programming\NoMI\NoMI\source\HKGene.txt",
                ComprehensiveHumanExpressionMapFilePath = @"D:\Programming\NoMI\NoMI\source\processedMatrix.Aurora.july2015\processedMatrix.Aurora.july2015.txt",
                ComprehensiveHumanExpressionMapSplitPath = @"D:\Programming\NoMI\NoMI\source\processedMatrix.Aurora.july2015\split\",
                AffimetrixProbeIdsFilePath = @"D:\Programming\NoMI\NoMI\source\GPL10355-24537.txt",
                GeneAnnotationsFilePath = @"D:\Programming\NoMI\NoMI\source\A-AFFY-44.adf.txt",
                HugoGeneNameFilePath = @"D:\Programming\NoMI\NoMI\source\hgnc_complete_set\hgnc_complete_set.txt",
                HugoGeneNameSplitPath = @"D:\Programming\NoMI\NoMI\source\hgnc_complete_set\split\"
            });
        }
    }
}
