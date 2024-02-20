using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using WpfApp.Models;

namespace WpfApp
{
    internal class ApplicationContext: DbContext
    {
        public DbSet<Abonent> Abonents { get; set; }
        public DbSet<AbonentDetails> AbonentDetails { get; set; }
        public DbSet<AbonentServices> AbonentServices { get; set; }
        public DbSet<AbonentType> AbonentTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=xe)));User Id=HANDAK;Password=111");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<AbonentType>(entity =>
            {
                entity.ToTable("ABONENT_TYPE");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("NUMBER(2)");

                entity.Property(e => e.Name)
                    .HasColumnName("NAME")
                    .HasColumnType("VARCHAR2(32 CHAR)");

                entity.Property(e => e.Mobile)
                    .HasColumnName("MOBILE")
                    .HasColumnType("NUMBER(1)")
                    .HasDefaultValue(0);
            });

            modelBuilder.Entity<Abonent>(entity =>
            {
                entity.ToTable("ABONENT");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).HasColumnName("ID");
                entity.Property(e => e.Country).HasColumnName("COUNTRY");
                entity.Property(e => e.City).HasColumnName("CITY");
                entity.Property(e => e.Pnumber).HasColumnName("PNUMBER");
                entity.Property(e => e.Fax).HasColumnName("FAX");
                entity.Property(e => e.Description).HasColumnName("DESCRIPTION");
                entity.Property(e => e.Ptype).HasColumnName("PTYPE");
                entity.Property(e => e.Secure).HasColumnName("SECURE");
            });

            modelBuilder.Entity<AbonentServices>(entity =>
            {
                entity.ToTable("ABONENT_SERVICES");
                
                entity.HasNoKey();

                entity.Property(e => e.DateTime)
                    .HasColumnName("TIMESTAMP#")
                    .HasColumnType("DATE");

                entity.Property(e => e.AbonentId)
                    .HasColumnName("ABONENT")
                    .HasColumnType("NUMBER(5)");

                entity.Property(e => e.Service)
                    .HasColumnName("SERVICE")
                    .HasColumnType("VARCHAR2(128 CHAR)");

                entity.Property(e => e.Duration)
                    .HasColumnName("DURATION")
                    .HasColumnType("VARCHAR2(32 CHAR)");

                entity.Property(e => e.Cost)
                    .HasColumnName("COST")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.CostNds)
                    .HasColumnName("COST_NDS")
                    .HasColumnType("NUMBER");
            });

            modelBuilder.Entity<AbonentDetails>(entity =>
            {
                entity.ToTable("ABONENT_DETAILS");

                entity.HasNoKey();

                entity.Property(e => e.DateTime)
                    .HasColumnName("TIMESTAMP#")
                    .HasColumnType("DATE");

                entity.Property(e => e.AbonentId)
                    .HasColumnName("ABONENT")
                    .HasColumnType("NUMBER(5)");

                entity.Property(e => e.Reporter)
                    .HasColumnName("REPORTER")
                    .HasColumnType("VARCHAR2(32 CHAR)");

                entity.Property(e => e.Service)
                    .HasColumnName("SERVICE")
                    .HasColumnType("VARCHAR2(64 CHAR)");

                entity.Property(e => e.Duration)
                    .HasColumnName("DURATION")
                    .HasColumnType("VARCHAR2(32 CHAR)");

                entity.Property(e => e.Cost)
                    .HasColumnName("COST")
                    .HasColumnType("NUMBER");

                entity.Property(e => e.Rouming)
                    .HasColumnName("ROUMING")
                    .HasColumnType("VARCHAR2(16 CHAR)");
            });
        }
    }
}
