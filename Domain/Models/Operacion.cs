using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Operacion
    {
        public Saldo Resumen { get; set; } = null!;
        public TipoMovimiento TipoMovimiento { get; set; } = null!;
    }
}
