using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionWEB.Models
{
    public class Producto
    {
        public int ID { get; set; }
        public string Descripciones { get; set; }
        public double Costo { get; set; }
        public double PrecioVenta { get; set; }
        public int Stock { get; set; }
        public Usuario Usuario { get; set; }

        public Producto() { }

        public Producto(int iD, string descripciones, double costo, double precioVenta, int stock, Usuario usuario)
        {
            ID = iD;
            Descripciones = descripciones;
            Costo = costo;
            PrecioVenta = precioVenta;
            Stock = stock;
            Usuario = usuario;
        }

        public String ToString()
        {
            return
                ($"ID:{ID}\t" +
                $"Producto:{Descripciones}\t" +
                $"Csoto:{Costo}\t" +
                $"Precio_venta:{PrecioVenta}\t" +
                $"Strock:{Stock}\t" +
                $"Usuario:{Usuario.Nombre} {Usuario.Apellido}");
        }

    }
}
