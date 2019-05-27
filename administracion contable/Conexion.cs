using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;

namespace administracion_contable
{
    class Conexion
    {
        String crearPlanContable = "create table if not exists elementosDelPlanContables(" +
                                        "registrado boolean," +
                                        "numeroIdentificador varchar," +
                                        "nombre varchar)";

        String crearLibroDiario = "create table if not exists LibrosDiarios(" +
                                        "dia int," +
                                        "codigo varchar," +
                                        "fecha varchar," +
                                        "nombre varchar," +
                                        "monto varchar," +
                                        "transaccion varchar," +
                                        "documentacion varchar," +
                                        "asentado boolean," +
                                        "indice int)";

        String crearFecha = "create table if not exists fecha(" +
                                 "inicio varchar," +
                                 "fin varchar)";



        SqliteConnection Connection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Revisar consultas SQL para comprobar si tienen vulnerabilidades de seguridad")]

        //metodos para toda la base de datos
        /// <summary>
        /// se conecta a la base de datos
        /// </summary>
        private void conectar()
        {

            try
            {
                this.Connection = new SqliteConnection("Data Source = datos contables");
                this.Connection.Open();
                this.Connection.DefaultTimeout = 1;
            }
            catch (Exception ex)
            {
                throw new Exception("no pudo realizarse la conexion" + ex);
            }


        }

