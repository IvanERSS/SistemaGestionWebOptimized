using Microsoft.AspNetCore.Mvc;
using SistemaGestionWEB.Models;
using SistemaGestionWEB.Repository;

namespace SistemaGestionWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        [HttpGet("all")]
        public List<Venta> Get()
        {
            return VentaRepository.Get();
        }

        [HttpGet("getByUserId")]
        public List<Venta> GetByUserId(int _UserId)
        {
            return VentaRepository.GetByUserId(_UserId);

        }

        [HttpDelete("delete")]
        public void Delete(int _id)
        {
            VentaRepository.Delete(_id);
        }

        [HttpPost("create")]
        public void Crear([FromBody] Dictionary<int, int> productoCantidad, string coments)
        {
            VentaRepository.Create(productoCantidad, coments);
        }

        /*
        [HttpPost("createII")]
        public void CreateII(List<KeyValuePair<int, int>> _ProductoCantidad, string _Comentarios)
        {
            VentaRepository.CreateII(_ProductoCantidad, _Comentarios);
        }
        */
    }
}
