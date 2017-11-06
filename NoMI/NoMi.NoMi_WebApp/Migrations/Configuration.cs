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
                HouseKeepingGenesFilePath = @"D:\Programming\NoMi_Correct\source\HKGene.txt",
                HugoGeneNameFilePaths = @"D:\Programming\NoMi_Correct\source\HUGO_edit.txt",
                ComprehensiveHumanExpressionMapFilePath = @"D:\Programming\NoMi_Correct\source\processedMatrix.Aurora.july2015.txt",
                AffimetrixProbeIdsFilePath = @"D:\Programming\NoMi_Correct\source\GPL10355-24537.txt"
            });
        }
    }
}
