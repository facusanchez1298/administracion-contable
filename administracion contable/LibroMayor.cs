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
    public partial class LibroMayor : Form
    {
        //atributos
        public List<ElementoLibroDiario> elementoLibroDiario { get; private set; }
        Form padre;
        Conexion conexion = new Conexion();




        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="padre"></param>
        public LibroMayor(Form padre)
        {
            
            this.padre = padre;

            //evitamos que pueda cerrarse o maximizarse
            this.MaximizeBox = false;
            this.ControlBox = false;

            InitializeComponent();

            IniciarComponestes();
           
        }


        //metodos
        public void IniciarComponestes()
        {
            dataGridView1.RowHeadersVisible = false;

            elementoLibroDiario = new List<ElementoLibroDiario>();

            Conexion.cargarComboBox(this.comboBoxTipo);
            if (comboBoxTipo.Items.Count > 0)
            {
                this.comboBoxTipo.SelectedIndex = 0;
            }

            mostrarTodo();



        }

        public void cargarTabla(List<ElementoLibroDiario> elementosTabla)
        {
          


            DataTable dataTable = new DataTable();

            //dataTable.Columns.Add("Codigo", typeof(String));
            dataTable.Columns.Add("Fecha", typeof(String));
            //dataTable.Columns.Add("Nombre", typeof(String));
            dataTable.Columns.Add("Debe", typeof(String));
            dataTable.Columns.Add("Haber", typeof(String));
            dataTable.Columns.Add("balance", typeof(float));
            dataTable.Columns.Add("Documentacion", typeof(string));
            float debe = 0;
            float haber = 0;
            float total = 0;

            if(elementosTabla.Any()) dataTable.Rows.Add("cuenta: " + elementosTabla.ElementAt(0).nombre, elementosTabla.ElementAt(0).codigo,  "", null, "");

            for (int i = 0; i < elementosTabla.Count; i++) //recorremos toda la lista
            {
                string nombre = elementosTabla.ElementAt(i).nombre;
                string codigo = elementosTabla.ElementAt(i).codigo;
                string fecha = elementosTabla.ElementAt(i).fecha.Date.ToString("dd/MM/yyyy");
                string monto = elementosTabla.ElementAt(i).monto;
                string transaccion = elementosTabla.ElementAt(i).transaccion;
                string documentacion = elementosTabla.ElementAt(i).documentacionRespaldatoria;

                string dia = elementosTabla.ElementAt(i).folio.ToString();

                if (transaccion.Equals("HABER"))
                {
                    total += float.Parse(monto);
                    haber += float.Parse(monto);
                    dataTable.Rows.Add(fecha, "", monto, total, documentacion); //generamos un registro con los datos que sacamos de la lista
                }
                else
                {
                    total -= float.Parse(monto);
                    debe += float.Parse(monto);
                    dataTable.Rows.Add(fecha, monto, "", total, documentacion); //generamos un registro con los datos que sacamos de la lista
                }

                if (i + 1 <= elementosTabla.Count - 1)
                {
                    if (codigo != elementosTabla.ElementAt(i + 1).codigo)
                    {
                        dataTable.Rows.Add( "TOTAL", debe, haber, total, "");
                        dataTable.Rows.Add(dataTable.NewRow());

                        debe = 0;
                        haber = 0;
                        total = 0;
                    }
                }
                else if (i + 1 > elementosTabla.Count - 1)
                {
                    dataTable.Rows.Add("TOTAL", debe, haber, total, "");
                    dataTable.Rows.Add(dataTable.NewRow());

                    debe = 0;
                    haber = 0;
                    total = 0;
                }
            }

            dataGridView1.DataSource = dataTable; //pasamos los datos al data table
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //ponemos que se adapten al contenedor


        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            volverAVentanaAnterior();
        }


        public void volverAVentanaAnterior()
        {
            this.Close(); //cierra esta ventana
            padre.Show(); //muesta la ventana que la llamo
            padre.WindowState = FormWindowState.Maximized; //maximiza al padre, sin esto la otra ventana aparece minimizada
            padre.WindowState = FormWindowState.Normal; //muestra al padre en su estado normal
        }

        private void darFormato(string texto)
        {
            if (texto != null)
            {
                double flotante;// variable float
                double.TryParse(texto, out flotante);
                if (!texto.Contains(".")) texto = flotante.ToString("0.00");
            }
        }

        public void mostrarMensaje(string mensaje)
        {
            const string caption = "Datos Erroneos";
            var result = MessageBox.Show(mensaje, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            PlanContable elementoContable = new PlanContable(this.padre);
            elementoContable.Show();
            this.Hide();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            if (!this.conexion.hayFechaLimite())
            {
                FechaMaxMin fecha = new FechaMaxMin(this.padre);
                fecha.Show();
                this.Hide();
            }
            else
            {
                LibroDiario elementoContable = new LibroDiario(this.padre);
                elementoContable.Show();
                this.Hide();
            }
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            string texto = darId(comboBoxTipo.Text);
            int codigo = comboBoxTipo.FindString(texto);

            if (texto.Any())
            {
                if (codigo != -1)
                {
                    this.conexion.buscarPorCodigo(texto, this.elementoLibroDiario);
                    cargarTabla(elementoLibroDiario);
                }
                else mostrarMensaje("Lo siento, el codigo ingresado no existe");
            }
            else mostrarMensaje("No ha ingresado ni seleccionado un codigo para buscar");
        }

        public void mostrarTodo()
        {
            this.conexion.mostrarLibroTotal(elementoLibroDiario);
            cargarTabla(elementoLibroDiario);
        }

        public string darId(string cadena)
        {
            //busca solo los caracteres numericos
            string nuevo = ""; //cadena que se retorna          
            int numero; //guarda el resultado de tryParse, no es importante
            string caracter; //el caracter que vamos a controlar lo cargamos aca 

            for (int i = 0; i < cadena.Length; i++)
            {
                caracter = cadena.ElementAt(i).ToString();

                if (int.TryParse(caracter, out numero))
                {
                    nuevo = nuevo + cadena.ElementAt(i);
                }
            }

            return nuevo;
        }

        private void buttonTodo_Click(object sender, EventArgs e)
        {
            mostrarTodo();
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;

            if (dgv.Columns[e.ColumnIndex].Name == "Total")  //Si es la columna a evaluar
            {
                if (!e.Value.ToString().Equals(""))
                {
                    if (float.Parse(e.Value.ToString()) < 0)   //Si el valor de la celda contiene la palabra hora
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                    else e.CellStyle.ForeColor = Color.Green;

                }
            }

            
        }
    }
}
