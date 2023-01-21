using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipoQ22.Servicio.Interfaz;
using EquipoQ22.Datos.Implementacion;
using EquipoQ22.Datos;
using EquipoQ22.Domino;

namespace EquipoQ22.Servicio.Implementacion
{
    public class Servicios : IServicio
    {
        public IEquipoDao dao;

        public Servicios()
        {
            dao = new EquipoDao();
        }
        public List<Persona> ObtenerPersonas()
        {
            return dao.ObtenerPersonas();
        }

    }
}
