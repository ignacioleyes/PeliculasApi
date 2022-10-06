using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PeliculasApi.DTOs;
using PeliculasApi.Servicios.Interfaces;

namespace PeliculasApi.Servicios
{
    public class SalasDeCineServices : ControllerBase, ISalasDeCineServices
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly GeometryFactory geometryFactory;

        public SalasDeCineServices(ApplicationDbContext context,
            IMapper mapper, GeometryFactory geometryFactory)
        {
            this.context = context;
            this.mapper = mapper;
            this.geometryFactory = geometryFactory;
        }

        public async Task<ActionResult<List<SalaDeCineCercanoDTO>>> Cercanos([FromQuery]
        SalaDeCineCercanoFiltroDTO filtro)
        {
            var ubicacionUsuario = geometryFactory.CreatePoint(new Coordinate(filtro.Longitud, filtro.Latitud));

            var salasDeCine = await context.SalasDeCine
                .OrderBy(x => x.Ubicacion.Distance(ubicacionUsuario))
                .Where(x => x.Ubicacion.IsWithinDistance(ubicacionUsuario, filtro.DistanciaEnKms * 1000))
                .Select(x => new SalaDeCineCercanoDTO
                {
                    Id = x.Id,
                    Nombre = x.Nombre,
                    Latitud = x.Ubicacion.Y,
                    Longitud = x.Ubicacion.X,
                    DistanciaEnMetros = Math.Round(x.Ubicacion.Distance(ubicacionUsuario))
                })
                .ToListAsync();

            return salasDeCine;
        }
    }
}
