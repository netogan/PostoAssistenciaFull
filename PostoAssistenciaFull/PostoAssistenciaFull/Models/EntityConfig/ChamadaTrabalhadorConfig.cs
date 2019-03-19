using System.Data.Entity.ModelConfiguration;

namespace PostoAssistenciaFull.Models.EntityConfig
{
    public class ChamadaTrabalhadorConfig : EntityTypeConfiguration<ChamadaTrabalhador>
    {
        public ChamadaTrabalhadorConfig()
        {
            HasKey(a => a.ChamadaTrabalhadorId);

            ToTable("ChamadaTrabalhadores");
        }
    }
}