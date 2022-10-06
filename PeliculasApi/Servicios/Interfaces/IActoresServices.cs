using Microsoft.AspNetCore.Mvc;
using PeliculasApi.DTOs;

namespace PeliculasApi.Servicios.Interfaces
{
    public interface IActoresServices
    {
        Task<ActionResult> Post([FromForm] ActorCreacionDTO actorCreacionDTO);
        Task<ActionResult> Put(int id, [FromForm] ActorCreacionDTO actorCreacionDTO);
    }
}
