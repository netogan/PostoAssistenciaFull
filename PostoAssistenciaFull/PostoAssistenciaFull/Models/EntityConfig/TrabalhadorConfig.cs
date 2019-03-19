using System.Data.Entity.ModelConfiguration;

namespace PostoAssistenciaFull.Models.EntityConfig
{
    public class TrabalhadorConfig : EntityTypeConfiguration<Trabalhador>
    {
        public TrabalhadorConfig()
        {
            HasKey(a => a.TrabalhadorId);

            //HasOptional(s => s.Assistido).WithOptionalPrincipal(ad => ad.EnderecoAtual)

            ToTable("Trabalhadores");
        }
    }
}