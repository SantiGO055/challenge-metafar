using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class TipoMovimiento
    {
        [Key]
        public int IDTipoMovimiento { get; set; }
        public string? DescripcionMovimiento { get; set; }
        public virtual Movimiento Movimiento { get; set; } = null!;

    }
}
