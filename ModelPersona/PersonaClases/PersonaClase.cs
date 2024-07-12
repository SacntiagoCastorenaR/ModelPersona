using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersona.PersonaClases
{
    public partial class PersonaClase
    {
        public int idPersona { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public int idTipoRegimen { get; set; }
        public System.DateTime FechaNacimiento { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public int idPais { get; set; }
        public int idSexo { get; set; }
        public bool Estatus { get; set; }

        public List<GetPersona_Result> getPersona()
        {
            var getPersona = new List<GetPersona_Result>();
            var query = string.Empty;
            try
            {
                using (var context = new PruebaTecnicaPersonasEntities())
                {
                    query = "EXEC GetPersona";
                    getPersona = context.Database.SqlQuery<GetPersona_Result>(query).ToList();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return getPersona;
        }

        public PersonaClase getPersonaById(int id)
        {
            var executeQueryDatabase = new GetPersonaById_Result();
            var persona = new PersonaClase();
            var query = string.Empty;
            try
            {
                using (var context = new PruebaTecnicaPersonasEntities())
                {
                    query = "EXEC GetPersonaById @idPersona";
                    executeQueryDatabase = context.Database.SqlQuery<GetPersonaById_Result>(query,
                        new SqlParameter("@idPersona", id)).FirstOrDefault();
                    if(executeQueryDatabase != null)
                    {
                        persona.idPersona = executeQueryDatabase.idPersona;
                        persona.Nombre = executeQueryDatabase.Nombre;
                        persona.ApellidoPaterno = executeQueryDatabase.ApellidoPaterno;
                        persona.ApellidoMaterno = executeQueryDatabase.ApellidoMaterno;
                        persona.idTipoRegimen = executeQueryDatabase.idTipoRegimen;
                        string formatDate = executeQueryDatabase.FechaNacimiento.ToString("dd/MM/yyyy");
                        persona.FechaNacimiento =DateTime.ParseExact(formatDate,"dd/MM/yyyy", CultureInfo.InvariantCulture);
                        persona.RFC = executeQueryDatabase.RFC;
                        persona.CURP = executeQueryDatabase.CURP;
                        persona.idPais = executeQueryDatabase.idPais;
                        persona.idSexo = executeQueryDatabase.idSexo;
                        persona.Estatus = executeQueryDatabase.Estatus;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return persona;
        }

        public void CrudPersona(int id)
        {
            if (id == 0)
            {
                SavePersona();
            }
            else
            {
                UpdatePersona(id);
            }
        }

        public void SavePersona()
        {
            var query = string.Empty;
            try
            {
                using (var context = new PruebaTecnicaPersonasEntities())
                {
                    query = "EXEC InsertPersona @Nombre,@ApellidoPaterno,@ApellidoMaterno,@idTipoRegimen,@FechaNacimiento,@RFC,@CURP,@idPais,@idSexo,@Estatus";
                    context.Database.ExecuteSqlCommand(query,
                        new SqlParameter("@Nombre", Nombre),
                        new SqlParameter("@ApellidoPaterno", ApellidoPaterno),
                        new SqlParameter("@ApellidoMaterno", ApellidoMaterno),
                        new SqlParameter("@idTipoRegimen", idTipoRegimen),
                        new SqlParameter("@FechaNacimiento", FechaNacimiento),
                        new SqlParameter("@RFC", RFC),
                        new SqlParameter("@CURP", CURP),
                        new SqlParameter("@idPais", idPais),
                        new SqlParameter("@idSexo", idSexo),
                        new SqlParameter("Estatus", Estatus));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdatePersona(int id)
        {
            var query = string.Empty;
            try
            {
                using (var context = new PruebaTecnicaPersonasEntities())
                {
                    query = "EXEC UpdatePresona @idPersona,@Nombre,@ApellidoPaterno,@ApellidoMaterno,@idTipoRegimen,@FechaNacimiento,@RFC,@CURP,@idPais,@idSexo,@Estatus";
                    context.Database.ExecuteSqlCommand(query,
                        new SqlParameter("@idPersona",id),
                        new SqlParameter("@Nombre", Nombre),
                        new SqlParameter("@ApellidoPaterno", ApellidoPaterno),
                        new SqlParameter("@ApellidoMaterno", ApellidoMaterno),
                        new SqlParameter("@idTipoRegimen", idTipoRegimen),
                        new SqlParameter("@FechaNacimiento", FechaNacimiento),
                        new SqlParameter("@RFC", RFC),
                        new SqlParameter("@CURP", CURP),
                        new SqlParameter("@idPais", idPais),
                        new SqlParameter("@idSexo", idSexo),
                        new SqlParameter("Estatus", Estatus));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void DeletePersona(int id)
        {
            var query = string.Empty;
            try
            {
                using (var context = new PruebaTecnicaPersonasEntities())
                {
                    query = "EXEC DeletePersona @idPersona";
                    context.Database.ExecuteSqlCommand(query,
                        new SqlParameter("@idPersona", id));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string GetJson(int id)
        {
            var result = string.Empty;
            var query = string.Empty;
            try
            {
                using (var context = new PruebaTecnicaPersonasEntities())
                {
                    query = "EXEC JsonCuentasBancos @idPersona";
                    result = context.Database.SqlQuery<string>(query,
                        new SqlParameter("@idPersona",id)).FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return result;
        }
    }
}