        public void crearTablas()
        {
            if (!File.Exists("datos contables"))
            {
                SQLiteConnection.CreateFile("datos contables");
            }

            try
            {
                conectar();
                SqliteCommand command = new SqliteCommand(crearPlanContable, this.Connection);
                command.ExecuteNonQuery();
                command.Connection.Close();

                conectar();
                command = new SqliteCommand(crearLibroDiario, this.Connection);
                command.ExecuteNonQuery();
                command.Connection.Close();

                conectar();
                command = new SqliteCommand(crearFecha, this.Connection);
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
            catch (Exception e)
            {
                mostrarMensaje("Error: " + e);
                throw new Exception("Error: " + e);
            }
            finally
            {
                close();

            }
            

        }



        /// <summary>
        /// cierra la base de datos
        /// </summary>
        public void close()
        {
            try
            {
                this.Connection.Close();
            }
            catch (Exception)
            {
                throw new Exception("no puso realizarse la desconexion");
            }
        }

        /// <summary>
        /// muestra un mensaje por pantalla
        /// </summary>
        /// <param name="mensaje">mensaje que se va a enviar</param>
        public void mostrarMensaje(string mensaje)
        {
            const string caption = "Datos Erroneos";
            var result = MessageBox.Show(mensaje, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Error);
        }


           


        //base de datos y libro diario

        public bool hayFechaLimite()

        {
            conectar();
            string sql = "select count(*) as total from fecha";
            int count = 0;

            SqliteCommand command = new SqliteCommand(sql, Connection);

            try
            {

                SqliteDataReader lector = command.ExecuteReader();
                count = lector.GetInt32(0);
                command.Connection.Close();
                lector.Close();
            }
            catch (Exception e)
            {
                mostrarMensaje(e.ToString());
                close();
                return false;
            }
            finally
            {
                close();
            }

            if (count > 0)
            {
                return true;
            }
            return false;


        }

        public void asignarRango(DateTimePicker dateTimePicker)
        {
            try
            {
                conectar();
                String sql = "select * from fecha";
                SqliteCommand command = new SqliteCommand(sql, this.Connection);

                try
                {
                    SqliteDataReader lector = command.ExecuteReader();
                    while (lector.Read())
                    {
                        dateTimePicker.MinDate = DateTime.Parse(lector.GetString(0));
                        dateTimePicker.MaxDate = DateTime.Parse(lector.GetString(1));

                    }

                    lector.Close();
                    command.Connection.Close();

                }
                catch (Exception e)
                {
                    mostrarMensaje("error " + e);
                    throw new Exception("error en la base de datos: " + e);
                }
                finally
                {
                    command.Connection.Close();
                    close();
                }
            }
            catch (Exception e)
            {
                throw new Exception("error :  " + e);
            }
        }

        /// <summary>
        /// define el inicio y el final del libro diario
        /// </summary>
        /// <param name="inicio">feha minima</param>
        /// <param name="final">fecha maxima</param>
        public void guardarLimites(string inicio, string final)
        {
            try
            {
                conectar();

                string sql = "insert into fecha(inicio,fin) values ('" + inicio + "','" + final + "')";
                SqliteCommand command = new SqliteCommand(sql, this.Connection);

                try
                {
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Error: " + e);
                }
                finally
                {
                    command.Connection.Close();
                    close();
                }

            }
            catch (Exception)
            {
                close();
            }
        }

        /// <summary>
        /// agrega un registro del libro diario a la base de datos
        /// </summary>
        /// <param name="dia">numero de libro diario</param>
        /// <param name="codigo">codigo </param>
        /// <param name="date">fecha de la accion</param>
        /// <param name="nombre">nombre identificatorio</param>
        /// <param name="monto">monto de la accion</param>
        /// <param name="transaccion">cargar o abonar</param>
        public void guardarLibroDiario(int dia, string codigo, DateTime date, string nombre, string monto, string transaccion, string documentacion)
        {
            string fecha = date.ToString("dd/mm/yyyy");

            try
            {
                conectar();

                string sql = "insert into LibrosDiarios (dia,codigo,fecha,nombre,monto,transaccion,asentado) values ('" + dia + "','" + codigo + "','" + fecha + "','" + nombre + "'  '" + monto + "','" + transaccion + "','" + documentacion + "', " + false + ")";
                SqliteCommand command = new SqliteCommand(sql, this.Connection);


                try
                {
                    command.ExecuteNonQuery();//cargamos los datos
                    command.Connection.Close();
                }
                catch (Exception e)
                {
                    mostrarMensaje("Error: " + e);
                }
                finally
                {
                    command.Connection.Close();
                    close();
                }

            }
            catch (Exception ex)
            {

                throw new Exception("no se ha podido realizar la conexion" + ex);

            }
            finally
            {
                close();
            }
        }

        /// <summary>
        /// agrega un registro del libro diario a la base de datos
        /// </summary>
        /// <param name="elemento">registro que vamos a cargar</param>
        internal void guardarLibroDiario(ElementoLibroDiario elemento)
        {

            string documentacion = elemento.documentacionRespaldatoria;
            int dia = elemento.dia;
            string codigo = elemento.codigo;
            string fecha = elemento.fecha.ToString();
            string nombre = elemento.nombre;
            string monto = elemento.monto;
            string transaccion = elemento.transaccion;
            int index = elemento.index;

            try
            {
                conectar();
                string sql = "insert into LibrosDiarios(dia, codigo, fecha, nombre, monto, transaccion, documentacion, asentado, indice)" +
                    " values (" + dia + ",'" + codigo + "','" + fecha + "','" + nombre + "','" + monto + "','" + transaccion + "','" + documentacion + "'," + false + "," + index + ")";

                SqliteCommand command = new SqliteCommand(sql, this.Connection);

                try
                {
                    command.ExecuteNonQuery();//cargamos los datos
                }
                catch (Exception e)
                {
                    command.Connection.Close();
                    mostrarMensaje(e.ToString());
                    throw new Exception(" " + e);
                }

                command.Connection.Close();
                close();


            }
            catch (Exception ex)
            {
                throw new Exception("no se ha podido realizar la conexion: " + ex);
            }
            finally
            {

                close();
            }
        }

        /// <summary>
        /// carga en la lista los elementos del libro diario que correspondan con el dia
        /// </summary>
        /// <param name="dia">inidice de los elementos a traer</param>
        /// <param name="librosDiarios">lista donde vamos a cargar los elementos</param>
        public void cargarLibroDiario(int dia, List<ElementoLibroDiario> librosDiarios)
        {
            librosDiarios.Clear();
            String consulta = "select * from LibrosDiarios where dia = " + dia;
            try
            {
                conectar();
                leerLibroDiario(consulta, librosDiarios);
            }
            catch (Exception e)
            {
                throw new Exception("Error: " + e);
            }
            finally
            {
                close();
            }
        }

        /// <summary>
        /// edita un registro del libro diario que tenga el index 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="elemento"></param>
        public void editarLibroDiario(int index, ElementoLibroDiario elemento)
        {
            string documentacion = elemento.documentacionRespaldatoria;
            string codigo = elemento.codigo;
            string fecha = elemento.fecha.ToString();
            string nombre = elemento.nombre;
            string monto = elemento.monto;
            string transaccion = elemento.transaccion;
            int dia = elemento.dia;

            try
            {

                conectar();
                string sql = "update LibrosDiarios set codigo = '" + codigo + "', fecha = '" + fecha + "', nombre = '" + nombre + "', monto = '" + monto + "'" +
                    ", transaccion = '" + transaccion + "', documentacion = '" + documentacion + "' where dia = " + dia + " and indice = " + index;
                SqliteCommand command = new SqliteCommand(sql, this.Connection);

                try
                {
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Error: " + e);
                }
                finally
                {
                    close();
                }


            }
            catch (Exception e)
            {

                throw new Exception("Error: " + e);
            }

        }

        /// <summary>
        /// carga en una lista todos los elementos que tengan el parametro "asentado" = false en la base de datos;
        /// </summary>
        /// <param name="librosDiarios"></param>
        public void cargarDatosNoAsentados(List<ElementoLibroDiario> librosDiarios)
        {
            librosDiarios.Clear();
            String consulta = "select * from LibrosDiarios where asentado = false";
            try
            {
                conectar();
                leerLibroDiario(consulta, librosDiarios);
            }
            catch (Exception e)
            {
                throw new Exception("Error: " + e);
            }
            finally
            {
                close();
            }
        }

        /// <summary>
        /// verifica si hay elementos que tengan el parametro "asentado" = false
        /// </summary>
        /// <returns></returns>
        public bool hayDatosNoAsentado()
        {
            try
            {
                conectar();
                string sql = "select * from LibrosDiarios where asentado = false";
                SqliteCommand command = new SqliteCommand(sql, this.Connection);

                try
                {
                    SqliteDataReader lector = command.ExecuteReader();

                    if (lector.Read())
                    {
                        command.Connection.Close();
                        lector.Close();
                        return true;
                    }
                    else
                    {
                        command.Connection.Close();
                        lector.Close();
                        return false;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Error: " + e);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error: " + e);
            }
        }

        /// <summary>
        /// entrega el numero mayor que hay en dia
        /// </summary>
        /// <returns>retorna el numero mas grande de la columna dia, si no hay ninguno retorna 1</returns>
        public int numeroDelDia()
        {
            try
            {
                conectar();
                string sql = "SELECT max(dia) FROM LibrosDiarios where asentado = true ORDER BY dia DESC";
                //SELECT max(numeroidentificador) FROM elementodelplancontables ORDER BY Id DESC;
                SqliteCommand command = new SqliteCommand(sql, Connection);
                try
                {
                    SqliteDataReader lector = command.ExecuteReader();//crea el objeto encargado de leer
                    int dia = lector.GetInt32(0) + 1;
                    lector.Close();
                    command.Connection.Close();
                    return dia;

                }
                catch (Exception)
                {
                    command.Connection.Close();
                    return 1;

                }


            }
            catch (Exception e)
            {
                throw new Exception("error: " + e);
            }

        }

        /// <summary>
        /// carga todos los elementos de la base de datos en una lista
        /// </summary>
        /// <param name="elementosLibroDiario">lista donde cargamos los elementos</param>
        public void mostrarLibroTotal(List<ElementoLibroDiario> elementosLibroDiario)
        {
            elementosLibroDiario.Clear();
            String consulta = "select * from LibrosDiarios where asentado = true ORDER BY codigo";
            try
            {
                conectar();
                leerLibroDiario(consulta, elementosLibroDiario);
            }
            catch (Exception e)
            {
                throw new Exception("error: " + e);
            }
            finally
            {
                close();
            }
        }

        /// <summary>
        /// busca en la base de datos los elementos con el mismo codigo y los carga en una lista
        /// </summary>
        /// <param name="codigo">codigo que va a comparar</param>
        /// <param name="librosDiarios">lista donde vas a cargar los elementos</param>
        public void buscarPorCodigo(string codigo, List<ElementoLibroDiario> librosDiarios)
        {

            librosDiarios.Clear();
            String consulta = "select * from LibrosDiarios where codigo = " + codigo;

            try
            {
                conectar();
                leerLibroDiario(consulta, librosDiarios);
            }
            catch (Exception e)
            {
                throw new Exception("Error: " + e);
            }
            finally
            {
                close();
            }
        }

        /// <summary>
        /// realiza la lectura de la base de datos y la carga en la lista
        /// </summary>
        /// <param name="consulta"></param>
        /// <param name="elementosLibroDiario"></param>
        private void leerLibroDiario(string consulta, List<ElementoLibroDiario> elementosLibroDiario)
        {
            SqliteCommand command = new SqliteCommand(consulta, Connection);

            try
            {
                SqliteDataReader lector = command.ExecuteReader();


                while (lector.Read())
                {
                    //dia,codigo,fecha,monto,transaccion
                    int dia = lector.GetInt32(0);
                    string codigo = lector.GetString(1);
                    DateTime fecha = DateTime.Parse(lector.GetString(2));
                    string nombre = lector.GetString(3);
                    string monto = lector.GetString(4);
                    string transaccion = lector.GetString(5);
                    string documentacionRespaldatoria = lector.GetString(6);
                    int index = lector.GetInt32(7);


                    ElementoLibroDiario elementoLibroDiario = new ElementoLibroDiario(dia, codigo, fecha, nombre, monto, transaccion, documentacionRespaldatoria, index);
                    elementosLibroDiario.Add(elementoLibroDiario);
                }

                lector.Close();
                command.Connection.Close();

            } catch (Exception e)
            {
                command.Connection.Close();
                throw new Exception("no se pudo realizar la coneccion: " + e);
            }
            finally
            {
                command.Connection.Close();
            }
        }

        /// <summary>
        /// elimina de la base de datos todos los elementos que tengan el parametro "asentado" = false
        /// </summary>
        public void borrarNoAsentado()
        {
            ///hay que agregar un campo boleano que sea guardado, cuando el campo sea true no lo borramos, van a ser false hasta que toquemos guardar
            try
            {
                conectar();
                string sql = "delete from LibrosDiarios where asentado = " + false;

                SqliteCommand command = new SqliteCommand(sql, this.Connection);

                try
                {
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (Exception e)
                {

                    throw new Exception("error: " + e);
                }
            }
            catch (Exception e)
            {
                throw new Exception("error: " + e);
            }
            finally
            {
                close();
            }
        }

        /// <summary>
        /// pone en true el parametro "asentado" de todos los elementos que lo tengan en false
        /// </summary>
        /// <param name="dia"></param>
        public void asentarLibroDiario(string dia)
        {
            int numero = int.Parse(dia);
            try
            {
                conectar();
                string sql = "update LibrosDiarios set asentado = true where dia = " + numero;
                SqliteCommand command = new SqliteCommand(sql, this.Connection);

                try
                {
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (Exception e)
                {

                    throw new Exception("Error: " + e);
                }

            }
            catch (Exception e)
            {

                throw new Exception("Error: " + e);
            }
            finally
            {
                close();
            }

        }

        /// <summary>
        /// elimina un elemento del libro diario
        /// </summary>
        /// <param name="indice">indice del elemento que vamos a borrar</param>
        /// <param name="dia">de que dia es ese indice</param>
        internal void borrarElementoLibroDiario(int indice, int dia)
        {
            try
            {
                conectar();
                string sql = "delete from LibrosDiarios where dia = " + dia + " and indice = " + indice;
                SqliteCommand command = new SqliteCommand(sql, this.Connection);

                try
                {
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (Exception e)
                {
                    mostrarMensaje("Error: " + e);
                    throw new Exception("Error: = " + e);
                }
            }
            catch (Exception e)
            {
                mostrarMensaje("Error: " + e);
                throw new Exception("Error: = " + e);
            }
            finally
            {
                close();
            }

        }

        /// <summary>
        /// elimina un dia entero de la base de datos
        /// </summary>
        /// <param name="dia">dia que vamos a eliminar</param>
        internal void borrarDiaLibroDiario(int dia)
        {
            try
            {
                conectar();
                string sql = "delete from LibrosDiarios where dia = " + dia;
                SqliteCommand command = new SqliteCommand(sql, this.Connection);

                try
                {
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (Exception e)
                {
                    mostrarMensaje("Error: " + e);
                    throw new Exception("Error: = " + e);
                }
            }
            catch (Exception e)
            {
                mostrarMensaje("Error: " + e);
                throw new Exception("Error: = " + e);
            }
            finally
            {
                close();
            }
        }







        //base de datos y plan contable

        /// <summary>
        /// guarda en la base de datos un elemento contable
        /// </summary>
        /// <param name="numeroIdentificador">codigo del elemento contable</param>
        /// <param name="nombre">nombre identificador del elemento contable</param>
        public void guardarElementoContable(bool registrado, string numeroIdentificador, string nombre)
        {

            try
            {
                conectar();
                string sql = "insert into elementosDelPlanContables(registrado,numeroIdentificador,nombre) values (" + registrado + ",'" + numeroIdentificador + "','" + nombre + "')";
                SqliteCommand command = new SqliteCommand(sql, this.Connection);

                try
                {
                    command.ExecuteNonQuery();//cargamos los datos
                    command.Connection.Close();
                }
                catch (Exception e)
                {
                    mostrarMensaje("error: " + e);

                }
                finally
                {
                    close();
                }

            }
            catch (Exception ex)
            {

                throw new Exception("no se ha podido realizar la conexion" + ex);

            }
            finally
            {
                close();
            }

        }

        /// <summary>
        /// cargar en una lista los elementos del plan contable 
        /// guardados en la base de datos
        /// </summary>
        /// <param name="lista">lista donde se cargaran los elementos</param>
        public void cargarElementoContable(List<ElementoPlanContable> lista)
        {
            lista.Clear();

            conectar();

            String consulta = "select * from elementosDelPlanContables ORDER BY numeroIdentificador COLLATE NOCASE ASC";
            SqliteCommand command = new SqliteCommand(consulta, Connection);

            try
            {
                SqliteDataReader lector = command.ExecuteReader();


                while (lector.Read())
                {
                    bool registrado = lector.GetBoolean(0);
                    string id = lector.GetString(1);
                    string nombre = lector.GetString(2);
                    ElementoPlanContable elementoPlanContable = new ElementoPlanContable(registrado, id, nombre);
                    lista.Add(elementoPlanContable);
                }

                command.Connection.Close();
                lector.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Error: " + e);

            }
            finally
            {
                close();
            }

        }

        /// <summary>
        /// carga en una lista todos los elementos del plan contable registrados
        /// </summary>
        /// <param name="lista"></param>
        private void cargarElementoContableRegistrado(List<ElementoPlanContable> lista)
        {
            lista.Clear();

            conectar();

            String consulta = "select * from elementosDelPlanContables where registrado = true ORDER BY numeroIdentificador COLLATE NOCASE ASC";
            SqliteCommand command = new SqliteCommand(consulta, Connection);

            try
            {
                SqliteDataReader lector = command.ExecuteReader();


                while (lector.Read())
                {
                    bool registrado = lector.GetBoolean(0);
                    string id = lector.GetString(1);
                    string nombre = lector.GetString(2);
                    ElementoPlanContable elementoPlanContable = new ElementoPlanContable(registrado, id, nombre);
                    lista.Add(elementoPlanContable);
                }

                command.Connection.Close();
                lector.Close();

            }
            catch (Exception e)
            {
                throw new Exception("Error: " + e);
            }
            finally
            {
                close();
            }

        }

        /// <summary>
        /// carga un comboBox con los elementos de el plan contable
        /// </summary>
        /// <param name="combobox">comboBox que vamos a cargar</param>
        public static void cargarComboBox(ComboBox combobox)
        {
            //generamos una lista
            List<ElementoPlanContable> planesContables = new List<ElementoPlanContable>();

            //creamos elemento conexion
            Conexion conexion = new Conexion();


            //llenamos la lista con los elementos de la base de datos
            conexion.cargarElementoContableRegistrado(planesContables);

            for (int i = 0; i < planesContables.Count; i++)
            {
                //creamos un string con todos los datos 
                string id = planesContables.ElementAt(i).numeroIdentificador;
                string nombre = planesContables.ElementAt(i).nombre;
                string total = id + " " + nombre;

                //agregamos ese string al comboBox
                combobox.Items.Add(total);
            }

        }
        
        /// <summary>
        /// retorna el elemento del plan contable que tenga el index que introducimos
        /// </summary>
        /// <param name="index"> indice que queremos buscar </param>
        /// <returns>retorna un elemento contable que exista en la base de datos</returns>
        public ElementoPlanContable buscarElementoPlanContable(string numeroIdentificador)
        {
            try
            {
                conectar();

                String consulta = "select * from elementosDelPlanContables where numeroIdentificador = " + numeroIdentificador;
                SqliteCommand command = new SqliteCommand(consulta, Connection);
                try
                {
                    SqliteDataReader lector = command.ExecuteReader();


                    while (lector.Read())
                    {
                        bool registrado = lector.GetBoolean(0);
                        string id = lector.GetString(1);
                        string nombre = lector.GetString(2);
                        ElementoPlanContable elementoPlanContable = new ElementoPlanContable(registrado, id, nombre);
                        return elementoPlanContable;
                    }

                    command.Connection.Close();
                    lector.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("Error al ejecutar codigo en la base de datos" + e);
                }
            }
            catch (Exception e)
            {
                throw new Exception("no se pudo realizar la coneccion a la base de datos" + e);
            }
            finally
            {
                close();
            }
            return null;
        }

        /// <summary>
        /// modifica un elemento en la base de datos
        /// </summary>
        /// <param name="index">indice del elemento</param>
        /// <param name="numeroIdentificador">numero que vamos a colocar</param>
        /// <param name="nombre">nombre que vamos a colocar</param>
        public void modificarElementoPlanContable(string numeroIdentificadorViejo, bool registrado, string numeroIdentificador, string nombre)
        {
            try
            {


                conectar();
                string sql = "update elementosDelPlanContables set registrado = " + registrado +
                    ", numeroIdentificador = '" + numeroIdentificador + "', nombre = '" + nombre + "' where numeroIdentificador = " + numeroIdentificadorViejo;

                SqliteCommand command = new SqliteCommand(sql, this.Connection);


                try
                {
                    command.ExecuteNonQuery();//cargamos los datos
                    command.Connection.Close();
                }
                catch (Exception e)
                {
                    throw new Exception("error durante la edicion: " + e);

                }
                finally
                {
                    close();
                }

            }
            catch (Exception ex)
            {

                throw new Exception("no se ha podido realizar la conexion" + ex);

            }
            finally
            {
                close();
                modificarPlanContableEnLibroDiario(numeroIdentificadorViejo, numeroIdentificador, nombre);
            }

        }

        public void modificarPlanContableEnLibroDiario(string numeroIdentificadorViejo, string numeroIdentificador, string nombre)
        {
            try
            {
                conectar();
                string sql = "update LibrosDiarios set codigo = '" + numeroIdentificador + "', nombre = '" + nombre + "' where codigo = " + numeroIdentificadorViejo;
                SqliteCommand command = new SqliteCommand(sql, this.Connection);

                try
                {
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (Exception e)
                {
                    mostrarMensaje("Error: " + e);
                    throw new Exception("Error: " + e);
                }
                finally
                {
                    close();
                }
            }
            catch (Exception e)
            {
                mostrarMensaje("Error: " + e);
                throw new Exception("Error: " + e);
            }
            finally
            {
                close();
            }
        }
        
        public void borrarElementoPlanContable(string codigo)
        {
            try
            {
                conectar();
                string sql = "delete from elementosDelPlanContables where numeroIdentificador = " + codigo;

                SqliteCommand command = new SqliteCommand(sql, this.Connection);

                try
                {
                    command.ExecuteNonQuery();
                    command.Connection.Close();
                }
                catch (Exception e)
                {
                    mostrarMensaje("Error: " + e);
                    throw new Exception("Error: " + e);
                }
            }
            catch (Exception e)
            {
                mostrarMensaje("Error: " + e);
                throw new Exception("Error: " + e);
            }
        }

        public bool yaTieneAsiento(string codigo)
        {
            List<ElementoLibroDiario> lista = new List<ElementoLibroDiario>();
            mostrarLibroTotal(lista);
            ElementoLibroDiario cosa = lista.Find(q => q.codigo == codigo);

            return cosa != null;          
           
        }

        

        /// <summary>
        /// create table escuela(
        /// numero INTEGER primary key AUTOINCREMENT ,
        /// nombre varchar,
        /// persona_dni int,
        /// foreign key(persona_dni) references persona(dni))
        /// </summary>
        /// <param name="combobox"></param>
        /// 





    }

}
