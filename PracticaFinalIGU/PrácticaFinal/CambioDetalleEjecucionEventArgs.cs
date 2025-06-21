using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrácticaFinal
{
    public class cambioDetalleEjecucionEventArgs : EventArgs
    {
        public DateTime fechaCambio {  get; set; }

        public cambioDetalleEjecucionEventArgs(DateTime d)
        {
            fechaCambio = d;
        }
    }
}
