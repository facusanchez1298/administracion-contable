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
    public partial class DialogoNombre : Form
    {
        public Item padre { get; private set; }

        public DialogoNombre(Item padre)
        {
            InitializeComponent();
            this.padre = padre;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            padre.descripcion = this.textBox1.Text;
            padre.documentacion = this.dateTimePicker1.Value.ToString("dd/MM/yyyy");
            this.Close();
        }

        private void DialogoNombre_Load(object sender, EventArgs e)
        {
            Conexion conexion = new Conexion();
            conexion.asignarRango(dateTimePicker1);
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
