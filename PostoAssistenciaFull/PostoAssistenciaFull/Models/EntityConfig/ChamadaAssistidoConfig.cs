using System.Data.Entity.ModelConfiguration;

namespace PostoAssistenciaFull.Models.EntityConfig
{
    public class ChamadaAssistidoConfig : EntityTypeConfiguration<ChamadaAssistido>
    {
        public ChamadaAssistidoConfig()
        {
            HasKey(a => a.ChamadaAssistidoId);

            ToTable("ChamadaAssistidos");
        }
    }
}