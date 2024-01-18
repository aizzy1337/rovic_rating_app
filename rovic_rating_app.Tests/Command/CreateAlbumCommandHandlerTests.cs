using AutoMapper;
using FluentAssertions;
using Moq;
using rovic_rating_app.Handlers;
using rovic_rating_app.Models.DTOs;
using rovic_rating_app.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rovic_rating_app.Tests.Command
{

    public class CreateAlbumCommandHandlerTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IMapper> _mapperMock;

        public CreateAlbumCommandHandlerTests()
        {
            _unitOfWorkMock = new();
            _mapperMock = new();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailueResult_UserNotExistingAsync()
        {
            // Arrange

            AlbumPostDTO albumPostDTO = new AlbumPostDTO
            {
                Title = "Test",
                Artist = "Test",
                ProductionYear = 1001,
                Cover = "Test",
                Rate = 10
            };

            var command = new CreateAlbumCommandRequest(albumPostDTO);
            var handler = new CreateAlbumCommandHandler(_unitOfWorkMock.Object, _mapperMock.Object);

            // Act

            var result = await handler.Handle(command, default);

            // Assert
            
            result.Id.Should().Be(0);
         }
    }
}
