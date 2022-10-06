using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PeliculasApi.DTOs;
using PeliculasApi.Helpers;
using PeliculasApi.Servicios.Interfaces;

namespace PeliculasApi.Controllers
{
    [Route("api/peliculas/{peliculaId:int}/reviews")]
    [ServiceFilter(typeof(PeliculaExisteAttribute))]
    public class ReviewController: ControllerBase
    {
        private readonly IReviewServices reviewServices;

        public ReviewController(IReviewServices reviewServices)
        {
            this.reviewServices = reviewServices;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReviewDTO>>> Get(int peliculaId, 
            [FromQuery] BaseFilter baseFilter)
        {
            return await reviewServices.Get(peliculaId, baseFilter);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int peliculaId, [FromBody] ReviewCreacionDTO reviewCreacionDTO)
        {
            return await reviewServices.Post(peliculaId, reviewCreacionDTO);
        }

        [HttpPut("{reviewId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Put(int peliculaId, int reviewId, 
            [FromBody] ReviewCreacionDTO reviewCreacionDTO)
        {
            return await reviewServices.Put(peliculaId, reviewId, reviewCreacionDTO);
        }

        [HttpDelete("{reviewId:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int reviewId)
        {
            return await reviewServices.Delete(reviewId);
        }

    }
}
