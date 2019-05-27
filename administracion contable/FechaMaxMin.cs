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
    public partial class FechaMaxMin : Form
    {
        Form padre;

        public FechaMaxMin(Form padre)
        {
            this.padre = padre;
            InitializeComponent();

            //evitamos que pueda cerrarse o maximizarse
            this.MaximizeBox = false;
            this.ControlBox = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime min = this.dateMin.Value;
            DateTime max = this.dateMax.Value;
            Conexion conexion = new Conexion();
            conexion.guardarLimites(min.ToString(), max.ToString());



            LibroDiario libroDiario = new LibroDiario(padre);
            libroDiario.Show();
            this.Hide();
            
        }
    }
}
