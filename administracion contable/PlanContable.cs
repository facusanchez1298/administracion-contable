using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace administracion_contable
{
    public partial class PlanContable : Form
    {
        //aca van a ir todos los elementos del plan contable antes de ir a la base de datos
        public List<ElementoPlanContable> elementosPlanContables { get; set; }
        private Form padre;//padre


        /// <summary>
        /// solo cambia cuando tocamos el boton buscar y encuentra algo
        /// </summary>
        string CodigoBuscado;

        //
        //
        //
        //
        //constructor
        //
        //
        //
        //
        
        public PlanContable(Form padre)
        {
            //evitamos que pueda cerrarse o maximizarse
            this.MaximizeBox = false;
            this.ControlBox = false;

            //guardamos la referencia al padre
            this.padre = padre;

            //inicializamos lista
            elementosPlanContables = new List<ElementoPlanContable>();

            //inicializamos los componentes
            InitializeComponent();

            //cargamos la lista con los datos de la base de datos y cargamos la tabla
            actualizar();

            //seleccionamos el primer elemento
            this.comboBoxTipo.SelectedIndex = 0;

            dataGridView1.RowHeadersVisible = false;
        }

        //
        //
        //
        //
        //eventos
        //
        //
        //
        //

        private void buttonCancelar_Click(object sender, EventArgs e)
        {            
            this.Close(); //cierra esta ventana
            padre.Show(); //muesta la ventana que la llamo
            padre.WindowState = FormWindowState.Maximized; //maximiza al padre, sin esto la otra ventana aparece minimizada
            padre.WindowState = FormWindowState.Normal; //muestra al padre en su estado normal
        }

        private void comboBoxTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            darPrimerNumero();  //cuando se cambie el item se cambia el numero  

        }

        private void guardarEnBaseDeDatos(object sender, EventArgs e)
        {
            if (!elementosVacios())
            {
                String nombre = textNombre.Text; //sacamos los datos de nombre y los guardamos
                if (esNumeral(this.textNumero.Text))
                {
                    string numeroIdentificador = this.textId.Text + this.textNumero.Text;
                    if (!existe(numeroIdentificador))
                    {
                        bool registrable = this.checkBox1.Checked;                        
                        this.guardarEnBase(registrable,numeroIdentificador, nombre);//guardo en base de datos
                        limpiarCampos();
                        mostrarMensajeGuardado("el elemento contable ha sido guardado con exito");
                        cargarLista();//cargo en la lista los datos de la base de datos
                        cargarTabla(this.elementosPlanContables); //muestro los datos de la lista en el dataGrid
                    }
                    else this.mostrarMensajeError("el numero ingresado ya existe");
                }
                else this.mostrarMensajeError("la cadena ingresada en el campo numero no es numerica. por favor no ingrese letras ni simbolos");

            }
            else this.mostrarMensajeError("hay elementos vacios, completelos y vuelva a intentar");
        }

        private void getNextCtrl(object sender, KeyEventArgs e)
        {
            
            
            Control ctr = (Control)sender;
            if (e.KeyCode == Keys.Enter)
            {
                if (ctr.Text != "")
                {
                    e.SuppressKeyPress = true;
                    ctr.Parent.SelectNextControl(ctr, true, true, true, true);
                }
            }
        }

        private void enterEnGuardar(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Space))
            {
                this.guardarEnBaseDeDatos(sender, e);
            }
        }

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            string numero = this.textBoxIndex.Text;
            if (esNumeral(numero))
            {
                this.CodigoBuscado = numero;
                // hay que verificar si existe
                ElementoPlanContable elemento = elementosPlanContables.Find(q => q.numeroIdentificador == numero);

                if (elemento != null)
                {
                    Conexion conexion = new Conexion();
                    elemento = conexion.buscarElementoPlanContable(numero);

                    cargarElementoEnCampos(elemento);
                    habilitarAgregar(true);
                    this.groupBox.Enabled = true;
                    this.buttonBorrar.Enabled = true;

                    if (conexion.yaTieneAsiento(CodigoBuscado)) this.checkBox1.Enabled = false;//no dejo que lo editen
                    
                   
                }
                else mostrarMensajeError("el indice ingresado no existe");
            }
            else mostrarMensajeError("El indice ingresado no es numeral.");
        }

        private void buttomGuardar_click(object sender, EventArgs e)
        {
            if (!elementosVacios())
            {
                string nombre = this.textNombre.Text;
                string numero = this.textId.Text + this.textNumero.Text;
                bool registrable = this.checkBox1.Checked;

                Conexion conexion = new Conexion();
                conexion.modificarElementoPlanContable(this.CodigoBuscado, registrable, numero, nombre);
                mostrarMensajeGuardado("el elemento se modifico correctamente");

                //refrescamos la tabla
                actualizar();
                this.habilitarEdicion(true);
                this.groupBox.Enabled = false;
                this.buttonBorrar.Enabled = false;
                habilitarAgregar(false);
                this.textBoxIndex.Text = "";
            }
            else mostrarMensajeError("complete todos los campos antes de guardar");

        }

        private void buttonCancelar_Click_1(object sender, EventArgs e)
        {
            habilitarAgregar(false);
            habilitarEdicion(true);
        }

        private void buttonBorrar_Click(object sender, EventArgs e)
        {
            Conexion conexion = new Conexion();
           
            if (!conexion.yaTieneAsiento(CodigoBuscado))
            {
                string respuesta = mostrarMensajeConfirmacion("¿Esta seguro que desea eliminar el elemento codigo: " + CodigoBuscado + ". de forma permanente?");

                if (respuesta.Equals("OK"))
                {
                    conexion.borrarElementoPlanContable(CodigoBuscado);
                    actualizar();
                    mostrarMensajeGuardado("Elemento eliminado con exito");
                    this.habilitarEdicion(true);
                    this.groupBox.Enabled = false;
                    this.buttonBorrar.Enabled = false;
                    habilitarAgregar(false);
                    this.textBoxIndex.Text = "";
                }
            }
            else mostrarMensajeError("El codigo no se puede borrar por que ya tiene asiento en el libro diario");
        }

        //
        //
        //
        //
        //eventos del menu
        //
        //
        //
        //

        private void abrirLibroMayor(object sender, EventArgs e)
        {
            LibroMayor elementoContable = new LibroMayor(this.padre);
            elementoContable.Show();
            this.Hide();
        }
        
        private void Modoeditar(object sender, EventArgs e)
        {
            
            if (elementosPlanContables.Any())
            {
                habilitarAgregar(false);
                habilitarEdicion(true);
            }
            else mostrarMensajeError("debe haber al menos un elemento para editar");

        }

        private void modoAgregar(object sender, EventArgs e)
        {
            habilitarAgregar(true);
            habilitarEdicion(false);
        }
        
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close(); //cierra esta ventana
            padre.Show(); //muesta la ventana que la llamo
            padre.WindowState = FormWindowState.Maximized; //maximiza al padre, sin esto la otra ventana aparece minimizada
            padre.WindowState = FormWindowState.Normal; //muestra al padre en su estado normal
        }

        private void menu_abirLibroDiario(object sender, EventArgs e)
        {
            Conexion conexion = new Conexion();
            if (!conexion.hayFechaLimite())
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

        //
        //
        //
        //
        //comprovaciones de seguridad
        //
        //
        //
        //

        public bool esNumeral(string cadena)
        {
            try
            {
                int.Parse(cadena);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool existe(String cadena)
        {
            for (int i = 0; i < elementosPlanContables.Count; i++)
            {
                if (elementosPlanContables.ElementAt(i).numeroIdentificador.Equals(cadena)) return true;
            }

            return false;
        }

        public bool elementosVacios()
        {
            bool nombreEstaVacio = String.IsNullOrEmpty(textNombre.Text);
            bool numeroEstaVacio = string.IsNullOrEmpty(this.textNumero.Text);
            bool tipoEstaVacio = string.IsNullOrEmpty(this.comboBoxTipo.Text);

            //si nombre o numero estan vacios devuelve true
            if ((nombreEstaVacio) || (numeroEstaVacio) || (tipoEstaVacio)) return true;
            return false;
        }

        //
        //
        //
        //
        //mensajes
        //
        //
        //
        //

        public String mostrarMensajeConfirmacion(string mensaje = "Esta seguro que desea continuar?")
        {
            /// <summary>
            /// muestra una pregunta y retorna ok o cancel depende lo que selecciono el usuario
            /// </summary>
            /// <param name="mensaje"></param>
            /// <returns></returns>
            const string caption = "Datos Erroneos";
            var result = MessageBox.Show(mensaje, caption,
                                         MessageBoxButtons.OKCancel,
                                         MessageBoxIcon.Warning);

            return result.ToString();//puede ser ok o cancel
        }
        
        public void mostrarMensajeError(string mensaje)
        {
            const string caption = "Datos Erroneos";
            var result = MessageBox.Show(mensaje, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
        }

        public void mostrarMensajeGuardado(String mensaje)
        {
            //muestra el mensaje que se le paso por atributo
            const string caption = "Datos guardados";
            var result = MessageBox.Show(mensaje, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        //
        //
        //
        //
        //
        //cosas con la base de datos
        //
        //
        //
        //

        private void guardarEnBase(bool registrable, string numeroIdentificador, string nombre)
        {
            //guarda elemento en la base de datos
            Conexion conexion = new Conexion();
            conexion.guardarElementoContable(registrable,numeroIdentificador, nombre);//guardo elemento en la base de datos
        }

        public void cargarLista()
        {
            //carga la lista con los elementos de la base de datos
            Conexion conexion = new Conexion();
            conexion.cargarElementoContable(this.elementosPlanContables);
        }

        //
        //
        //
        //
        //trabajos con campos
        //
        //
        //
        //

        public void darPrimerNumero()
        {
            string valorComboBox = (comboBoxTipo.SelectedIndex + 1).ToString();
            this.textId.Text = valorComboBox;
        }
        
        public void limpiarCampos()
        {
            //vacia los campos
            this.textNumero.Text = "";
            this.textNombre.Text = "";
        }

        public void cargarTabla(List<ElementoPlanContable> planContables)
        {
            this.elementosPlanContables = planContables;

            
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("numero", typeof(String));
            dataTable.Columns.Add("nombre", typeof(String));
            dataTable.Columns.Add("registrable", typeof(string));

            for (int i = 0; i < this.elementosPlanContables.Count; i++) //recorremos toda la lista
            {
                bool registrable = elementosPlanContables.ElementAt(i).registrable;          
                string nombre = elementosPlanContables.ElementAt(i).nombre;
                string id = elementosPlanContables.ElementAt(i).numeroIdentificador;
                dataTable.Rows.Add(id, nombre,registrable.ToString()); //generamos un registro con los datos que sacamos de la lista
            }


            dataGridView1.DataSource = dataTable; //pasamos los datos al data table
            //dataGridView1.AutoResizeColumns(); //ponemos que el ancho de la columna se pueda cambiar
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //ponemos que se adapten al contenedor
        }

        public void habilitarAgregar(bool habilitacion)
        {
            this.textNombre.Enabled = habilitacion;
            this.comboBoxTipo.Enabled = habilitacion;
            this.textNumero.Enabled = habilitacion;
            this.textId.Enabled = habilitacion;
            this.checkBox1.Enabled = habilitacion;
        }

        public void habilitarEdicion(bool habilitacion)
        {
            this.groupBox.Enabled = false;
            this.groupBox.Visible = habilitacion;
            this.buttonBuscar.Enabled = habilitacion;
            this.textBoxIndex.Enabled = habilitacion;
            this.buttonBorrar.Enabled = false;
            this.buttonBorrar.Visible = habilitacion;

            this.buttonAgregar.Visible = !habilitacion;

        }

        public void cargarElementoEnCampos(ElementoPlanContable elemento)
        {
            Conexion conexion = new Conexion();

            this.textNombre.Text = elemento.nombre;
            this.comboBoxTipo.SelectedIndex = int.Parse(elemento.numeroIdentificador.ElementAt(0).ToString()) - 1;
            this.textNumero.Text = elemento.numeroIdentificador.Remove(0, 1);
            this.checkBox1.Checked = elemento.registrable;
             
            
        }
               
        public void refrescarTabla()
        {
            actualizar();
        }
                       
        public void actualizar()
        {
            Conexion conexion = new Conexion();
            conexion.cargarElementoContable(this.elementosPlanContables);
            cargarTabla(elementosPlanContables);
        }

        
    }
}
