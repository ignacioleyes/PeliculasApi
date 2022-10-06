using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PeliculasApi.DTOs;
using PeliculasApi.Servicios.Interfaces;


namespace PeliculasApi.Controllers
{
    [ApiController]
    [Route("api/cuentas")]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentasServices cuentasServices;
        private readonly ICustomBaseServices customBaseServices;

        public CuentasController(
            ICuentasServices cuentasServices, ICustomBaseServices customBaseServices)
        {
            this.cuentasServices = cuentasServices;
            this.customBaseServices = customBaseServices;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            return await cuentasServices.CreateUser(model);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo model)
        {
            return await cuentasServices.Login(model);
        }

        [HttpPost("RenovarToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserToken>> Renovar()
        {
            return await cuentasServices.Renovar();
        }

        [HttpGet("Usuarios")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<List<UsuarioDTO>>> Get([FromQuery] BaseFilter baseFilter)
        {
            return await customBaseServices.Get<IdentityUser, UsuarioDTO>(baseFilter);
        }

        [HttpGet("Roles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult<List<string>>> GetRoles()
        {
            return await cuentasServices.GetRoles();
        }

        [HttpPost("AsignarRol")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> AsignarRol(EditarRolDTO editarRolDTO)
        {
            return await cuentasServices.AsignarRol(editarRolDTO);
        }

        [HttpPost("RemoveRol")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> RemoverRol(EditarRolDTO editarRolDTO)
        {
            return await cuentasServices.RemoverRol(editarRolDTO);
        }

    }
}
