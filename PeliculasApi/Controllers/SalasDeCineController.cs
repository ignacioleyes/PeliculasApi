using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using PeliculasApi.Servicios.Interfaces;

namespace PeliculasApi.Controllers
{
    [ApiController]
    [Route("api/salasDeCine")]
    public class SalasDeCineController: ControllerBase
    {
        private readonly ISalasDeCineServices salasDeCineServices;
        private readonly ICustomBaseServices customBaseServices;

        public SalasDeCineController(ISalasDeCineServices salasDeCineServices, ICustomBaseServices customBaseServices)

        {
            this.salasDeCineServices = salasDeCineServices;
            this.customBaseServices = customBaseServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<SalaDeCineDTO>>> Get()
        {
            return await customBaseServices.Get<SalaDeCine, SalaDeCineDTO>();
        }

        [HttpGet("{id:int}", Name = "obtenerSalaDeCine")]
        public async Task<ActionResult<SalaDeCineDTO>> Get(int id)
        {
            return await customBaseServices.Get<SalaDeCine, SalaDeCineDTO>(id);
        }

        [HttpGet("cercanos")]
        public async Task<ActionResult<List<SalaDeCineCercanoDTO>>> Cercanos([FromQuery] 
        SalaDeCineCercanoFiltroDTO filtro)
        {
            return await salasDeCineServices.Cercanos(filtro);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] SalaDeCineCreacionDTO salaDeCineCreacionDTO)
        {
            return await customBaseServices.Post<SalaDeCineCreacionDTO, SalaDeCine, SalaDeCineDTO>(salaDeCineCreacionDTO, "obtenerSalaDeCine");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] SalaDeCineCreacionDTO salaDeCineCreacionDTO)
        {
            return await customBaseServices.Put<SalaDeCineCreacionDTO, SalaDeCine>(id, salaDeCineCreacionDTO);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            return await customBaseServices.Delete<SalaDeCine>(id);
        }

    }
}
