using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PeliculasApi.DTOs;

namespace PeliculasAPI.Tests.PruebasDeIntegracion
{
    [TestClass]
    public class GenerosControllerTests : BasePruebas<Program>
    {
        private static readonly string url = "api/generos";

        [TestMethod]
        public async Task GetRequestedGenres_ShouldReturn200_And_RequestedGenres()
        {

            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<List<GeneroDTO>>(contentString);

            Assert.IsNotNull(content);

        }
    }
}
