using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;
using xUnitTestDemo.Models;
using xUnitTestDemo.Tests.Fixtures;

namespace xUnitTestDemo.Tests
{
    public class CatControllerTest
    {
        [Collection("Base collection")]
        public class CatControllerTests : IClassFixture<BaseFixture<TestStartup>>
        {
            private readonly string BaseEndpoint = "http://localhost/api/cat";
            protected TestServer Server { get; }
            protected HttpClient Client { get; }

            public CatControllerTests(BaseFixture<TestStartup> fixture)
            {
                Server = fixture.Server;
                Client = fixture.Client;
            }

            // GET
            [Fact]
            public async Task GetReturnsListWithoutParams()
            {
                // Act
                var response = await Client.GetAsync(BaseEndpoint);
                dynamic obj = JArray.Parse(await response.Content.ReadAsStringAsync());

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.True(obj.Count >= 0);
            }

            // GET ById
            [Fact]
            public async Task GetByIdReturnsResultWithExistingId()
            {
                // Arrange
                var EntityId = 1;

                // Act
                var response = await Client.GetAsync(BaseEndpoint + "/" + EntityId);
                dynamic obj = JObject.Parse(await response.Content.ReadAsStringAsync());

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(EntityId, (int)obj.id);
            }

            // POST
            [Fact]
            public async Task PostReturnsACreatedAtRouteResultWithCorrectInputs()
            {
                // Arrange
                var content = new StringContent(JsonConvert.SerializeObject(new Cat
                {
                    Name = "Mina",
                    Color = "Black&White"
                }), Encoding.UTF8, "application/json");

                // Act
                var response = await Client.PostAsync(BaseEndpoint, content);
                dynamic obj = JObject.Parse(await response.Content.ReadAsStringAsync());

                // Assert
                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
                Assert.NotEqual(0, (int)obj.id);
            }
        }
    }
}
