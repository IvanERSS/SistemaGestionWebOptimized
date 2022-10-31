using SistemaGestionWEB.Models;
using System.Data.SqlClient;

namespace SistemaGestionWEB.Repository
{
    public class UsuarioRepository
    {
        public static List<Usuario> Get()
        {
            var listaUsuarios = new List<Usuario>();

            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = @"
                                    SELECT 
										Id,
										Nombre,
										Apellido,
										NombreUsuario,
										Contraseña,
										Mail
									FROM USUARIO
                                   ";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var usuario = new Usuario();

                    usuario.ID = Convert.ToInt32(reader.GetValue(0));
                    usuario.Nombre = reader.GetValue(1).ToString();
                    usuario.Apellido = reader.GetValue(2).ToString();
                    usuario.NombreUsuario = reader.GetValue(3).ToString();
                    usuario.Contrasenia = reader.GetValue(4).ToString();
                    usuario.Mail = reader.GetValue(5).ToString();

                    listaUsuarios.Add(usuario);

                }

                foreach (var usuario in listaUsuarios)
                {
                    Console.WriteLine(usuario.ToString());
                }
                Console.WriteLine("\n");

                reader.Close();
                connection.Close();
            }

            return listaUsuarios;
        }

        public static Usuario Get(int _UserID)
        {
            var _Usuario = new Usuario();

            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int) { Value = _UserID });
                cmd.CommandText = @"
                                    SELECT 
										Id,
										Nombre,
										Apellido,
										NombreUsuario,
										Contraseña,
										Mail
									FROM USUARIO
									WHERE Id = @id
                                   ";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _Usuario.ID = Convert.ToInt32(reader.GetValue(0));
                    _Usuario.Nombre = reader.GetValue(1).ToString();
                    _Usuario.Apellido = reader.GetValue(2).ToString();
                    _Usuario.NombreUsuario = reader.GetValue(3).ToString();
                    _Usuario.Contrasenia = reader.GetValue(4).ToString();
                    _Usuario.Mail = reader.GetValue(5).ToString();
                }

                //Console.WriteLine(_Usuario.ToString() + "\n"); //Para verificar


                reader.Close();
                connection.Close();
            }

            return _Usuario;


        }

        public static Usuario Get(string _UserName)
        {
            var _Usuario = new Usuario();

            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("user", System.Data.SqlDbType.VarChar) { Value = _UserName });
                cmd.CommandText = @"
                                    SELECT 
										Id,
										Nombre,
										Apellido,
										NombreUsuario,
										Contraseña,
										Mail
									FROM 
										USUARIO
									WHERE 
										nombre = @user OR NombreUsuario = @user
                                   ";
                var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    _Usuario.ID = Convert.ToInt32(reader.GetValue(0));
                    _Usuario.Nombre = reader.GetValue(1).ToString();
                    _Usuario.Apellido = reader.GetValue(2).ToString();
                    _Usuario.NombreUsuario = reader.GetValue(3).ToString();
                    _Usuario.Contrasenia = reader.GetValue(4).ToString();
                    _Usuario.Mail = reader.GetValue(5).ToString();
                }

                //Verificar console
                //Console.WriteLine(_Usuario.ToString() + "\n");


                reader.Close();
                connection.Close();
            }

            return _Usuario;


        }

        public static bool Update(Usuario _UserParameter)
        {
            //if (0 == UsuarioRepository.Get(_UserParameter.ID).ID) { return false; }//Validaciones de que existe el usuario
            using (SqlConnection connection = RepositoryTools.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();

                cmd.Parameters.Add(new SqlParameter("id", System.Data.SqlDbType.Int) { Value = _UserParameter.ID });
                cmd.Parameters.Add(new SqlParameter("nombre", System.Data.SqlDbType.VarChar) { Value = _UserParameter.Nombre });
                cmd.Parameters.Add(new SqlParameter("apellido", System.Data.SqlDbType.VarChar) { Value = _UserParameter.Apellido });
                cmd.Parameters.Add(new SqlParameter("nombreUsuario", System.Data.SqlDbType.VarChar) { Value = _UserParameter.NombreUsuario });
                cmd.Parameters.Add(new SqlParameter("contrasenia", System.Data.SqlDbType.VarChar) { Value = _UserParameter.Contrasenia });
                cmd.Parameters.Add(new SqlParameter("mail", System.Data.SqlDbType.VarChar) { Value = _UserParameter.Mail });

                cmd.CommandText = @"
                                UPDATE Usuario
                                SET Nombre = @nombre, Apellido = @apellido, NombreUsuario = @nombreUsuario, Contraseña = @contrasenia, Mail = @mail
                                WHERE id = @id
                            ";

                if(cmd.ExecuteNonQuery() > 0) { return true; }
                connection.Close();
            }
            return false;
        }


    }
}
