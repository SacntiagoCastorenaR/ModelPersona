using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace ModelPersona.CuentasClases
{
    public partial class CuentasClase
    {
        public int idCuentas { get; set; }
        public int idPersona { get; set; }
        public string Banco { get; set; }
        public string Cuenta { get; set; }

        public CuentasClase getCuentasById(int id)
        {
            var executeQueryDatabase = new GetCuentasById_Result();
            var cuenttas = new CuentasClase();
            var query = string.Empty;
            try
            {
                query = "EXEC GetCuentasById @idCuentas";
                using(var context = new PruebaTecnicaPersonasEntities())
                {
                    executeQueryDatabase = context.Database.SqlQuery<GetCuentasById_Result>(query,
                        new SqlParameter("@idCuentas", id)).FirstOrDefault();
                    if(executeQueryDatabase != null)
                    {
                        cuenttas.idCuentas = executeQueryDatabase.idCuentas;
                        cuenttas.Banco = executeQueryDatabase.Banco;
                        cuenttas.Cuenta = executeQueryDatabase.Cuenta;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cuenttas;
        }

        public void CrudCuentas(int id)
        {
            if (id == 0)
            {
                SaveCuenta();
            }
            else
            {
                UpdateCuenta(id);
            }
        }

        public void SaveCuenta()
        {
            var query = string.Empty;
            try
            {
                using (var context = new PruebaTecnicaPersonasEntities())
                {
                    query = "EXEC InsertCuentas @idPersona,@Banco,@Cuenta";
                    context.Database.ExecuteSqlCommand(query,
                        new SqlParameter("@idPersona", idPersona),
                        new SqlParameter("@Banco", Banco),
                        new SqlParameter("@Cuenta", Cuenta));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void UpdateCuenta(int id)
        {
            var query = string.Empty;
            try
            {
                using (var context = new PruebaTecnicaPersonasEntities())
                {
                    query = "EXEC UpdateCuentas @idCuentas,@idPersona,@Banco,@Cuenta";
                    context.Database.ExecuteSqlCommand(query,
                        new SqlParameter("@idCuentas", id),
                        new SqlParameter("@idPersona", idPersona),
                        new SqlParameter("@Banco", Banco),
                        new SqlParameter("@Cuenta", Cuenta));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public void DeleteCuenta(int id)
        {
            var query = string.Empty;
            try
            {
                using (var context = new PruebaTecnicaPersonasEntities())
                {
                    query = "EXEC DeleteCuenta @idCuentas";
                    context.Database.ExecuteSqlCommand(query,
                        new SqlParameter("@idCuentas", id));
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
