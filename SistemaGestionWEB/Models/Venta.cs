using SistemaGestionWEB.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionWEB.Models
{
    public class Venta
    {
        public int ID { get; set; }
        public string Comentarios { get; set; }
        public Usuario Usuario { get; set; }
        public List<ProductoVendido> Productos { get; set; }


        public Venta() { }

        public Venta(int iD, string comentarios, Usuario usuario, List<ProductoVendido> productos)
        {
            ID = iD;
            Comentarios = comentarios;
            Usuario = usuario;
            Productos = productos;
        }

        public double Total()
        {
            double total = 0;
            foreach (var p in Productos)
            {
                total = total + p.Total();
            }
            return total;
        }

        public int TotalArticulos()
        {
            int totalA = 0;
            foreach (var p in Productos)
            {
                totalA = totalA + p.Cantidad;
            }
            return totalA;
        }

        public string ToString()
        {
            return
                ($"ID:{ID}\t"+
                $"Comentarios:{Comentarios}\t"+
                $"Usuario:{Usuario.Nombre}{Usuario.Apellido}\t"+
                $"Articulos:{TotalArticulos()}\t" +
                $"Total:{Total()}");
        }

    }
}
