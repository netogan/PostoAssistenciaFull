using Microsoft.AspNet.Identity.EntityFramework;
using PostoAssistenciaFull.Models.EntityConfig;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace PostoAssistenciaFull.Models
{
    //[DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext() : base("Contexto", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>().Configure(m => m.HasMaxLength(200).HasColumnType("nvarchar"));

            modelBuilder.Configurations.Add(new AssistidoConfig());
            modelBuilder.Configurations.Add(new EnderecoConfig());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Assistido> Assistidos { get; set; }

        public DbSet<Endereco> Enderecos { get; set; }

        public DbSet<Chamada> Chamadas { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        object placeHolderVariable;
    }
}