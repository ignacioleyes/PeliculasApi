using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using PeliculasApi.DTOs;

namespace PeliculasAPI.Tests.PruebasDeIntegracion
{
    [TestClass]
    public class ActoresControllerTests: BasePruebas<Program>
    {
        private static readonly string url = "api/actores";

        [TestMethod]
        public async Task GetRequestedActors_ShouldReturn200_And_RequestedActors()
        {

            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();
            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<List<ActorDTO>>(contentString);

            Assert.IsNotNull(content);

        }

        [TestMethod]
        public async Task GetRequestedActorById_ShouldReturn200_And_RequestedActor()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();
            var actorId = 1;
            var response = await client.GetAsync($"{url}/{actorId}");

            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();
            var requestedActor = JsonConvert.DeserializeObject<ActorDTO>(contentString);

            Assert.AreEqual(requestedActor?.Id, actorId);
        }

        [TestMethod]
        public async Task CreateActorRequest_ShouldReturn200_And_PendingActorRequest()
        {
            var factory = BuildWebApplicationFactory(Guid.NewGuid().ToString());
            var client = factory.CreateClient();

            var newActorRequestCreation = new ActorCreacionDTO() { Nombre = "asddd" };

            var response = await client.PostAsJsonAsync(url, newActorRequestCreation);
            response.EnsureSuccessStatusCode();

            var dataAsString = await response.Content.ReadAsStringAsync();
            var newActorRequest = JsonConvert.DeserializeObject<ActorDTO>(dataAsString);

            Assert.AreEqual(newActorRequest?.Nombre, newActorRequestCreation.Nombre);
        }

    }
}
