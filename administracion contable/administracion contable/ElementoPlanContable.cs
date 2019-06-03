using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace administracion_contable
{
    public class ElementoPlanContable
    {
        public string numeroIdentificador { get; set; }
        public string nombre { get; set; }
        public bool registrable { get; set; }

        public ElementoPlanContable(bool registrable, string id, string nombre)
        {
            this.registrable = registrable;
            this.numeroIdentificador = id;
            this.nombre = nombre;
        }

       
    }
}
