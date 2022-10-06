using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using PeliculasApi.Servicios.Interfaces;

namespace PeliculasApi.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GenerosController : ControllerBase
    {
        private readonly ICustomBaseServices customBaseServices;

        public GenerosController(ICustomBaseServices customBaseServices)

        {
            this.customBaseServices = customBaseServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            return await customBaseServices.Get<Genero, GeneroDTO>();
        }

        [HttpGet("{id:int}", Name = "obtenerGenero")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            return await customBaseServices.Get<Genero, GeneroDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            return await customBaseServices.Post<GeneroCreacionDTO, Genero, GeneroDTO>(generoCreacionDTO, "obtenerGenero");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            return await customBaseServices.Put<GeneroCreacionDTO, Genero>(id, generoCreacionDTO);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            return await customBaseServices.Delete<Genero>(id);
        }


    }
}
