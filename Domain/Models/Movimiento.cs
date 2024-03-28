using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Movimiento
    {
        [Key]
        public int IDMovimientos { get; set; }
        public int IDCuentaBancaria { get; set; }
        public int IDTipoMovimiento { get; set; } 
        public decimal Saldo { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public virtual TipoMovimiento TipoMovimiento { get; set; } = null!;
        public virtual CuentaBancaria CuentaBancaria { get; set; } = null!;

    }
}
