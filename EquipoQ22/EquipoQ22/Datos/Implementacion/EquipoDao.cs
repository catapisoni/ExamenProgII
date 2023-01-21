using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipoQ22.Datos.Implementacion;
using EquipoQ22.Domino;
using System.Data;
using System.Data.SqlClient;


namespace EquipoQ22.Datos.Implementacion
{
    class EquipoDao: IEquipoDao
    {
     
        public List<Persona> ObtenerPersonas()
        {
            List<Persona> lstPersonas = new List<Persona>();
            string nombre_sp = "[dbo].[SP_CONSULTAR_PERSONAS]";
            DataTable tabla = HelperDB.ObtenerInstancia().ObtenerCombo(nombre_sp);
            foreach (DataRow row in tabla.Rows)
            {
                int id = Convert.ToInt32(row["id_persona"].ToString());
                string nombre = row["nombre_completo"].ToString();
                int clase = Convert.ToInt32(row["clase"].ToString());
                Persona persona = new Persona(id, nombre, clase);
                lstPersonas.Add(persona);
            }
            return lstPersonas;
        }
    }
}
