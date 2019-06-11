using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace administracion_contable
{
    public partial class Inicio : Form
    {
        public Inicio()
        {
            Conexion conexion = new Conexion();
           conexion.crearTablas();
            this.MaximizeBox = false;
            InitializeComponent();
        }

 

      

        private void toolStripMenuNuevoLibroDiario(object sender, EventArgs e)
        {
                LibroMayor elementoContable = new LibroMayor(this);
                elementoContable.Show();
                this.Hide();
        }

        private void toolStripMenuNuevoPlanContable(object sender, EventArgs e)
        {
            PlanContable elementoContable = new PlanContable(this);
            elementoContable.Show();
            this.Hide();
        }

        

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void abrirLibroDiario(object sender, EventArgs e)
        {
            Conexion conexion = new Conexion();
            if (!conexion.hayFechaLimite())
            {
                FechaMaxMin fecha = new FechaMaxMin(this);
                fecha.Show();
                this.Hide();
            }
            else
            {
                Seleccion seleccion = new Seleccion(this);
                seleccion.Show();
                this.Hide();
            }
        }

        private void formatearDatosToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string respuesta = mostrarMensajeConfirmacion("esta seguro que desea eliminar todos los datos de forma permanente?");

            if (respuesta.Equals("OK"))
            {
                Conexion conexion = new Conexion();
                conexion.borrarBaseDeDatos();
            }
            
        }

        /// <summary>
        /// hace una pregunta al usuario para confirmar su decision
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns>retorna OK si el usuario acepta, cancel si no acepta</returns>
        public String mostrarMensajeConfirmacion(string mensaje)
        {
            const string caption = "Datos Erroneos";
            var result = MessageBox.Show(mensaje, caption,
                                         MessageBoxButtons.OKCancel,
                                         MessageBoxIcon.Warning);

            return result.ToString();//puede ser ok o cancel
        }

        private void selecctorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Seleccion elementoContable = new Seleccion(this);
            elementoContable.Show();
            this.Hide();
        }
    }
}
