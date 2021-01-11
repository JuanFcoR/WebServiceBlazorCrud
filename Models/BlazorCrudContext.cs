using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebServiceBlazorCrud.Models.Response;

#nullable disable

namespace WebServiceBlazorCrud.Models
{
    public partial class BlazorCrudContext : DbContext
    {
        public BlazorCrudContext()
        {
        }

        public BlazorCrudContext(DbContextOptions<BlazorCrudContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cerveza> Cervezas { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                /*optionsBuilder.UseSqlServer(
                    "Server=tcp:juansfcorerver.database.windows.net,1433;Initial Catalog=BlazorCrud;Persist Security Info=False;User ID=juanfcor;Password=Softendo03;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                   );*/
                /*
                 Scaffold-DbContext "server=bxpp4awzgjkuajm3fvl6-mysql.services.clever-cloud.com;port=3306;user=u81tdr5i30nvfgey;password=QJ4Qpz9jBzbgeJL25g6i;database=bxpp4awzgjkuajm3fvl6" Pomelo.EntityFrameWorkCore.MySql -OutputDir bxpp4awzgjkuajm3fvl6 -Tables usuario, language -f
                 */
                optionsBuilder.UseMySQL(@"Server =bxpp4awzgjkuajm3fvl6-mysql.services.clever-cloud.com; Database=bxpp4awzgjkuajm3fvl6; Uid=u81tdr5i30nvfgey; Pwd=QJ4Qpz9jBzbgeJL25g6i; Port=3306; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cerveza>(entity =>
            {
                entity.ToTable("cerveza");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });
            /*
              modelBuilder.Entity<Cerveza>(entity =>
            {
                entity.ToTable("cerveza");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Marca)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("marca");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nombre");
            });
             */

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");

                entity.Property(e => e.UsuarioId).HasColumnType("int(11)");

                entity.Property(e => e.Apelidos)
                    .IsRequired()
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Cedula)
                    .IsRequired()
                    .HasColumnType("varchar(11)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Correo)
                    .IsRequired()
                    .HasColumnType("varchar(30)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Nacimiento).HasColumnType("datetime");

                entity.Property(e => e.Nombres)
                    .IsRequired()
                    .HasColumnType("varchar(15)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasColumnType("varchar(10)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
