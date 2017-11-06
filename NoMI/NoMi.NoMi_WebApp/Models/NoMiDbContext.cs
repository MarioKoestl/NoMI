using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Profile;

namespace Nomi.Nomi_WebApp.Models
{
	public class NoMiDbContext : DbContext
	{
	    public DbSet<Gene> Genes { get; set; }
	    public DbSet<HKGene> HKGenes { get; set; }
        public DbSet<Config> Configurations { get; set; }
	}
}