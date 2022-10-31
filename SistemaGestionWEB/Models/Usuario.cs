using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaGestionWEB.Models
{
    public class Usuario
    {
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Contrasenia { get; set; }
        public string Mail { get; set; }

        public Usuario() { }
        public Usuario(int iD, string nombre, string apellido, string nombreUsuario, string contrasenia, string mail)
        {
            ID = iD;
            Nombre = nombre;
            Apellido = apellido;
            NombreUsuario = nombreUsuario;
            Contrasenia = contrasenia;
            Mail = mail;
        }

        public String ToString()
        {
            return
                ($"ID:{ID}\t" +
                $"Nombre:{Nombre}\t" +
                $"Nombre_Usuario:{NombreUsuario}\t" +
                $"Contrasenia:{Contrasenia}\t" +
                $"Mail:{Mail}");
        }
    }
}
