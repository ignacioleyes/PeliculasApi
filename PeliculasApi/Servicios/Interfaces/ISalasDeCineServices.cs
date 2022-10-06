using Microsoft.AspNetCore.Mvc;
using PeliculasApi.DTOs;

namespace PeliculasApi.Servicios.Interfaces
{
    public interface ISalasDeCineServices
    {
        Task<ActionResult<List<SalaDeCineCercanoDTO>>> Cercanos([FromQuery]
        SalaDeCineCercanoFiltroDTO filtro);
    }
}
