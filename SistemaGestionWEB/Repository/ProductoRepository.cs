using SistemaGestionWEB.Models;
using System.Data.SqlClient;


namespace SistemaGestionWEB.Repository
{
    public static class ProductoRepository
    {
        public static List<Producto> Get()
        {
            var listaProductos = new List<Producto>();
            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"
                                    SELECT 
										id,
										Descripciones AS Producto,
										Costo,
										PrecioVenta,
										Stock,
										IdUsuario
									FROM 
										producto";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    Producto produc = new Producto()
                    {
                        ID = Convert.ToInt32(reader.GetValue(0)),
                        Descripciones = reader.GetValue(1).ToString(),
                        Costo = Convert.ToDouble(reader.GetValue(2)),
                        PrecioVenta = Convert.ToDouble(reader.GetValue(3)),
                        Stock = Convert.ToInt32(reader.GetValue(4)),
                        Usuario = UsuarioRepository.Get(Convert.ToInt32(reader.GetValue(5))),
                    };

                    listaProductos.Add(produc);

                }

                /*//Validaciones
                foreach (var product in listaProductos)
                {
                    Console.WriteLine(product.ToString());
                }
                Console.WriteLine("\n");
                */

                reader.Close();
                connection.Close();
            }

            return listaProductos;
        }

        public static Producto Get(int _ProductId)
        {
            Producto _Producto = new Producto();

            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(
                    new SqlParameter("id", System.Data.SqlDbType.Int) { Value = _ProductId }
                    );

                cmd.CommandText = @"
                                    SELECT 
										id,
										Descripciones AS Producto,
										Costo,
										PrecioVenta,
										Stock,
										IdUsuario
									FROM 
										PRODUCTO
                                    WHERE id = @id
                                    ";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producto producto = new Producto()
                    {
                        ID = Convert.ToInt32(reader.GetValue(0)),
                        Descripciones = reader.GetValue(1).ToString(),
                        Costo = Convert.ToDouble(reader.GetValue(2)),
                        PrecioVenta = Convert.ToDouble(reader.GetValue(3)),
                        Stock = Convert.ToInt32(reader.GetValue(4)),
                        Usuario = UsuarioRepository.Get(Convert.ToInt32(reader.GetValue(5))),
                    };
                    _Producto = producto;
                }
                /*
                //Validaciones en console
                Console.WriteLine(_Producto.ToString() + "\n");
                */
                reader.Close();
                connection.Close();
            }


            return _Producto;
        }

        public static List<Producto> GetByUserId(int UserIdParameter)
        {
            var listaProductos = new List<Producto>();
            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                cmd.Parameters.Add(
                    new SqlParameter("id", System.Data.SqlDbType.Int) { Value = UserIdParameter }
                    );

                cmd.CommandText = @"
                                    SELECT
										id,
										Descripciones AS Producto,
										Costo,
										PrecioVenta,
										Stock,
										IdUsuario
									FROM PRODUCTO
									WHERE idUsuario = @id
                                ";

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Producto produc = new Producto()
                    {
                        ID = Convert.ToInt32(reader.GetValue(0)),
                        Descripciones = reader.GetValue(1).ToString(),
                        Costo = Convert.ToDouble(reader.GetValue(2)),
                        PrecioVenta = Convert.ToDouble(reader.GetValue(3)),
                        Stock = Convert.ToInt32(reader.GetValue(4)),
                        Usuario = UsuarioRepository.Get(Convert.ToInt32(reader.GetValue(5))),
                    };

                    listaProductos.Add(produc);
                }

                foreach (var product in listaProductos)
                {
                    Console.WriteLine(product.ToString());
                }
                Console.WriteLine("\n");

                reader.Close();
                connection.Close();
            }


            return listaProductos;

        }

        public static bool Update(Producto _ProductParameter)
        {
            //if (0 == UsuarioRepository.Get(_ProductParameter.Usuario.ID).ID) { return false; }//Validaciones de que existe el producto
            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int) { Value = _ProductParameter.ID });
                cmd.Parameters.Add(new SqlParameter("descripciones", System.Data.SqlDbType.VarChar) { Value = _ProductParameter.Descripciones });
                cmd.Parameters.Add(new SqlParameter("costo", System.Data.SqlDbType.Decimal) { Value = _ProductParameter.Costo });
                cmd.Parameters.Add(new SqlParameter("precioVenta", System.Data.SqlDbType.Decimal) { Value = _ProductParameter.PrecioVenta });
                cmd.Parameters.Add(new SqlParameter("stock", System.Data.SqlDbType.Int) { Value = _ProductParameter.Stock });
                cmd.Parameters.Add(new SqlParameter("idUsuario", System.Data.SqlDbType.Int) { Value = _ProductParameter.Usuario.ID });

                cmd.CommandText = @"
                                UPDATE Producto
                                SET Descripciones = @descripciones, Costo = @costo, PrecioVenta = @precioVenta, Stock = @stock, IdUsuario = @idUsuario
                                WHERE id = @id
                            ";
                
                if(cmd.ExecuteNonQuery() > 0) { return true; }
                connection.Close();
            }
            return false;
        }

        public static void Delete(int _idParameter)
        {
            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int) { Value = _idParameter });
                cmd.CommandText = @"
                                    DELETE FROM Producto WHERE id = @id
                                ";
                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static bool Create(Producto _ProductParameter)
        {
            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("descripciones", System.Data.SqlDbType.VarChar) { Value = _ProductParameter.Descripciones });
                cmd.Parameters.Add(new SqlParameter("costo", System.Data.SqlDbType.Float) { Value = _ProductParameter.Costo });
                cmd.Parameters.Add(new SqlParameter("precioVenta", System.Data.SqlDbType.Float) { Value = _ProductParameter.PrecioVenta });
                cmd.Parameters.Add(new SqlParameter("stock", System.Data.SqlDbType.Int) { Value = _ProductParameter.Stock });
                cmd.Parameters.Add(new SqlParameter("idUsuario", System.Data.SqlDbType.Int) { Value = _ProductParameter.Usuario.ID });

                cmd.CommandText = @"
									INSERT INTO Producto (Descripciones,Costo,PrecioVenta,Stock,IdUsuario)
									VALUES (@descripciones,@costo,@precioVenta,@stock,@idUsuario)
                                ";
                if (cmd.ExecuteNonQuery() > 0) { return true; }
                connection.Close();
            }
            return false;
        }
    }
}
