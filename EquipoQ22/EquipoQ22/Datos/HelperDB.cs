using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using EquipoQ22.Domino;

namespace EquipoQ22.Datos
{
    class HelperDB
    {
        private static HelperDB instancia;

        SqlConnection cnn = new SqlConnection(Properties.Resources.conexion);

        public static HelperDB ObtenerInstancia()
        {
            if (instancia == null)
            {
                instancia = new HelperDB();
            }
            return instancia;
        }

        public DataTable ObtenerCombo(string nombre_sp)
        {
            DataTable tabla = new DataTable();
            SqlCommand cmdCombo = new SqlCommand();
            cnn.Open();
            cmdCombo.Connection = cnn;
            cmdCombo.CommandText = nombre_sp;
            cmdCombo.CommandType = CommandType.StoredProcedure;
            tabla.Load(cmdCombo.ExecuteReader());
            cnn.Close();
            return tabla;
        }

        public bool CargarEquipo(Equipo equipo)
        {
            bool aux = true;
            SqlCommand cmdEquipo = null;
            SqlTransaction t = null;

            try
            {
                cmdEquipo = new SqlCommand();
                cnn.Open();
                t = cnn.BeginTransaction();
                cmdEquipo.Transaction = t;
                cmdEquipo.Connection = cnn;
                cmdEquipo.CommandText = "[dbo].[SP_INSERTAR_EQUIPO]";
                cmdEquipo.CommandType = CommandType.StoredProcedure;
                cmdEquipo.Parameters.Add("@pais", equipo.pais);
                cmdEquipo.Parameters.Add("@director_tecnico", equipo.director_Tecnico);

                SqlParameter pOut = new SqlParameter();
                pOut.ParameterName = "@id ";
                pOut.Direction = ParameterDirection.Output;
                pOut.DbType = DbType.Int32;
                cmdEquipo.Parameters.Add(pOut);
                cmdEquipo.ExecuteNonQuery();

                int nro_equipo = (int)pOut.Value;

                SqlCommand cmdJugador = null;
                foreach (Jugador jugador in equipo.ltsjugador)
                {
                    cmdJugador = new SqlCommand("[dbo].[SP_INSERTAR_DETALLES_EQUIPO]", cnn, t);
                    cmdJugador.CommandType = CommandType.StoredProcedure;
                    cmdJugador.Parameters.AddWithValue("@id_equipo", nro_equipo);
                    cmdJugador.Parameters.AddWithValue("@id_persona",jugador.Persona.IdPersona);
                    cmdJugador.Parameters.AddWithValue("@camiseta",jugador.Camiseta);
                    cmdJugador.Parameters.AddWithValue("@posicion",jugador.Posicion);
                    cmdJugador.ExecuteNonQuery();
                }
                t.Commit();
            }
            catch (Exception ex)
            {
                if (t != null)
                {
                    t.Rollback();
                    aux = false;
                }
            }
            finally
            {
                if (cnn != null && cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }
            }
            return aux;
        }
    }
}
