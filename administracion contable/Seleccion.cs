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
        List<ElementoLibroDiario> elementoLibroDiarios = new List<ElementoLibroDiario>();
        Conexion Conexion;

        public Seleccion(Form padre)
        {
            this.padre = padre;
            InitializeComponent();
            this.ControlBox = false;
        }


        int x = -1;
        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
           x++;
            label1.Text = x.ToString();
            
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            Item item = new Item();
            item.MaximumSize = new Size(390, 90);
            item.Width = flowLayoutPanel1.Width;

            item.index = flowLayoutPanel1.Controls.Count + 1;
            item.editarDatos();

            this.flowLayoutPanel1.Controls.Add(item);

            item.guardar();
        }

        

        private void Seleccion_Load(object sender, EventArgs e)
        {
            recargarLista();

            int dia = 0;

            foreach (ElementoLibroDiario elemento in elementoLibroDiarios)
            {
                dia = (dia >= elemento.dia) ? dia : elemento.dia;
            }
        }

        public void recargarLista()
        {
            elementoLibroDiarios.Clear();
            Conexion = new Conexion();
            Conexion.CargarTodoLibroDiario(elementoLibroDiarios); 
        }
    }
}
