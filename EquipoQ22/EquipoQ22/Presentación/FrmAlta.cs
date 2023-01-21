using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EquipoQ22.Domino;
using EquipoQ22.Servicio.Interfaz;
using EquipoQ22.Servicio.Implementacion;
using EquipoQ22.Datos;




//CadenaDeConexion: "Data Source=sqlgabineteinformatico.frc.utn.edu.ar;Initial Catalog=Qatar2022;User ID=alumnoprog22;Password=SQL+Alu22"

namespace EquipoQ22
{
    public partial class FrmAlta : Form
    {
        private IServicio gestor;
        private Equipo nuevo;

        public FrmAlta()
        {
            InitializeComponent();
            gestor = new Servicios();
            nuevo = new Equipo();
        }
        private void FrmAlta_Load(object sender, EventArgs e)
        {
            CargarJugadores();
            LimpiarCampos();
            cboPosicion.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (cboPersona.SelectedIndex == -1)
            {
                MessageBox.Show("No se selecciono una persona para ingresar!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(nudCamiseta.Value<=0 || nudCamiseta.Value>23)
            {
                MessageBox.Show("Ingresar camisetas del 1 al 23", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cboPosicion.SelectedIndex == -1)
            {
                MessageBox.Show("No se selecciono la posicion!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (DataGridViewRow row in dgvDetalles.Rows)
            {
                if (row.Cells["jugador"].Value == null)
                {
                    break;
                }
                if (row.Cells["jugador"].Value.ToString().Equals(cboPersona.Text))
                {
                    MessageBox.Show("El Jugador: " + cboPersona.Text + " Ya fue ingresado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (row.Cells["camiseta"].Value.ToString().Equals(nudCamiseta.Value.ToString()))
                {
                    MessageBox.Show("El numero de camiseta: " + nudCamiseta.Value.ToString() + " Ya fue ingresado", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            Persona persona = (Persona)cboPersona.SelectedItem;
            int camiseta = Convert.ToInt32(nudCamiseta.Value);
            string posicion = cboPosicion.Text;

            Jugador jugador = new Jugador(persona, camiseta, posicion);
            nuevo.AgregarDetalle(jugador);
            dgvDetalles.Rows.Add(new object[] { jugador.Persona.IdPersona, jugador.Persona.NombreCompleto, jugador.Camiseta, jugador.Posicion });
            CantidaJugadores();
        }

        private void CantidaJugadores()
        {
            int aux = dgvDetalles.Rows.Count;
            if (aux > 0)
            {
                lblTotal.Text = "Cantidad de jugadores: " + aux.ToString();
            }
        
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            if (txtPais.Text == string.Empty)
            {
                MessageBox.Show("No se ingreso el nombre del pais", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPais.Focus();
                return;   
            }

            if (txtDT.Text == string.Empty)
            {
                MessageBox.Show("No se ingreso el nombre del Dt", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtDT.Focus();
                return;
            }

            if (cboPersona.SelectedIndex==-1)
            {
                MessageBox.Show("No se ingreso el ningun jugador", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboPersona.Focus();
                return;
            }
            if (nudCamiseta.Value<=0)
            {
                MessageBox.Show("No se ingreso ninguna camiseta", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                nudCamiseta.Focus();
                return;
            }

            if (cboPosicion.SelectedIndex==-1)
            {
                MessageBox.Show("No se ingreso una posicion", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboPosicion.Focus();
                return;
            }

            
            nuevo.pais = txtPais.Text;
            nuevo.director_Tecnico = txtPais.Text;

            if (HelperDB.ObtenerInstancia().CargarEquipo(nuevo))
            {
                MessageBox.Show("Se cargo el equipo correctamente!", "Confirmacion", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LimpiarCampos();
                lblTotal.Text = "Cantidad de jugadores:";
            }
            else
            {
                MessageBox.Show("No se pudo cargar el equipo!", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimpiarCampos()
        {
            txtDT.Text = string.Empty;
            txtPais.Text = string.Empty;
            cboPersona.SelectedIndex = -1;
            nudCamiseta.Value = 1;
            cboPosicion.SelectedIndex = -1;
            dgvDetalles.Rows.Clear();
        }

        private void CargarJugadores()
        {
            cboPersona.DisplayMember = "nombre_completo";
            cboPersona.ValueMember = "id_persona";
            cboPersona.DataSource = gestor.ObtenerPersonas();
            cboPersona.DropDownStyle = ComboBoxStyle.DropDownList;
        
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("¿Está seguro que desea cancelar?", "Salir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void dgvDetalles_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDetalles.CurrentCell.ColumnIndex == 4)
            {
                nuevo.QuitarDetalle(dgvDetalles.CurrentRow.Index);
                dgvDetalles.Rows.Remove(dgvDetalles.CurrentRow);
                CantidaJugadores();
            }
        }

        private void txtPais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
