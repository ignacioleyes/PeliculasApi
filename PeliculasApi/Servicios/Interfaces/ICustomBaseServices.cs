using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;

namespace PeliculasApi.Servicios.Interfaces
{
    public interface ICustomBaseServices
    {
        Task<List<TDTO>> Get<TEntidad, TDTO>() where TEntidad : class;
        Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class, IId;
        Task<List<TDTO>> Get<TEntidad, TDTO>(BaseFilter baseFilter) where TEntidad : class;
        //Task<List<TDTO>> Get<TEntidad, TDTO>(BaseFilter baseFilter, IQueryable<TEntidad> queryable) where TEntidad : class;
        Task<ActionResult> Post<TCreacion, TEntidad, TLectura>(TCreacion creacionDTO, string nombreRuta) where TEntidad : class, IId;
        Task<ActionResult> Put<TCreacion, TEntidad>(int id, TCreacion creacionDTO) where TEntidad : class, IId;
        Task<ActionResult> Patch<TEntidad, TDTO>(int id, JsonPatchDocument<TDTO> patchDocument) where TDTO : class
        where TEntidad : class, IId;
        Task<ActionResult> Delete<TEntidad>(int id) where TEntidad : class, IId, new();

    }
}
