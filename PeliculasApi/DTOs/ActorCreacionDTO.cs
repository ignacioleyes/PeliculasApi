using PeliculasApi.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.DTOs
{
    public class ActorCreacionDTO: ActorPatchDTO // con esta herencia actorCreacionDTO hereda nombre y fecha de nac. de actorPatchDTO
    {
        
        [PesoArchivoValidacion(PesoMaximoEnMegaBytes: 4)]
        [TipoArchivoValidacion(grupoTipoArchivo: GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
