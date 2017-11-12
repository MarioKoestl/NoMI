using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using Nomi.Nomi_WebApp.Models;

namespace Nomi.Nomi_WebApp.Manager
{
	public class FileManager : IDisposable
	{
	    public NoMiDbContext DbContext { get; set; }
	    public string FilePath { get; set; }

	    public void Initialize(string filePath)
	    {
	        DbContext = new NoMiDbContext();
	        FilePath = filePath;
	    }

	    public void Split(string inputfile, string splitDirPath)
	    {
	        int i = 0;
	        int count = 0;
	        System.IO.StreamWriter outfile = null;
	        string line;
	        try
	        {
	            using (var infile = new System.IO.StreamReader(inputfile))
	            {
	                while (!infile.EndOfStream)
	                {
	                    line = infile.ReadLine();
	                    if (count % 1000 == 0)
	                    {
	                        if (outfile != null)
	                        {
	                            outfile.Dispose();
	                            outfile = null;
	                        }
	                        count++;
	                        continue;
	                    }
	                    if (outfile == null)
	                    {
	                        outfile = new System.IO.StreamWriter(Path.Combine(splitDirPath,
	                            Path.GetFileNameWithoutExtension(inputfile) +$"_{i++}.txt"),
	                            false,
	                            infile.CurrentEncoding);
	                       
	                    }
	                    outfile.WriteLine(line);
	                    count++;
                    }

	            }
	        }
	        finally
	        {
	            if (outfile != null)
	                outfile.Dispose();
	        }
	    }



        protected virtual void Dispose(bool disposing)
	    {
	        if (disposing)
	        {
	            DbContext?.Dispose();
	        }
	    }

	    public void Dispose()
	    {
	        Dispose(true);
	        GC.SuppressFinalize(this);
	    }
	}
}