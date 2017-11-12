using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Nomi.Nomi_WebApp.Manager;
using hf = Hangfire;

namespace Nomi.Nomi_WebApp.HangfireJobs
{
    public class RefreshAllGenesJob : IDisposable
    {
        public GeneFileManager FileManager { get; set; }

        [DisplayName("GetGenesFromFile: {0}")]
        public async Task GetGenesFromFile(string filePath)
        {
            FileManager = new GeneFileManager();
            await FileManager.ReadAndWriteAllGenesToDataBase(filePath);
        }

        public void RefreshAllGenes(string splitFileDirPath)
        {
            var filePaths = Directory.GetFiles(splitFileDirPath);
            foreach (var filePath in filePaths)
            {
                hf.BackgroundJob.Enqueue(() => GetGenesFromFile(filePath));
            }
            
        }
        public void Create(string splitFileDirPath)
        {
            RefreshAllGenes(splitFileDirPath);
        }

        public void Dispose()
        {
            FileManager?.Dispose();
        }
    }
}