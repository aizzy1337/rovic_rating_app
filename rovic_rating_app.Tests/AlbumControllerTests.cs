using FluentAssertions;
using rovic_rating_app.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace rovic_rating_app.Tests
{
    public class AlbumControllerTests
    {
        [Fact]
        public async Task AlbumPost_AddsAlbumToDatabase()
        {
            // Arrange

            var application = new RovicWebApplicationFactory();

            AlbumPostDTO albumPostDTO = new AlbumPostDTO
                { Title = "Test",
                Artist = "Test",
                ProductionYear = 1001,
                Cover = "Test",
                Rate = 10,
                UserId = 1 };

            var client = application.CreateClient();

            // Act

            var response = await client.PostAsJsonAsync("/api/album", albumPostDTO);

            // Assert

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AlbumGetDTO>();

            result?.Id.Should().BePositive();
            result?.Tags.Should().HaveCount(0);
        }
    }
}
