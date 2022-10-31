using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaGestionWEB.Models;
using SistemaGestionWEB.Repository;

namespace SistemaGestionWEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet("Session")]
        public Usuario Session(string _User, string _Pass)
        {
            return RepositoryTools.Session(_User,_Pass);
        }
    }
}
