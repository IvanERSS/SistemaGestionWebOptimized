using SistemaGestionWEB.Repository;

namespace SistemaGestionWEB.Models
{
    public class ProductoVendido
    {
        public int ID { get; set; }
        public Producto Producto { get; set; }
        public int Cantidad { get; set; }
        public int IDVenta { get; set; }

        public ProductoVendido() { }

        public ProductoVendido(int iD, Producto producto, int cantidad, int iDVenta, double total)
        {
            ID = iD;
            Producto = producto;
            Cantidad = cantidad;
            IDVenta = iDVenta;
            //Total = total;
        }

        public double Total()
        {
            return Producto.PrecioVenta * Cantidad;
        }

        public Usuario GetOwner()
        {
            return UsuarioRepository.Get(Producto.Usuario.ID);
        }

        public string ToString()
        {
            return
                ($"ID:{ID}\t" +
                $"Articulo:{Producto.Descripciones}\t" +
                $"Precio:{Producto.PrecioVenta}\t" +
                $"Cantidad:{Cantidad}\t" +
                $"Total:{Total()}\t" +
                $"idVenta{IDVenta}");
        }
    }
}
