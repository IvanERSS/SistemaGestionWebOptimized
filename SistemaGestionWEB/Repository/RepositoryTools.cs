using SistemaGestionWEB.Models;
using System.Data.SqlClient;

namespace SistemaGestionWEB.Repository
{
    public static class RepositoryTools
    {
        public static SqlConnection GetConnection()
        {
            string connectionString = "Server=WORK-LAP-IERS\\SQLEXPRESS;Database=SistemaGestion;Trusted_Connection=True;";
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }

        public static Usuario Session(string userParameter, string passParameter)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.Parameters.Add(new SqlParameter("user", System.Data.SqlDbType.VarChar) { Value = userParameter });
                cmd.Parameters.Add(new SqlParameter("pass", System.Data.SqlDbType.VarChar) { Value = passParameter });

                cmd.CommandText = @"
                SELECT
	                id
                FROM
	                Usuario
				WHERE
					NombreUsuario = @user AND
					Contraseña = @pass
                ";

                int UserId = Convert.ToInt32(cmd.ExecuteScalar());

                if (UserId > 0) { return UsuarioRepository.Get(UserId); }
                else { return null; }

                connection.Close();
            }

            return null;
        }

    }
}
