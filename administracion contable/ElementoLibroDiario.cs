using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion_contable
{
    public class ElementoLibroDiario
    {
        public int dia;
        public string documentacionRespaldatoria { get; set; }
        public int folio;
        public string nombre;
        public DateTime fecha;
        public string monto;
        public string transaccion;
        public string codigo;
        public int index;
        public string descripcion { get; set; }

        public ElementoLibroDiario(int folio, string codigo, DateTime date, string nombre, string monto, string transaccion, string documentacionRespaldatoria, int index, int dia, string descripcion = "")
        {
            this.nombre = nombre;
            this.fecha = date;            
            this.monto = monto;
            this.transaccion = transaccion;
            this.codigo = codigo;
            this.folio = folio;
            this.documentacionRespaldatoria = documentacionRespaldatoria;
            this.index = index;
            this.descripcion = descripcion;
            this.dia = dia;
        }

        public void cambiarElemento(ElementoLibroDiario nuevo)
        {
            this.nombre = nuevo.nombre;
            this.fecha = nuevo.fecha;           
            this.monto = nuevo.monto;
            this.transaccion = nuevo.transaccion;
            this.codigo = nuevo.codigo;
            this.folio = nuevo.folio;
            this.documentacionRespaldatoria = nuevo.documentacionRespaldatoria;
            this.descripcion = nuevo.descripcion;
            this.dia = nuevo.dia;
        }
    }
}
