using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Usuario
    {
        [Key]
        public int IDUsuario { get; set; }
        public string? Nombre { get; set; }
        public virtual CuentaBancaria CuentaBancaria { get; set; } = null!;

    }
}
