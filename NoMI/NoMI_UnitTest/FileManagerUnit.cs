using System;
using System.Linq;
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
        public void ReadAndWriteAllGenesToDataBase_ValidGenes_InDatabase()
        {
           
            //Arrange
            var manager = new AllGeneFileManager();
            var filePath = Config.HugoGeneNameFilePath;
            //Act
            manager.ReadAndWriteAllGenesToDataBase(filePath);
            //Assert
            Assert.IsTrue(DBContext.Genes.Any());
        }
        [TestMethod]
        public void ReadAndWriteAllHKGenesToDataBase_ValidGenes_InDatabase()
        {
            //Arrange
            var manager = new AllHKGeneFileManager();
            var filePath = Config.HouseKeepingGenesFilePath;
            //Act
            manager.ReadAndWriteAllHKGenesToDataBase(filePath);
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
