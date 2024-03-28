using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CuentaBancaria
    {
        [Key]
        public int IDCuentaBancaria { get; set; }
        public int IDMovimiento { get; set; }
        public int IDTarjeta { get; set; }
        public virtual Tarjeta Tarjeta { get; set; } = null!;
        public virtual Usuario Usuario { get; set; } = new Usuario();
        public ICollection<Movimiento> Movimientos { get; set; } = [];
        public int NroCuenta { get; set; }
        public decimal Saldo { get; set; }
        // public Realizar

    }
}
