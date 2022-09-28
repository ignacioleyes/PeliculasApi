using PeliculasApi.Entidades;
using System.ComponentModel.DataAnnotations;

namespace PeliculasApi.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string Comentario { get; set; }
        [Range(1, 5)]
        public int Puntuacion { get; set; }
        public int PeliculaId { get; set; }
        public Pelicula Pelicula { get; set; }
        public string UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
    }
}
