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
            Item[] items = new Item[3];

            for (int i = 0; i < items.Length; i++)
            {
                items[i] = new Item();
                items[i].MaximumSize = new Size(390, 90);
                items[i].descripcion = "holi mundo " + flowLayoutPanel1.Controls.Count;
                items[i].documentacion = "soy documentacion";

                
                this.flowLayoutPanel1.Controls.Add(items[i]);
            }
            
        }

        
    }
}
