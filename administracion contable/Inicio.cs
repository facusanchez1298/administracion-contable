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
                LibroDiario elementoContable = new LibroDiario(this);
                elementoContable.Show();
                this.Hide();
            }
        }
    }
}
