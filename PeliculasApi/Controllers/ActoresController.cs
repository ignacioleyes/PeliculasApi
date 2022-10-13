using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using PeliculasApi.Servicios.Interfaces;

namespace PeliculasApi.Controllers
{
    [ApiController]
    [Route("api/actores")]
    public class ActoresController : ControllerBase
    {
        private readonly ICustomBaseServices customBaseServices;
        private readonly IActoresServices actoresServices;

        public ActoresController(ICustomBaseServices customBaseServices,
            IActoresServices actoresServices)
        {
            this.customBaseServices = customBaseServices;
            this.actoresServices = actoresServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get([FromQuery] BaseFilter baseFilter)
        {
            return await customBaseServices.Get<Actor, ActorDTO>();
        }

        [HttpGet("{id}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
          return await customBaseServices.Get<Actor, ActorDTO>(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            return await actoresServices.Post(actorCreacionDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO)
        {
            return await actoresServices.Put(id, actorCreacionDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<ActorPatchDTO> patchDocument)
        {
            return await customBaseServices.Patch<Actor, ActorPatchDTO>(id, patchDocument);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await customBaseServices.Delete<Actor>(id);
        }

    }
}
