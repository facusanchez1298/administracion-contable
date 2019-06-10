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
        #endregion




        public Item()
        {
            InitializeComponent();
        }

        private void Item_DoubleClick(object sender, EventArgs e)
        {
            LibroDiario libroDiario = new LibroDiario(this.ParentForm);
            libroDiario.Show();
            
        }
    }
}
