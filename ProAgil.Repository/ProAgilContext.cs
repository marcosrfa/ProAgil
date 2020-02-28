using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;
using ProAgil.Domain.Identity;

namespace ProAgil.Repository
{
    public class ProAgilContext : IdentityDbContext<User, Role, int,
                                                    IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                                    IdentityRoleClaim<int>, IdentityUserToken<int>> 
    {
        public ProAgilContext(DbContextOptions<ProAgilContext> options) : base (options){ }

        public DbSet <Evento> Eventos { get; set; }
        public DbSet <Palestrante> Palestrantes { get; set; }
        public DbSet <PalestranteEvento> PalestrantesEventos { get; set; }
        public DbSet <Lote> Lote { get; set; }
        public DbSet <RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder){
            //Criar tabelas de Autenticação
            base.OnModelCreating(modelbuilder);

            modelbuilder.Entity<UserRole>(userRole =>{
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();

            });

            modelbuilder.Entity<PalestranteEvento>()
                .HasKey(PE => new {
                    PE.EventoId, 
                    PE.PalestranteId
                });
        }

    }
}