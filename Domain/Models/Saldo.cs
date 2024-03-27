using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Saldo
    {
        [Key]
        public string? NombreUsuario { get; set; }
        public int NroCuenta { get; set; }
        public decimal? SaldoActual { get; set; }
        public DateTime? FechaExtraccion { get; set; }
    }
}
