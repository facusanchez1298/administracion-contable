using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace administracion_contable
{
    public partial class Item : UserControl
    {

        #region propiedades

        private string _descripcion;
        private string _documentacion;
        private int _index;

        [Category("Propiedades")]
        public string descripcion
        {
            get { return _descripcion; }
            set { _descripcion = value; lblDescripcion.Text = value; }
        }

        [Category("Propiedades")]
        public string documentacion
        {
            get { return _documentacion; }
            set { _documentacion = value; lblDocumentacion.Text = value; }
        }

        [Category("Propiedades")]
        public int index
        {
            get { return _index; }
            set { _index = value; }
        }

        #endregion




        public Item()
        {
            InitializeComponent();

            this.descripcion = "Nombre Generico de dia";
            this.documentacion = "no se ha hecho referencia a fecha";
        }

        public void Item_DoubleClick(object sender, EventArgs e)
        {
            guardar();          
        }

        public void guardar()
        {
            LibroDiario libroDiario = new LibroDiario(this);
            libroDiario.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            editarDatos();
        }

        public void editarDatos()
        {
             DialogoNombre dialogoNombre = new DialogoNombre(this);
            dialogoNombre.ShowDialog();
        }
    }
}
