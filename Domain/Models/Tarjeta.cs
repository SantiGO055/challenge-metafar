using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Domain.Models
{
    public class Tarjeta
    {
        [Key]
        public int IDTarjeta {  get; set; }
        public string NroTarjeta { get; set; } = string.Empty;
        public int Pin { get; set; }
        public int Intentos { get; set; }
        public bool TarjetaBloqueada { get; set; }
        public virtual CuentaBancaria CuentaBancaria { get; set; } = null!;
        public virtual ICollection<Movimiento> Movimientos { get; set; } = [];
    }
}
