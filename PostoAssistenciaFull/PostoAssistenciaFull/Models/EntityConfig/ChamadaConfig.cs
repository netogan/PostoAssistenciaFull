using System.Data.Entity.ModelConfiguration;

namespace PostoAssistenciaFull.Models.EntityConfig
{
    public class ChamadaConfig : EntityTypeConfiguration<Chamada>
    {
        public ChamadaConfig()
        {
            HasKey(a => a.ChamadaId);

            ToTable("Chamadas");
        }
    }
}