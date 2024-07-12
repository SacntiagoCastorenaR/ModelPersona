using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersona.PaisClases
{
    public partial class PaisClase
    {
        public int idPais { get; set; }
        public string Pais1 { get; set; }

        public List<Pais> getPais()
        {
            var getPais = new List<Pais>();
            try
            {
                using(var context = new PruebaTecnicaPersonasEntities())
                {
                    getPais = context.Pais.ToList();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return getPais;
        }
    }
}
