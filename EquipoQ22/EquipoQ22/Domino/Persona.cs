using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Domino
{
    public class Persona
    {
        public int IdPersona { get; set; }
        public string NombreCompleto { get; set; }
        public int Clase { get; set; }

        public Persona(int id, string nombre, int clase)
        {
            IdPersona = id;
            NombreCompleto = nombre;
            Clase = clase;
        }

        public override string ToString()
        {
            return NombreCompleto;
        }

    }
}
