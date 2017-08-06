using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Impresora.Objects
{
    class Order
    {
        public int IdCaja { get; set; }
        public int IdOperador { get; set; }
        public int IdCliente { get; set; }
        public int Volumen { get; set; }
    }
}
