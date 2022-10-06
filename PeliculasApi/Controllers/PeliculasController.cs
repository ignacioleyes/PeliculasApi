using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using PeliculasApi.Servicios.Interfaces;


namespace PeliculasApi.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController : ControllerBase
    {
        private readonly ICustomBaseServices customBaseServices;
        private readonly IPeliculasServices peliculasServices;

        public PeliculasController(
            ICustomBaseServices customBaseServices,
            IPeliculasServices peliculasServices)

        {
            this.customBaseServices = customBaseServices;
            this.peliculasServices = peliculasServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<PeliculaDTO>>> Get()
        {
            return await customBaseServices.Get<Pelicula, PeliculaDTO>();
        }

        [HttpGet("estrenosYCines")]
        public async Task<ActionResult<PeliculasIndexDTO>> EstrenosYCines()
        {
            return await peliculasServices.EstrenosYCines();
        }

        //[HttpGet("filtro")]
        //public async Task<ActionResult<List<PeliculaDTO>>> Filtrar([FromQuery] BaseFilter baseFilter)
        //{
        //    return await peliculasServices.Filtrar(baseFilter);
        //}

        [HttpGet("{id}", Name = "obtenerPelicula")]
        public async Task<ActionResult<PeliculaDetallesDTO>> Get(int id)
        {
            return await peliculasServices.Get(id);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            return await peliculasServices.Post(peliculaCreacionDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromForm] PeliculaCreacionDTO peliculaCreacionDTO)
        {
            return await peliculasServices.Put(id, peliculaCreacionDTO);
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaPatchDTO> patchDocument)
        {
            return await customBaseServices.Patch<Pelicula, PeliculaPatchDTO>(id, patchDocument);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
           return await customBaseServices.Delete<Pelicula>(id);
        }
    }
}
