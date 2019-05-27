using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion_contable
{
    public class ElementoLibroDiario
    {
        public string documentacionRespaldatoria { get; set; }
        public int dia;
        public string nombre;
        public DateTime fecha;
        public string monto;
        public string transaccion;
        public string codigo;
        public int index;

        public ElementoLibroDiario(int dia, string codigo, DateTime date, string nombre, string monto, string transaccion, string documentacionRespaldatoria, int index)
        {
            this.nombre = nombre;
            this.fecha = date;            
            this.monto = monto;
            this.transaccion = transaccion;
            this.codigo = codigo;
            this.dia = dia;
            this.documentacionRespaldatoria = documentacionRespaldatoria;
            this.index = index;
        }

        public void cambiarElemento(ElementoLibroDiario nuevo)
        {
            this.nombre = nuevo.nombre;
            this.fecha = nuevo.fecha;           
            this.monto = nuevo.monto;
            this.transaccion = nuevo.transaccion;
            this.codigo = nuevo.codigo;
            this.dia = nuevo.dia;
            this.documentacionRespaldatoria = nuevo.documentacionRespaldatoria;
        }
    }
}
