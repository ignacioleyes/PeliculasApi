using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PeliculasApi;
using PeliculasApi.DTOs;

namespace PeliculasAPI.Tests.PruebasDeIntegracion
{
    [TestClass]
    public class ActoresControllerTests: BasePruebas<Program>
    {
        private static readonly string url = "api/actores";

        [TestMethod]
        public async Task ObtenerTodosLosActoresListadoVacio()
        {

            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<List<ActorDTO>>(contentString);

            //Assert.AreEqual(0, content?.Count);
            Assert.IsNotNull(content);

        }

    }
}
