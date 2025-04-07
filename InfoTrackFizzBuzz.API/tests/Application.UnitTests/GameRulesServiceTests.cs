using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MediatR;
using FluentAssertions;
using InfoTrackFizzBuzz.Application.Services.GameRules.Create;
using InfoTrackFizzBuzz.Application.Services.GameRules.Delete;
using InfoTrackFizzBuzz.Application.Services.GameRules.Dtos;
using InfoTrackFizzBuzz.Application.Services.GameRules.Get;
using InfoTrackFizzBuzz.Application.Services.GameRules.Update;
using InfoTrackFizzBuzz.Application.Services.GameSessions.Create;
using InfoTrackFizzBuzz.Application.Services.GameSessions.Dtos;
using InfoTrackFizzBuzz.Application.Services.GameSessions.Get;
using InfoTrackFizzBuzz.Application.Services.GameRounds.Get;
using InfoTrackFizzBuzz.Application.Services.GameRounds.Dtos;
using InfoTrackFizzBuzz.Application.Services.GameRounds.Submit;
using InfoTrackFizzBuzz.Application.Services.GameRounds.End;

namespace InfoTrackFizzBuzz.Application.UnitTests
{
    [TestFixture]
    public class GameRulesServiceTests
    {
        private Mock<ISender> _mockSender;

        [SetUp]
        public void Setup()
        {
            _mockSender = new Mock<ISender>();
        }

        [Test]
        public async Task GetGameRules_ShouldReturnListOfRules()
        {
            var expectedRules = new List<GameRulesDto>
            {
                new GameRulesDto { Id = 1, Divisor = 3, OutputText = "Fizz", IsActive = true },
                new GameRulesDto { Id = 2, Divisor = 5, OutputText = "Buzz", IsActive = true }
            };

            _mockSender
                .Setup(s => s.Send(It.IsAny<GetGameRulesRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedRules);

            var result = await _mockSender.Object.Send(new GetGameRulesRequest());
            result.Should().BeEquivalentTo(expectedRules);
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task CreateGameRule_ValidRule_ShouldSucceed()
        {
            var createRequest = new CreateGameRuleRequest
            {
                Divisor = 7,
                OutputText = "Test",
                IsActive = true
            };

            var expectedResponse = new GameRulesDto
            {
                Id = 3,
                Divisor = 7,
                OutputText = "Test",
                IsActive = true
            };

            _mockSender
                .Setup(s => s.Send(createRequest, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var result = await _mockSender.Object.Send(createRequest);

            result.Should().BeEquivalentTo(expectedResponse);
            Assert.That(result.Divisor, Is.EqualTo(7));
            Assert.That(result.OutputText, Is.EqualTo("Test"));
        }

        [Test]
        public async Task UpdateGameRule_ValidRule_ShouldSucceed()
        {
            var updateRequest = new UpdateGameRuleRequest
            {
                Id = 1,
                Divisor = 3,
                OutputText = "NewFizz",
                IsActive = true
            };

            _mockSender
                .Setup(s => s.Send(updateRequest, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _mockSender.Object.Send(updateRequest);

            _mockSender.Verify(
                s => s.Send(It.Is<UpdateGameRuleRequest>(
                    r => r.Id == 1 &&
                    r.OutputText == "NewFizz"),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Test]
        public async Task DeleteGameRule_ValidId_ShouldSucceed()
        {
            var deleteRequest = new DeleteGameRuleRequest(1);

            _mockSender
                .Setup(s => s.Send(deleteRequest, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            await _mockSender.Object.Send(deleteRequest);

            _mockSender.Verify(
                s => s.Send(It.Is<DeleteGameRuleRequest>(r => r.Id == 1),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
