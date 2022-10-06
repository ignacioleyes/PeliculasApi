using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.DTOs;
using PeliculasApi.Entidades;
using PeliculasApi.Extensions;
using PeliculasApi.Servicios.Interfaces;
using System.Security.Claims;

namespace PeliculasApi.Servicios
{
    public class ReviewServices: ControllerBase, IReviewServices
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly ICustomBaseServices customBaseServices;
        private readonly IActionContextAccessor actionContextAccessor;

        public ReviewServices(ApplicationDbContext context,
            IMapper mapper, ICustomBaseServices customBaseServices,
            IActionContextAccessor actionContextAccessor)
        {
            this.context = context;
            this.mapper = mapper;
            this.customBaseServices = customBaseServices;
            this.actionContextAccessor = actionContextAccessor;
        }

        public async Task<ActionResult<List<ReviewDTO>>> Get(int peliculaId, BaseFilter baseFilter)
        {
            var reviews = context.Reviews.AsQueryable();
            return await reviews.FilterSortPaginate<Review, ReviewDTO>(baseFilter, mapper, actionContextAccessor);

        }


        public async Task<ActionResult> Post(int peliculaId, [FromBody] ReviewCreacionDTO reviewCreacionDTO)
        {

            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await context.Users.FindAsync(usuarioId);

            var reviewExiste = await context.Reviews
                .AnyAsync(x => x.PeliculaId == peliculaId && x.UsuarioId == usuarioId);

            if (reviewExiste)
            {
                return BadRequest($"Ya existe un review del usuario {user.Email}");
            }

            var review = mapper.Map<Review>(reviewCreacionDTO);
            review.PeliculaId = peliculaId;
            review.UsuarioId = usuarioId;

            context.Add(review);
            await context.SaveChangesAsync();
            return NoContent();

        }

        public async Task<ActionResult> Put(int peliculaId, int reviewId,
            [FromBody] ReviewCreacionDTO reviewCreacionDTO)
        {

            var reviewDB = await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (reviewDB == null)
            {
                return NotFound();
            }

            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (reviewDB.UsuarioId != usuarioId)
            {
                return BadRequest("Notiene permiso para editar este review");
            }

            reviewDB = mapper.Map(reviewCreacionDTO, reviewDB);

            await context.SaveChangesAsync();
            return NoContent();
        }

        public async Task<ActionResult> Delete(int reviewId)
        {
            var reviewDB = await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);

            if (reviewDB == null)
            {
                return NotFound();
            }

            var usuarioId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;

            if (reviewDB.UsuarioId != usuarioId)
            {
                return Forbid();
            }

            context.Remove(reviewDB);
            await context.SaveChangesAsync();
            return NoContent();
        }
    }
}
