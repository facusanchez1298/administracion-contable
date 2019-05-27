using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace administracion_contable
{
    public partial class LibroDiario : Form
    {
        /// <summary>
        /// form al cual va a volver cuando toques salir
        /// </summary>
        public Form padre;

        /// <summary>
        /// aca cargamos los registros del libro diario actual
        /// </summary>
        List<ElementoLibroDiario> elementosLibroDiario; //dia actual

        /// <summary>
        /// aca cargamos los registros de libros diarios anteriores
        /// </summary>
        List<ElementoLibroDiario> lista = new List<ElementoLibroDiario>();//dias anteriores

        /// <summary>
        /// indice que buscamos
        /// </summary>
        int index;

        /// <summary>
        /// objeto que se encarga de la comunicacion con la base de datos
        /// </summary>
        Conexion conexion = new Conexion();
        
        //
        //
        //
        //
        //          constructor
        //
        //
        //
        //

        public LibroDiario(Form padre)
        {
            //guardamos la referencia al padre
            this.padre = padre;

            //inicio componentes
            InitializeComponent();


            //evitamos que pueda cerrarse o maximizarse
            this.MaximizeBox = false;
            this.ControlBox = false;

            //cargamos el comboBox desde la base de datos
            Conexion.cargarComboBox(comboBox1);

            //inicializamos lista
            this.elementosLibroDiario = new List<ElementoLibroDiario>();


            if (this.conexion.hayDatosNoAsentado()) continuar();

            this.buttonAgregar.Show();//mostramos boton agregar
            this.groupBox.Hide();//escondemos los botones de edicion
            this.buttonBorrar.Hide();//escondemos el boton de borrar


            //definimos el rango maximo de las fechas
            conexion.asignarRango(this.dateTimePicker1); 

            iniciarElementos();
        }

        //
        //
        //
        //
        //          controles de seguridad
        //
        //
        //
        //

        /// <summary>
        /// verifica si hay campos sin completar
        /// </summary>
        /// <returns></returns>
        public bool hayCamposVacios()
        {
            bool nombreEstaVacio = this.textnombre.Text.Any();

            bool montoEstaVacio = this.textBoxMonto.Text.Any();

            if ((!nombreEstaVacio) || (!montoEstaVacio))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// verifica si el texto ingresado es un numero
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public bool esDecimal(string numero)
        {
            try
            {
                double a = double.Parse(numero);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// verifica si el numero ingresado es positivo
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>
        public bool esPositivo(string numero)
        {
            return decimal.Parse(numero) > 0;
        }

        /// <summary>
        /// verifica que en el debe y en el haber haya la misma cantidad
        /// </summary>
        /// <param name="elementoLibroDiarios"></param>
        /// <returns></returns>
        public bool estaBalanceado(List<ElementoLibroDiario> elementoLibroDiarios)
        {
            float debe = 0;
            float haber = 0;
            string texto;

            for (int i = 0; i < elementoLibroDiarios.Count; i++)
            {
                texto = elementoLibroDiarios.ElementAt(i).transaccion;
                if (texto.Equals("DEBE"))
                {
                    debe += float.Parse(elementoLibroDiarios.ElementAt(i).monto);
                }
                else haber += float.Parse(elementoLibroDiarios.ElementAt(i).monto);
            }

            return debe == haber;
        }

        /// <summary>
        /// verifica que haya al menos un registro en el debe
        /// </summary>
        /// <param name="elemento"></param>
        /// <returns></returns>
        public bool tieneDebe(List<ElementoLibroDiario> elemento)
        {
            for (int i = 0; i < elemento.Count; i++)
            {
                if (elemento.ElementAt(i).transaccion.Equals("DEBE"))
                {
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// verifica que haya al menos un registro en el haber
        /// </summary>
        /// <param name="elemento"></param>
        /// <returns></returns>
        public bool tieneHaber(List<ElementoLibroDiario> elemento)
        {
            for (int i = 0; i < elemento.Count; i++)
            {
                if (elemento.ElementAt(i).transaccion.Equals("HABER"))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// verifica que haya al menos un elemento en el debe y uno en el haber
        /// </summary>
        /// <param name="elementos"></param>
        /// <returns></returns>
        public bool tieneDosElementos(List<ElementoLibroDiario> elementos)
        {
            if (elementos.Any())
            {
                if (elementos.Count >= 2)
                {
                    if ((tieneDebe(elementos)) && (tieneHaber(elementos)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// verifica que el codigo ingresado exista
        /// </summary>
        /// <returns></returns>
        public bool codigoEsCorrecto()
        {
            string numero = this.comboBox1.Text;//texto actual
            int index = this.comboBox1.FindString(numero); //tiene coinsidencia con otro?

            return (index != -1) ? true : false;
        }
                       
        //
        //
        //
        //
        //          mensajes
        //
        //
        //
        //

        /// <summary>
        /// muestra un mensaje de error
        /// </summary>
        /// <param name="mensaje">mensaje que se va a mostrar</param>
        public void mostrarMensajeError(string mensaje)
        {
            const string caption = "Datos Erroneos";
            var result = MessageBox.Show(mensaje, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
        }

        /// <summary>
        /// mostrar mensaje de guardado
        /// </summary>
        /// <param name="mensaje">el mensaje que vamos a mostrar</param>
        public void mostrarMensajeGuardado(String mensaje)
        {
            //muestra el mensaje que se le paso por atributo
            const string caption = "Datos guardados";
            var result = MessageBox.Show(mensaje, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        /// <summary>
        /// hace una pregunta al usuario para confirmar su decision
        /// </summary>
        /// <param name="mensaje"></param>
        /// <returns>retorna OK si el usuario acepta, cancel si no acepta</returns>
        public String mostrarMensajeConfirmacion(string mensaje)
        {  
            const string caption = "Datos Erroneos";
            var result = MessageBox.Show(mensaje, caption,
                                         MessageBoxButtons.OKCancel,
                                         MessageBoxIcon.Warning);

            return result.ToString();//puede ser ok o cancel
        }

        /// <summary>
        /// crea un cartel donde se puede ingresar texto
        /// </summary>
        /// <param name="pregunta"></param>
        /// <returns>el texto ingresado por el usuario</returns>
        public string preguntarNombre(string pregunta)
        {
            string nombre = Interaction.InputBox(
            pregunta,
            "titulo",
            "respuesta por defecto");

            return nombre;
        }

        /// <summary>
        /// realiza una pregunta al usuario antes de continuar
        /// </summary>
        /// <param name="pregunta"></param>
        /// <returns>retorna true si el usuario acepta y false si no</returns>
        public bool estaSeguro(string pregunta)
        {
            string confirmacion = mostrarMensajeConfirmacion(pregunta);

            if (confirmacion.Equals("OK"))
            {
                return true;
            }
            return false;
        }

        //
        //
        //
        //
        //          eventos
        //
        //
        //
        //

        private void textBoxMonto_TextChanged(object sender, EventArgs e)
        {
            if (esDecimal(this.Text)) decimal.Parse(this.Text);
        }

        private void seleccion(object sender, MouseEventArgs e)
        {
            if (this.textBoxMonto.Text.Equals("0.00")) this.textBoxMonto.Text = "";
        }

        private void textBoxMonto_Click(object sender, EventArgs e)
        {
            if (this.textBoxMonto.Text.Equals("0.00")) this.textBoxMonto.Text = "";
        }

        private void textBoxMonto_Leave(object sender, EventArgs e)
        {

            string texto = this.textBoxMonto.Text;

            if (texto != null)
            {
                double flotante;// variable float
                double.TryParse(texto, out flotante);
                if (!texto.Contains(".")) this.textBoxMonto.Text = flotante.ToString("0.00");
            }
        }

        private void buttonAgregar_Click(object sender, EventArgs e)
        {
            agregarElemento();            
        }

        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            this.Close(); //cierra esta ventana
            padre.Show(); //muesta la ventana que la llamo
            padre.WindowState = FormWindowState.Maximized; //maximiza al padre, sin esto la otra ventana aparece minimizada
            padre.WindowState = FormWindowState.Normal; //muestra al padre en su estado normal
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = this.comboBox1.Text;
            this.textnombre.Text = darNombre(id);            
        }

        private void buttonGuardar_Click(object sender, EventArgs e)
        {
            if (tieneDosElementos(elementosLibroDiario))
            {
                if (estaBalanceado(elementosLibroDiario))
                {
                    if (mostrarMensajeConfirmacion("esta seguro que quiere guardar el libro diario?").Equals("OK"))
                    {

                        mostrarMensajeGuardado("libro diario guardado");
                    }
                }
                else mostrarMensajeError("libro diario no esta valanceado");
            }
            else mostrarMensajeError("necesita al menos un elemento en el debe y uno en el haber");
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

        private void buttonBuscar_Click(object sender, EventArgs e)
        {
            if (this.comboBoxDebeHaber.Enabled == false)
            {
                if (this.textBoxEditar.Text.Any())
                {
                    if (this.esDecimal(this.textBoxEditar.Text))
                    {
                        this.index = int.Parse(this.textBoxEditar.Text) - 1;
                        int dia = int.Parse(labelDia.Text);
                        if(dia == conexion.numeroDelDia())
                        {
                            if ((this.index < elementosLibroDiario.Count) && (this.index >= 0))
                            {
                                ElementoLibroDiario elemento = elementosLibroDiario.ElementAt(this.index);
                                cambiarHabilitacionAgregar(true); //si encuentra el indice habilita la edicion en los campos

                                montarEnCampos(elemento); //luego carga el elemento en los campos
                                this.groupBox.Show(); //muestra botones para guardar o cancelar
                                this.buttonBorrar.Show();//muestra el boton de borrar
                            }
                            else
                            {
                                mostrarMensajeError("el indice indicado no existe");
                                this.groupBox.Hide();
                                this.buttonBorrar.Hide();
                            }
                        }
                        else
                        {
                            if ((this.index < lista.Count) && (this.index >= 0))
                            {
                                ElementoLibroDiario elemento = lista.ElementAt(this.index);
                                cambiarHabilitacionAgregar(true); //si encuentra el indice habilita la edicion en los campos

                                montarEnCampos(elemento); //luego carga el elemento en los campos
                                this.groupBox.Show(); //muestra botones para guardar o cancelar
                                this.buttonBorrar.Show();//muestra el boton de borrar
                            }
                            else
                            {
                                mostrarMensajeError("el indice indicado no existe");
                                this.groupBox.Hide();
                                this.buttonBorrar.Hide();
                            }
                        }
                       
                    }
                    else mostrarMensajeError("debe ingresar un numero");
                }
                else mostrarMensajeError("debe ingresar el index que desea editar");
            }
            else
            {
                string decision = mostrarMensajeConfirmacion("seguro que quiere buscar sin guardar los cambios?");
                if (decision.Equals("OK"))
                {
                    cambiarHabilitacionAgregar(false); //deshabilito los controles de agregar
                    buttonBuscar_Click(sender, e);//llamo devuelta a la funcion
                }
            }
        }
                
        private void comboBox1_Leave(object sender, EventArgs e)
        {
            if (codigoEsCorrecto())
            {
                string text = this.comboBox1.Text;
                this.comboBox1.SelectedIndex = this.comboBox1.FindString(text);
            }
        }

        private void BuscarConEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int index = int.Parse(this.textBoxEditar.Text) - 1;
                if ((index < elementosLibroDiario.Count) && (index > -1))
                {
                    ElementoLibroDiario elemento = elementosLibroDiario.ElementAt(index);
                    this.comboBox1.SelectedIndex = this.comboBox1.FindString(elemento.codigo); //editamos el codigo

                    this.textBoxMonto.Text = elemento.monto;//editamos el monto
                    this.dateTimePicker1.Value = elemento.fecha; //editamos la fecha

                    this.comboBoxDebeHaber.SelectedIndex = (elemento.transaccion.Equals("DEBE")) ? 1 : 0;
                }
            }
        }

        private void buttonGuardarCambios_Click(object sender, EventArgs e)
        {
            if (editarElemento(index))
            {
                cambiarHabilitacionAgregar(false); //deshabilito los controles de agregar
                this.groupBox.Visible = false;
                this.buttonBorrar.Visible = false;               
            }
        }

        private void buttonCancelar_Click_1(object sender, EventArgs e)
        {

            cambiarHabilitacionAgregar(false); //deshabilito los controles de agregar
            this.groupBox.Visible = false;            
            this.buttonBorrar.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_MouseHover(object sender, EventArgs e)
        {
            this.checkedListBox1.Size = new System.Drawing.Size(146, 169);
        }

        private void checkedListBox1_MouseLeave(object sender, EventArgs e)
        {
            this.checkedListBox1.Size = new System.Drawing.Size(146, 19);
        }

        private void buttonBorrar_Click(object sender, EventArgs e)
        {
            string respuesta = mostrarMensajeConfirmacion("¿Esta seguro que desea eliminar este registro de forma permanente?");
            int dia = int.Parse(this.labelDia.Text);

            if (respuesta.Equals("OK")) this.conexion.borrarElementoLibroDiario(this.index, dia);

            if (dia == conexion.numeroDelDia()) actualizar(elementosLibroDiario);
            else actualizar(lista);
            

            cambiarHabilitacionAgregar(false); //deshabilito los controles de agregar
            this.groupBox.Visible = false;
            this.buttonBorrar.Visible = false;
        }

        private void checkedListBox1_Enter(object sender, EventArgs e)
        {
            checkedListBox1_MouseHover(sender, e);
        }

        private void checkedListBox1_Leave(object sender, EventArgs e)
        {
            checkedListBox1_MouseLeave(sender, e);
        }
        
        //
        //
        //
        //
        //          eventos del menu
        //
        //
        //
        //

        private void menu_asentarLibroDiario(object sender, EventArgs e)
        { int diaEditado = int.Parse(labelDia.Text);
            if ((tieneDosElementos(elementosLibroDiario) && conexion.numeroDelDia() == diaEditado) || (conexion.numeroDelDia() != diaEditado && tieneDosElementos(lista)))
            {
                if ((estaBalanceado(elementosLibroDiario) && conexion.numeroDelDia() == diaEditado) || (conexion.numeroDelDia() != diaEditado && estaBalanceado(lista)))
                {
                    if (mostrarMensajeConfirmacion("esta seguro que quiere guardar el libro diario?").Equals("OK"))
                    {
                        if (int.Parse(this.labelDia.Text) == conexion.numeroDelDia())
                        {
                            
                            this.conexion.asentarLibroDiario(labelDia.Text);

                            this.labelDia.Text = this.conexion.numeroDelDia().ToString(); //cambiamos el numero de folio
                            

                            elementosLibroDiario.Clear(); //limpiamos la lista
                            cargarTabla(elementosLibroDiario); //la lista vacia la "cargamos" en la tabla
                        }
                        else
                        {
                            asentarCambios(int.Parse(this.labelDia.Text));
                            lista.Clear();
                        }
                        mostrarMensajeGuardado("libro diario guardado");
                        selector.Enabled = true;
                        cambiarHabilitacionAgregar(false);
                    }
                }
                else mostrarMensajeError("libro diario no esta valanceado");
            }
            else mostrarMensajeError("necesita al menos un elemento en el debe y uno en el haber");           
        }

        [Obsolete("metodo en desuso", true)]
        private void menu_guardar(object sender, EventArgs e)
        {
            if (tieneDosElementos(elementosLibroDiario))
            {
                if (estaBalanceado(elementosLibroDiario))
                {
                    if (mostrarMensajeConfirmacion("esta seguro que quiere guardar el libro diario?").Equals("OK"))
                    {
                        
                        
                        for (int i = 0; i < elementosLibroDiario.Count; i++)
                        {
                            ElementoLibroDiario elemento = elementosLibroDiario.ElementAt(i);
                            this.conexion.guardarLibroDiario(elemento);
                        }
                    }
                }
                else mostrarMensajeError("libro diario no esta valanceado");
            }
            else mostrarMensajeError("necesita al menos un elemento en el debe y uno en el haber");
        }

        private void menu_salir(object sender, EventArgs e)
        {
            if (elementosLibroDiario.Any())
            {
                if (estaSeguro("esta seguro que desea salir sin asentar el libro diario? los datos no guardados se perdera"))
                {
                    conexion.borrarNoAsentado();
                    volverAVentanaAnterior();
                }
            }
            else volverAVentanaAnterior();

        }

        private void menu_nuevo(object sender, EventArgs e)
        {
            if (this.elementosLibroDiario.Any())
            {
                if (estaSeguro("esta seguro que quiere salir sin guardar? no habra forma de recuperar los datos"))
                {
                    this.conexion.borrarNoAsentado();
                    this.elementosLibroDiario.Clear();
                    cargarTabla(elementosLibroDiario);
                }
            }
        }

        private void menu_editar(object sender, EventArgs e)
        {
            menuAgregarElemento.Enabled = true;
            menuEditarElemento.Enabled = false;

            int dia = int.Parse(labelDia.Text);
            if ((elementosLibroDiario.Any() && dia == conexion.numeroDelDia()) || ( lista.Any() && dia != conexion.numeroDelDia()))
            {
                selector.Enabled = false;
                cambiarHabilitacionAgregar(false);
                cambiarHabilitacionEdicion(true);
                this.buttonAgregar.Hide();
            }
            else mostrarMensajeError("aun no hay elementos para editar");

        }

        private void menu_agregarElementos(object sender, EventArgs e)
        {
            this.menuAgregarElemento.Enabled = false;
            this.menuEditarElemento.Enabled = true;
            this.groupBox.Hide();
            this.buttonBorrar.Hide();
            this.buttonAgregar.Show();

            //habilitamos los campos para agregar y deshabilitamos los de edicion
            cambiarHabilitacionAgregar(true);
            cambiarHabilitacionEdicion(false);

            //si no lo hago asi no funciona
            this.comboBoxDebeHaber.SelectedIndex = 1;
            this.comboBox1.SelectedIndex = 0;
            this.comboBox1.SelectedIndex = 0;
            this.textBoxMonto.Text = "0.00";
            


        }

        private void menu_abrirLibroMayor(object sender, EventArgs e)
        {
            LibroMayor elementoContable = new LibroMayor(this.padre);
            elementoContable.Show();
            this.Hide();
        }
        
        [Obsolete("metodo en desuso", true)]
        private void menu_verTodo(object sender, EventArgs e)
        {
            List<ElementoLibroDiario> elementos = new List<ElementoLibroDiario>();
            
            this.conexion.mostrarLibroTotal(elementos);
            cargarTabla(elementos);
            this.labelDia.Text = "---";
        }

        private void menu_verDiaActual(object sender, EventArgs e)
        {
            
            labelDia.Text = this.conexion.numeroDelDia().ToString();
            cargarTabla(elementosLibroDiario);       
            cambiarHabilitacionAgregar(true);
            cargarSelector();
            

        }

        private void menu_abrirPlanContable(object sender, EventArgs e)
        {
            PlanContable elementoContable = new PlanContable(this.padre);
            elementoContable.Show();
            this.Hide();
        }

        private void menu_verDiasAnteriores(object sender, EventArgs e)
        {
            this.selector.Show();
            this.selector.Enabled = true;
            cambiarHabilitacionAgregar(false);
            cambiarHabilitacionEdicion(false);
        }
       
        //
        //
        //
        //
        //          trabajo con campos
        //
        //
        //
        //
        
        /// <summary>
        /// inicializa los elementos que colocamos
        /// </summary>
        public void iniciarElementos()
        {
            dataGridView1.RowHeadersVisible = false;

            //si el comboBox no esta vacio seleccionamos el primer elemento
            if ((this.comboBox1.SelectedItem == null) && (this.comboBox1.Items.Count > 0))
            {
                this.comboBox1.SelectedIndex = 0; //ponemos el indice
                string id = this.comboBox1.Text; //cargamos la referencia en una variable
                this.textnombre.Text = darNombre(id); //cambiamos el texto de el text nombre
                                                      // this.textBoxGrupo.Text = darGrupo(id); //cambiamos el texto a grupo
            }

            //ocultamos el selector de dias
            //selector.Hide();

            //seleccionamos index del debe/haber
            this.comboBoxDebeHaber.SelectedIndex = 0;

            //cargamos la tabla
            cargarTabla(this.elementosLibroDiario);

            //cargamos valores del total
            totales(elementosLibroDiario);

            //monto = 0.00
            this.textBoxMonto.Text = "0.00";

            //numero de libro
            this.labelDia.Text = this.conexion.numeroDelDia().ToString();


            //cargamos libro
            cargarSelector();
        }

        /// <summary>
        /// carga la lista ingresada en la tabla
        /// </summary>
        /// <param name="elementosTabla"></param>
        public void cargarTabla(List<ElementoLibroDiario> elementosTabla)
        {
            //this.elementosLibroDiario = planContables;


            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("index", typeof(int));
            dataTable.Columns.Add("codigo", typeof(String));
            dataTable.Columns.Add("fecha", typeof(String));
            dataTable.Columns.Add("nombre", typeof(String));
            dataTable.Columns.Add("debe", typeof(String));
            dataTable.Columns.Add("haber", typeof(String));
            dataTable.Columns.Add("documentacion", typeof(string));
            


            for (int i = 0; i < elementosTabla.Count; i++) //recorremos toda la lista
            {
               
                string nombre = elementosTabla.ElementAt(i).nombre;
                string id = elementosTabla.ElementAt(i).codigo;
                string fecha = elementosTabla.ElementAt(i).fecha.Date.ToString("dd/MM/yyyy");
                string monto = elementosTabla.ElementAt(i).monto;
                string transaccion = elementosTabla.ElementAt(i).transaccion;
                string documentacion = elementosTabla.ElementAt(i).documentacionRespaldatoria;

                string dia = elementosTabla.ElementAt(i).dia.ToString();

                if (transaccion.Equals("HABER"))
                {
                    dataTable.Rows.Add((i + 1), id, fecha, nombre, "", monto, documentacion); //generamos un registro con los datos que sacamos de la lista
                }
                else
                {
                    dataTable.Rows.Add((i + 1), id, fecha, nombre, monto, "", documentacion); //generamos un registro con los datos que sacamos de la lista
                }
            }

            dataGridView1.DataSource = dataTable; //pasamos los datos al data table
            //dataGridView1.AutoResizeColumns(); //ponemos que el ancho de la columna se pueda cambiar
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; //ponemos que se adapten al contenedor
            totales(this.elementosLibroDiario);//actualiza los totales 

        }
        
        /// <summary>
        /// no hace nada, es para pruebas
        /// </summary>
        public void guardarDateBase()
        {
            DateTime a = this.dateTimePicker1.Value; //tomo el valor del dateTimePicker
            DateTime b = DateTime.Parse(a.Date.ToString("dd/mm/yyyy")); // parseo a string y elijo el formato y lo guardo en dateTime
            this.dateTimePicker1.Value = b; //lo traigo devuelta y lo coloco en el value

            mostrarMensajeError(a.Date.ToString("dd/MM/yyyy"));
        }

        /// <summary>
        /// muestra el total del debe y del haber
        /// </summary>
        /// <param name="elemento">de donde va a sacar los datos</param>
        public void totales(List<ElementoLibroDiario> elemento)
        {
            float debe = 0;
            float haber = 0;

            if (elemento.Any())
            {
                for (int i = 0; i < elemento.Count; i++)
                {
                    if (elemento.ElementAt(i).transaccion.Equals("DEBE"))
                    {
                        debe += float.Parse(elemento.ElementAt(i).monto);
                    }
                    else haber += float.Parse(elemento.ElementAt(i).monto);
                }
            }

            this.textBoxTotalDebe.Text = debe.ToString("0.00");
            this.textBoxTotalHaber.Text = haber.ToString("0.00");
        }

        /// <summary>
        /// cierra la ventana actual y habre la ventana padre
        /// </summary>
        public void volverAVentanaAnterior()
        {
            this.Close(); //cierra esta ventana
            padre.Show(); //muesta la ventana que la llamo
            padre.WindowState = FormWindowState.Maximized; //maximiza al padre, sin esto la otra ventana aparece minimizada
            padre.WindowState = FormWindowState.Normal; //muestra al padre en su estado normal
        }

        /// <summary>
        /// habilida o desabilita los campos que sirven para agregar elementos
        /// </summary>
        /// <param name="habilitacion"></param>
        public void cambiarHabilitacionAgregar(bool habilitacion)
        {
            this.comboBox1.Enabled = habilitacion;
            this.textnombre.Enabled = habilitacion;
            this.dateTimePicker1.Enabled = habilitacion;
            this.textBoxMonto.Enabled = habilitacion;
            this.comboBoxDebeHaber.Enabled = habilitacion;
            //this.textBoxGrupo.Enabled = habilitacion;
            this.buttonAgregar.Enabled = habilitacion;
            this.checkedListBox1.Enabled = habilitacion;

            if (!habilitacion)
            {
                //vaciarCamposDeTexto();
            }
            else
            {
                this.comboBox1.SelectedIndex = 0;
                this.comboBoxDebeHaber.SelectedIndex = 0;
            }
        }

        /// <summary>
        ///  habilida o desabilita los campos que sirven para editar elementos
        /// </summary>
        /// <param name="habilitacion"></param>
        public void cambiarHabilitacionEdicion(bool habilitacion)
        {
            this.groupBox.Visible = false;
            this.textBoxEditar.Enabled = habilitacion;
            this.buttonBuscar.Enabled = habilitacion;
        }

        /// <summary>
        /// vacia los campos
        /// </summary>
        public void vaciarCamposDeTexto()
        {
            this.comboBox1.Text = "";
            this.textnombre.Text = "";
            this.dateTimePicker1.Text = "";
            this.textBoxMonto.Text = "";
            this.comboBoxDebeHaber.Text = "";
            //this.textBoxGrupo.Text = "";
        }
                     
        /// <summary>
        ///  crea elementos con los datos en los campos y los carga a la base de datos
        /// </summary>
        private void agregarElemento()
        {           
            if (!hayCamposVacios())
            {
                if (this.checkedListBox1.CheckedItems.Count > 0)
                {
                    if (primerNumeroValido())
                    {
                        if (codigoEsCorrecto())
                        {
                            string monto = textBoxMonto.Text; //monto
                            if (!monto.Contains("."))
                            {
                                if (esDecimal(monto))
                                {
                                    if (esPositivo(monto))
                                    {

                                        ElementoLibroDiario elemento = crearElemento();  //creamos el objeto y lo agregamos a la lista
                                        if (int.Parse(this.labelDia.Text) == conexion.numeroDelDia())
                                        {                                            
                                            this.conexion.guardarLibroDiario(elemento);//guardamos el objeto en la base de datos
                                            actualizar(elementosLibroDiario);
                                        }  
                                        else
                                        {                                           
                                            lista.Add(elemento);
                                            cargarTabla(lista);
                                        }          
                                    }
                                    else this.mostrarMensajeError("El monto debe ser positivo mayor a 0.");
                                }
                                else this.mostrarMensajeError("El monto debe ser un numero.");
                            }
                            else this.mostrarMensajeError("el monto debe ir con coma, no con punto.");
                        }
                        else preguntarSiAgregar();
                    }
                    else mostrarMensajeError("el primer caracter de tu codigo debe estar entre el 1 y el 7 en referencia a su grupo:\n\n" +
                        "\t1. Activo\n" +
                        "\t2. Pasivo\n" +
                        "\t3. Patrimonio Neto\n" +
                        "\t4. Resultados Positivos\n" +
                        "\t5. Resultados Negativos\n" +
                        "\t6. Orden\n" +
                        "\t7. Movimiento");
                }
                else this.mostrarMensajeError("Debe seleccionar la documentacion respaldatoria.");
            }
            else this.mostrarMensajeError("Debe rellenar todos los campos.");
        }

        /// <summary>
        /// verifica que el primer numero del codigo ingresado este entre 1 y 8
        /// </summary>
        /// <returns></returns>
        private bool primerNumeroValido()
        {
            int numero = int.Parse(this.comboBox1.Text.ElementAt(0).ToString());

            if ((numero > 0) && (numero < 8)) return true;
            return false;
        }

        /// <summary>
        /// pregunta si se desea agregar un elemento al plan contable, en caso de que la respuesta sea si
        /// se lo crea y se lo guarda, en caso contrario no se hace nada
        /// </summary>
        private void preguntarSiAgregar()
        {
            string respuesta = mostrarMensajeConfirmacion("El codigo no existe ¿desearia agregarlo al plan contable?");

            if (respuesta.Equals("OK"))
            {
                crearElementoPlanContable();

                mostrarMensajeGuardado("El elemento contable ha sido añadido con exito");
                
                this.comboBox1.Items.Clear();
                Conexion.cargarComboBox(this.comboBox1);
                comboBox1.SelectedIndex = comboBox1.FindString(comboBox1.Text);
                agregarElemento();
            }
            else this.mostrarMensajeError("El numero del codigo no es corecto.");
        }

        /// <summary>
        /// edita el elemento del index que le pasemos
        /// selecciona el dia automaticamente en base a los datos guardados 
        /// previamente en el elemento
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool editarElemento(int index)
        {   
            if (!hayCamposVacios())
            {
                if (codigoEsCorrecto())
                {
                    if (this.checkedListBox1.CheckedItems.Count > 0)
                    {
                        string monto = textBoxMonto.Text; //monto
                        if (esDecimal(monto))
                        {
                            if (esPositivo(monto))
                            {
                                int dia = int.Parse(labelDia.Text);
                                if (dia == conexion.numeroDelDia())
                                {
                                    ElementoLibroDiario elemento2 = crearElemento();
                                    //guardamos el elemento en la base de datos
                                    this.conexion.editarLibroDiario(index, elemento2);
                                    actualizar(elementosLibroDiario);
                                    return true;
                                }
                                else
                                {
                                    ElementoLibroDiario elemento2 = crearElemento();
                                    lista.RemoveAt(index);
                                    lista.Insert(index, elemento2);
                                    cargarTabla(lista);
                                    return true;
                                }

                            }
                            else this.mostrarMensajeError("el monto debe ser positivo mayor a 0");
                        }
                        else this.mostrarMensajeError("el monto debe ser un numero");
                    }
                    else this.mostrarMensajeError("Debe seleccionar la documentacion respaldatoria.");
                }
                else this.mostrarMensajeError("el numero del codigo no es corecto");


            }
            else this.mostrarMensajeError("rellene todos los campos para continuar");
            return false;
        }

        /// <summary>
        /// pregunta al usuario si desea continuar desde donde quedo 
        /// en caso de que sea si se carga el ultimo dia sin asentar
        /// </summary>
        public void continuar()
        {
            string respuesta = mostrarMensajeConfirmacion("desea continuar desde lo ultimo que se guardo?");


            if (!respuesta.Equals("OK"))
            {
                this.conexion.borrarNoAsentado();
                this.elementosLibroDiario.Clear();
                cargarTabla(elementosLibroDiario);
            }
            else conexion.cargarDatosNoAsentados(elementosLibroDiario);

        }

        /// <summary>
        /// carga el elemento en los campos de edicion
        /// </summary>
        /// <param name="elemento"></param>
        private void montarEnCampos(ElementoLibroDiario elemento)
        {
            //si no inicializo asi por alguna razon queda en blanco
            this.comboBox1.SelectedIndex = 1;
            this.comboBoxDebeHaber.SelectedIndex = 1;



            //aca si cargo los elementos
            this.comboBox1.SelectedIndex = this.comboBox1.FindString(elemento.codigo); //editamos el codigo
            this.textBoxMonto.Text = elemento.monto;//editamos el monto
            this.dateTimePicker1.Value = elemento.fecha; //editamos la fecha
            this.comboBoxDebeHaber.SelectedIndex = elemento.transaccion.Equals("HABER") ? 1 : 0;
            this.checkedListBox1.SelectedItem = elemento.documentacionRespaldatoria;

        }

        /// <summary>
        /// cambiar la propiedad asebtado a true en la base de datos
        /// </summary>
        /// <param name="dia"></param>
        private void asentarCambios(int dia)
        {

            conexion.borrarDiaLibroDiario(dia);

            for (int i = 0; i < lista.Count; i++)
            {
                conexion.guardarLibroDiario(lista.ElementAt(i));
            }

            conexion.asentarLibroDiario(labelDia.Text);
        }

        /// <summary>
        /// pone en la lista todos los elementos del libro diario
        /// y carga la tabla
        /// </summary>
        /// <param name="dia"></param>
        public void actualizar(List<ElementoLibroDiario> libroDiario, int dia = -1)
        {
            if (dia == -1) dia = int.Parse(this.labelDia.Text);

            this.conexion.cargarLibroDiario(dia, libroDiario);
            cargarTabla(libroDiario);
        }

        /// <summary>
        /// cambia la tabla en base al numero que diga en el labbelDia
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cambiarDia(object sender, EventArgs e)
        {

            if (esDecimal(selector.Text))
            {
                if (conexion.numeroDelDia() == int.Parse(selector.Text)) cambiarHabilitacionAgregar(true);
                else cambiarHabilitacionAgregar(false);
                
                this.conexion.cargarLibroDiario(int.Parse(selector.Text), lista);
                this.labelDia.Text = selector.Text;
                cargarTabla(lista);
                totales(lista);//actualiza los totales 
            }
            else if (!selector.Text.Any()) { }
            else mostrarMensajeError("debe ingresar un numero");

        }

        /// <summary>
        /// carga todos los dias en el selector
        /// </summary>
        public void cargarSelector()
        {
            int maximo = this.conexion.numeroDelDia();

            for (int i = 1; i <= maximo; i++)
            {
                selector.Items.Add(i);
            }

            this.selector.SelectedIndex = int.Parse(labelDia.Text) - 1;
        }

        //
        //
        //
        //
        //          retorna valores
        //
        //
        //
        //

        /// <summary>
        /// busca el nombre de el codigo que ingresaste
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public string darNombre(string cadena)
        {
            //busca solo los caracteres alfabeticos
            string nuevo = ""; //cadena que se retorna          
            int numero; //guarda el resultado de tryParse, no es importante
            string caracter; //el caracter que vamos a controlar lo cargamos aca 
            bool bandera = true;

            int i = 0;
            while (bandera)
            {
                caracter = cadena.ElementAt(i).ToString();
                while (int.TryParse(caracter, out numero))
                {
                    caracter = cadena.ElementAt(i).ToString();
                    i++;
                }
                while (bandera)
                {
                    nuevo += cadena.ElementAt(i);
                    i++;
                    if(i >= cadena.Length)
                    {
                        bandera = false;
                    }
                }  
            }

            return nuevo;
            
        }

        /// <summary>
        /// busca el id del codigo que ingresaste
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public string darId(string cadena)
        {
            
            string nuevo = ""; //cadena que se retorna          
            string caracter; //el caracter que vamos a controlar lo cargamos aca 
            bool bandera = true;

            int i = 0;
            while (bandera)
            {
                caracter = cadena.ElementAt(i).ToString();

                if (!esDecimal(caracter)) bandera = false;
                else nuevo = nuevo + cadena.ElementAt(i);
                i++;
            }

            return nuevo;
        }

        /// <summary>
        /// entrega el grupo al cual pertenece el codigo
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public string darGrupo(string cadena)
        {
            switch (cadena.ElementAt(0))
            {
                case '1': return "ACTIVO";
                case '2': return "PASIVO";
                case '3': return "PATRIMONIO NETO";
                case '4': return "RESULTADO POSITIVO";
                case '5': return "RESULTADO NEGATIVO";
                case '6': return "ORDEN";
                case '7': return "MOVIMIENTO";
            }
            return "ACTIVO";
        }

        /// <summary>
        /// toma el contenido de todos los campos y crea un nuevo elemento del libro diario
        /// </summary>
        /// <returns></returns>
        public ElementoLibroDiario crearElemento()
        {
            string monto = this.textBoxMonto.Text;
            string nombre = this.textnombre.Text; //nombre
            string id = this.comboBox1.Text.ToString(); //numero
            DateTime fecha = this.dateTimePicker1.Value; //fecha
            string transaccion = this.comboBoxDebeHaber.Text; //si es debe o haber
            int dia = int.Parse(this.labelDia.Text);
            string documentacionRespaldatoria = crearDocumentacionRespaldatoria();
            int index = elementosLibroDiario.Count;


            //creamos el objeto y lo agregamos a la lista
            ElementoLibroDiario elemento = new ElementoLibroDiario(dia, this.darId(id), fecha, nombre, monto, transaccion, documentacionRespaldatoria, index);
            return elemento;
        }

        /// <summary>
        /// crea un string con la documentacion respaldatoria
        /// </summary>
        /// <returns></returns>
        private string crearDocumentacionRespaldatoria()
        {
            string texto ="";
            int cantidadDeElementos = this.checkedListBox1.CheckedItems.Count;
            for (int i = 0; i < cantidadDeElementos; i++)
            {
                if ((i + 1) == cantidadDeElementos)
                {
                    texto += this.checkedListBox1.CheckedItems[i].ToString() + " ";
                }
                else texto += this.checkedListBox1.CheckedItems[i].ToString() + ", ";
            }   
            return texto;
        }
        
        /// <summary>
        /// crea un elemento del plan contable y lo guarda en la base de datos
        /// </summary>
        public void crearElementoPlanContable()
        {
            string nombre = preguntarNombre("ingrese el nombre que desea darle a el nuevo elemento del plan contable");
            string codigo = comboBox1.Text;
            this.conexion.guardarElementoContable(true, codigo, nombre);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
        }
    }    
}
