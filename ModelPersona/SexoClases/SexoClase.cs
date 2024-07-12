using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersona.SexoClases
{
    public partial class SexoClase
    {
        public int idSexo { get; set; }
        public string Sexo1 { get; set; }

        public List<Sexo> getSexo()
        {
            var sexoList = new List<Sexo>();
            try
            {
                using(var context = new PruebaTecnicaPersonasEntities())
                {
                    sexoList = context.Sexo.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return sexoList;
        }
    }
}
