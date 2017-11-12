using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nomi.Nomi_WebApp.Manager;
using Nomi.Nomi_WebApp.Models;

namespace NoMI_UnitTest
{
    [TestClass]
    public class FileManagerUnit : IDisposable
    {
        public Config Config { get; set; }
        // ReSharper disable once InconsistentNaming
        public NoMiDbContext DBContext { get; set; }

        public FileManagerUnit()
        {
            DBContext = new NoMiDbContext();
            Config = DBContext.Configurations.FirstOrDefault();
        }

        [TestMethod]
        public async Task ReadAndWriteAllGenesToDataBase_ValidGenes_InDatabase()
        {
            //Arrange
            var manager = new GeneFileManager();
            var filePath = @"D:\Programming\NoMI\NoMI\source\hgnc_complete_set\split\hgnc_complete_set_0.txt";
            //Act
            await manager.ReadAndWriteAllGenesToDataBase(filePath);
            //Assert
            Assert.IsTrue(DBContext.Genes.Any());
        }
        [TestMethod]
        public async Task ReadAndWriteAllHKGenesToDataBase_ValidGenes_InDatabase()
        {
            //Arrange
            var manager = new HKGeneFileManager();
            var filePath = Config.HouseKeepingGenesFilePath;
            //Act
            await manager.ReadAndWriteAllHKGenesToDataBase(filePath);
            //Assert
            Assert.IsTrue(DBContext.HKGenes.Any());
        }

        [TestMethod]
        public void SplitHugoFile()
        {
            var manager = new FileManager {DbContext = new NoMiDbContext()};
            var filePath = Config.HugoGeneNameFilePath;
            manager.Split(filePath,Config.HugoGeneNameSplitPath);

        }
        [TestMethod]
        public void SplitComprehensiveHumanExpressionMapFile()
        {
            var manager = new FileManager { DbContext = new NoMiDbContext() };
            var filePath = Config.ComprehensiveHumanExpressionMapFilePath;
            manager.Split(filePath,Config.ComprehensiveHumanExpressionMapSplitPath);

        }
        public void Dispose()
        {
            DBContext?.Dispose();
        }
    }
}
