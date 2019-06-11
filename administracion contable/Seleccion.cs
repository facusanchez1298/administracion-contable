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
    public partial class Seleccion : Form
    {
        Form padre;

        public Seleccion(Form padre)
        {
            this.padre = padre;
            InitializeComponent();
            this.ControlBox = false;
        }


        int x = 0;
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
           x++;
            label1.Text = x.ToString();
            
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            Item item = new Item();
            item.MaximumSize = new Size(390, 90);
            item.descripcion = "holi mundo " + flowLayoutPanel1.Controls.Count;
            item.documentacion = "soy documentacion";

            item.index = flowLayoutPanel1.Controls.Count + 1;
            item.guardar();

            this.flowLayoutPanel1.Controls.Add(item);


        }


    }
}
