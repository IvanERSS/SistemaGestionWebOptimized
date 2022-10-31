using SistemaGestionWEB.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace SistemaGestionWEB.Repository
{
    public class VentaRepository
    {
        public static List<Venta> Get()
        {
            var Ventas = new List<Venta>();

            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"
									SELECT
										id,
										Comentarios,
										IdUsuario
									FROM
										Venta
                                    ";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var venta = new Venta();
                    var pVendidos = new List<ProductoVendido>();
                    venta.ID = Convert.ToInt32(reader.GetValue(0));
                    venta.Comentarios = reader.GetValue(1).ToString();
                    venta.Usuario = UsuarioRepository.Get(Convert.ToInt32(reader.GetValue(2)));
                    venta.Productos = ProductoVendidoRepository.GetByIdVenta(Convert.ToInt32(reader.GetValue(0)));
                    Ventas.Add(venta);

                    //Console.WriteLine(venta.ToString());
                }


                //Validaciones console

                return Ventas;

                reader.Close();
                connection.Close();

            }

            return Ventas;
        }
        
        public static Venta Get(int _id)
        {
            var _Venta = new Venta();

            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int) { Value = _id });

                cmd.CommandText = @"
									SELECT
										id,
										Comentarios,
										IdUsuario
									FROM
										Venta
									WHERE id = @id
                                    ";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var venta = new Venta();
                    var pVendidos = new List<ProductoVendido>();
                    venta.ID = Convert.ToInt32(reader.GetValue(0));
                    venta.Comentarios = reader.GetValue(1).ToString();
                    venta.Usuario = UsuarioRepository.Get(Convert.ToInt32(reader.GetValue(2)));
                    venta.Productos = ProductoVendidoRepository.GetByIdVenta(Convert.ToInt32(reader.GetValue(0)));
                    _Venta = venta;

                    //Console.WriteLine(venta.ToString());
                }




                reader.Close();
                connection.Close();

            }

            return _Venta;
        }

        public static List<Venta> GetByUserId(int _UserId)
        {
            List<Venta> ventas = new List<Venta>();
            List<int> idVentas = new List<int>();

            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("idUser", System.Data.SqlDbType.Int) { Value = _UserId });
                cmd.CommandText = @"
								SELECT
									pv.IdVenta as idVenta
					            FROM
					            Producto AS p
					            INNER JOIN ProductoVendido pv ON p.Id = pv.IdProducto
					            INNER JOIN Usuario u ON u.id = p.IdUsuario
								WHERE
									@idUser = IdUsuario
								GROUP BY pv.IdVenta
                                ";

                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    idVentas.Add(Convert.ToInt32(reader.GetValue(0)));
                }
                connection.Close();
            }

            foreach (var id in idVentas)
            {
                ventas.Add(VentaRepository.Get(id));
            }

            return ventas;
        }

        public static int Create(int _IdUsuario, string _Comentarios = "")
        {
            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("comentarios", System.Data.SqlDbType.VarChar) { Value = _Comentarios });
                cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int) { Value = _IdUsuario });
                cmd.CommandText = @"
									INSERT INTO Venta(Comentarios,IdUsuario)
									VALUES (@comentarios,@id); SELECT @@IDENTITY
                                ";
                int id = Convert.ToInt32(cmd.ExecuteScalar());
                if (id > 0) { return id; }
                connection.Close();
            }
            return 0;
        }

        public static void Create(Dictionary<int,int> _ProductoCantidad,string _Comentarios)
        {
            List<KeyValuePair<int, int>> myList = new List<KeyValuePair<int, int>>();
            myList = _ProductoCantidad.ToList();

            //Tomar el id usuario del primer producto agregado mediante un list
            //Console.WriteLine(ProductoRepository.Get(myList[0].Key).Usuario.ID);
            int idUsuario = ProductoRepository.Get(myList[0].Key).Usuario.ID;

            Venta _Venta = new Venta();
            int idVenta = VentaRepository.Create(idUsuario,_Comentarios);

            List<ProductoVendido> listaProductosVendidos = new List<ProductoVendido>();
            foreach (var products in _ProductoCantidad)
            {
                listaProductosVendidos.Add(ProductoVendidoRepository.Crear(products.Key, products.Value,idVenta));
            }

            _Venta.Productos = listaProductosVendidos;
            _Venta.Usuario = UsuarioRepository.Get(idUsuario);
            _Venta.Comentarios = _Comentarios;


        }//FALTA VALIDAR QUE SEAN PRODUCTOS DEL MISMO USUARIO

        public static void CreateII(List<KeyValuePair<int, int>> _ProductoCantidad, string _Comentarios)
        {
            List<ProductoVendido> listaProductosVendidos = new List<ProductoVendido>();
            Venta _Venta = new Venta();

            //Tomar el id usuario del primer producto agregado
            int idUsuario = ProductoRepository.Get(_ProductoCantidad[0].Key).Usuario.ID;
            int idVenta = VentaRepository.Create(idUsuario, _Comentarios);

            foreach (var products in _ProductoCantidad)
            {
                listaProductosVendidos.Add(ProductoVendidoRepository.Crear(products.Key, products.Value, idVenta));
            }

            _Venta.Productos = listaProductosVendidos;
            _Venta.Usuario = UsuarioRepository.Get(idUsuario);
            _Venta.Comentarios = _Comentarios;

        }

        public static void Delete(int _idParameter)
        {
            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int) { Value = _idParameter });
                cmd.CommandText = @"
                                    DELETE FROM Venta WHERE id = @id
                                ";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }



        /*
        public static List<Venta> GetByUserId(int _UserId)
        {
            List<Venta> ventas = new List<Venta>();
            Usuario user = UsuarioRepository.Get(_UserId);
            List<Venta> AllVentas = VentaRepository.Get();

            foreach(var venta in AllVentas)
            {
                if (venta.Usuario.ID == user.ID)
                {
                    ventas.Add(venta);
                    //Console.WriteLine(ventas.Last().ToString());
                }
            }
            return ventas;
        }//Optimizar
         
         */

    }
}