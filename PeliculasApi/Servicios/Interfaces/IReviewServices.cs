using Microsoft.AspNetCore.Mvc;
using PeliculasApi.DTOs;

namespace PeliculasApi.Servicios.Interfaces
{
    public interface IReviewServices
    {
        Task<ActionResult<List<ReviewDTO>>> Get(int peliculaId, BaseFilter baseFilter);
        Task<ActionResult> Post(int peliculaId, [FromBody] ReviewCreacionDTO reviewCreacionDTO);
        Task<ActionResult> Put(int peliculaId, int reviewId,[FromBody] ReviewCreacionDTO reviewCreacionDTO);
        Task<ActionResult> Delete(int reviewId);
    }
}
