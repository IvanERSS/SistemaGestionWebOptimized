using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionWEB.Models;
using SistemaGestionWEB.Repository;

namespace SistemaGestionWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        [HttpGet ("all")]
        public List<Producto> Get()
        {
            return ProductoRepository.Get();
        }

        [HttpGet("getByUserId")]
        public List<Producto> GetByUserId(int _id)
        {
            return ProductoRepository.GetByUserId(_id);
        }

        [HttpPut("update")]
        public void Update([FromBody]Producto _Producto)
        {
            ProductoRepository.Update(_Producto);
        }

        [HttpDelete("delete")]
        public void Delete([FromBody] int _ProductId)
        {
            ProductoRepository.Delete(_ProductId);
        }

        [HttpPost("create")]
        public void Create([FromBody] Producto _Producto)
        {
            ProductoRepository.Create(_Producto);
        }

    }
}
