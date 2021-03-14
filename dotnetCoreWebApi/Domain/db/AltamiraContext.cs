using System;
using System.Linq;
using AltamiraTaskApi.Domain.Repositories;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AltamiraTaskApi.Domain
{
    public partial class AltamiraContext : DbContext
    {

        public AltamiraContext()
        {

        }

        public AltamiraContext(DbContextOptions<AltamiraContext> options)
            : base(options)
        {
            
         
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Geo> Geo { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserManager> UserManager { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //optionsBuilder.UseSqlServer("Server=.;Database=Altamira;Trusted_Connection=True;", buider => buider.EnableRetryOnFailure());
            optionsBuilder.UseSqlServer("Server=mssqldb,1433;Initial Catalog=Altamira;User ID=SA;Password=Altamira1111!!", buider => buider.EnableRetryOnFailure());

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserManager>().HasData(
                new UserManager
                {
                    Id = 1,
                    Email = "test",
                    Password = "test"
                });


            OnModelCreatingPartial(modelBuilder);
          

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);


    }
}
