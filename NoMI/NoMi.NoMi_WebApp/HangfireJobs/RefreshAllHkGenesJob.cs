using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Hangfire.Common;
using Nomi.Nomi_WebApp.Manager;
using hf = Hangfire;

namespace Nomi.Nomi_WebApp.HangfireJobs
{
    public class RefreshAllHkGenesJob : IDisposable
    {
        public HKGeneFileManager FileManager { get; set; }

        [DisplayName("GetHKGenesFromFile: {0}")]
        public async Task GetHkGenesFromFile(string filePath)
        {
            FileManager = new HKGeneFileManager();
            await FileManager.ReadAndWriteAllHKGenesToDataBase(filePath);
        }

        public void RefreshAllHkGenes(string filePath)
        {
            hf.BackgroundJob.Enqueue(() => GetHkGenesFromFile(filePath));
        }
        public void Create(string filePath)
        {
            RefreshAllHkGenes(filePath);
        }

        public void Dispose()
        {
            FileManager?.Dispose();
        }
    }
}