using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipoQ22.Domino
{
    public class Equipo
    {
        public int equipo { get; set; }

        public List<Jugador> ltsjugador { get; set; }

        public string pais { get; set; }

        public string director_Tecnico { get; set; }

        public Equipo()
        {
            equipo = 0;
            ltsjugador = new List<Jugador>();
            director_Tecnico = string.Empty;
            pais = string.Empty;
        }


        public Equipo(int equipo, List<Jugador> ltsjugador,string director_tecnico,string pais)
        {
            this.equipo = equipo;
            this.ltsjugador = ltsjugador;
            this.director_Tecnico = director_Tecnico;
            this.pais = pais;
        }

        public void AgregarDetalle(Jugador jugador)
        {
            ltsjugador.Add(jugador);
        }

        public void QuitarDetalle(int indice)
        {
            ltsjugador.RemoveAt(indice);
        }

        public override string ToString()
        {
            return "Equipo:"+ equipo;
        }
    }

}
