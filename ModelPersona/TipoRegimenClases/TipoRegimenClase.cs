using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersona.TipoRegimenClases
{
    public partial class TipoRegimenClase
    {
        public int idTipoRegimen { get; set; }
        public string Descripcion { get; set; }

        public List<TipoRegimenFiscal> getTipoRegimen()
        {
            var tipo = new List<TipoRegimenFiscal>();
            try
            {
                using(var context = new PruebaTecnicaPersonasEntities())
                {
                    tipo = context.TipoRegimenFiscal.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return tipo;
        }
    }
}
