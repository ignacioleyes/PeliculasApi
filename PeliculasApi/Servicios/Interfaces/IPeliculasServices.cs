using Microsoft.AspNetCore.Mvc;
using PeliculasApi.DTOs;

namespace PeliculasApi.Servicios.Interfaces
{
    public interface IPeliculasServices
    {
        Task<ActionResult<PeliculasIndexDTO>> EstrenosYCines();
        //Task<ActionResult<List<PeliculaDTO>>> Filtrar([FromQuery] FiltroPeliculasDTO filtroPeliculasDTO);
        Task<ActionResult<PeliculaDetallesDTO>> Get(int id);
        Task<ActionResult> Post([FromForm] PeliculaCreacionDTO peliculaCreacionDTO);
        Task<ActionResult> Put(int id, [FromForm] PeliculaCreacionDTO peliculaCreacionDTO);
    }
}
