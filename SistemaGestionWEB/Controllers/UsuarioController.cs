using Microsoft.AspNetCore.Mvc;
using SistemaGestionWEB.Models;
using SistemaGestionWEB.Repository;

namespace SistemaGestionWEB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        //METODOS GET
        [HttpGet("all")]
        public List<Usuario> GetAll()
        {
            return UsuarioRepository.Get();
        }
        
        [HttpGet("id")]
        public Usuario GetById(int id)
        {
            return UsuarioRepository.Get(id);
        }

        [HttpGet("name")]
        public Usuario GetByName(string name)
        {
            return UsuarioRepository.Get(name);
        }


        //METODOS PUT
        [HttpPut("update")]
        public void Update([FromBody] Usuario user)
        {
            UsuarioRepository.Update(user);
        }

        //METODOS DELETE
        /*
        [HttpDelete()]
        public void Eliminar()
        {

        }

        */

    }
}
