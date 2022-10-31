using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionWEB.Models;
using SistemaGestionWEB.Repository;
using System.Diagnostics.CodeAnalysis;

namespace SistemaGestionWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet("all")]
        public List<ProductoVendido> Get()
        {
            return ProductoVendidoRepository.Get();
        }

        [HttpGet("GetByUserId")]
        public List<ProductoVendido> Get(int _UserId)
        {
            return ProductoVendidoRepository.GetByUserId(_UserId);
        }

        [HttpPost("create")]
        public void Create([FromBody] int idProducto,int idUsuario,int idVenta)
        {
            ProductoVendidoRepository.Crear(idProducto,idUsuario,idVenta);
        }
    }
}
