using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Application.Services.GameSessions.Create;
using InfoTrackFizzBuzz.Application.Services.GameSessions.Dtos;
using InfoTrackFizzBuzz.Application.Services.GameSessions.Get;
using InfoTrackFizzBuzz.Domain.Entities;
using MediatR;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using AutoMapper;
using InfoTrackFizzBuzz.Application.Interfaces;
using InfoTrackFizzBuzz.Application.Services.GameRounds.Dtos;

namespace InfoTrackFizzBuzz.Application.UnitTests
{
    [TestFixture]
    public class GameSessionsServiceTests
    {
        private Mock<ISender> _mockSender;
        private Mock<IGameSessionRepository> _mockGameSessionRepository;
        private Mock<IGameRuleRepository> _mockGameRuleRepository;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockSender = new Mock<ISender>();
            _mockGameSessionRepository = new Mock<IGameSessionRepository>();
            _mockGameRuleRepository = new Mock<IGameRuleRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Test]
        public async Task StartGameSession_ShouldReturnSessionDetails()
        {
            var sessionId = Guid.NewGuid();
            var expectedSession = new GameSessionResponseDto
            {
                SessionId = sessionId,
                StartTime = DateTimeOffset.UtcNow
            };

            var newSession = new GameSession
            {
                Id = sessionId,
                StartTime = expectedSession.StartTime
            };

            _mockGameSessionRepository
                .Setup(r => r.AddAsync(It.IsAny<GameSession>(), It.IsAny<CancellationToken>()))
                .Callback<GameSession, CancellationToken>((session, _) => session.Id = sessionId);

            _mockMapper
                .Setup(m => m.Map<GameSessionResponseDto>(It.Is<GameSession>(
                    s => s.Id == sessionId)))
                .Returns(expectedSession);

            _mockSender
                .Setup(s => s.Send(It.IsAny<StartGameSessionRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedSession);

            var result = await _mockSender.Object.Send(new StartGameSessionRequest());

            result.Should().BeEquivalentTo(expectedSession, options =>
                options.ComparingByMembers<GameSessionResponseDto>());
            Assert.That(result.SessionId, Is.Not.EqualTo(Guid.Empty));
            Assert.That(result.StartTime, Is.EqualTo(expectedSession.StartTime));
        }

        [Test]
        public async Task GetGameSessionDetails_ValidId_ShouldReturnDetails()
        {
            var sessionId = Guid.NewGuid();
            var startTime = DateTimeOffset.UtcNow.AddMinutes(-10);

            var gameSession = new GameSession
            {
                Id = sessionId,
                StartTime = startTime,
                TotalRounds = 10,
                CorrectAnswers = 7,
                EndTime = null
            };

            var expectedDetails = new GameSessionDetailsDto
            {
                SessionId = sessionId,
                StartTime = startTime,
                EndTime = null,
                TotalRounds = 10,
                CorrectAnswers = 7,
                Accuracy = 70.0,
                IsActive = true,
                Rounds = new List<GameRoundDto>()
            };

            _mockGameSessionRepository
                .Setup(r => r.GetByCondition(It.Is<System.Linq.Expressions.Expression<Func<GameSession, bool>>>(
                    expr => TestExpressionHelper.GetIdFromExpression(expr) == sessionId)))
                .Returns(new TestAsyncEnumerable<GameSession>(new[] { gameSession }));

            _mockMapper
                .Setup(m => m.Map<GameSessionDetailsDto>(It.Is<GameSession>(
                    s => s.Id == sessionId)))
                .Returns(expectedDetails);

            _mockSender
                .Setup(s => s.Send(
                    It.Is<GetGameSessionDetailsRequest>(r => r.SessionId == sessionId),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedDetails);

            var result = await _mockSender.Object.Send(new GetGameSessionDetailsRequest(sessionId));

            result.Should().BeEquivalentTo(expectedDetails, options =>
                options.ComparingByMembers<GameSessionDetailsDto>());

            Assert.That(result.TotalRounds, Is.EqualTo(10));
            Assert.That(result.CorrectAnswers, Is.EqualTo(7));
            Assert.That(result.Accuracy, Is.EqualTo(70.0));
            Assert.That(result.IsActive, Is.True);
        }

        private static class TestExpressionHelper
        {
            public static Guid GetIdFromExpression(System.Linq.Expressions.Expression<Func<GameSession, bool>> expr)
            {
                var memberExpression = expr.Body as System.Linq.Expressions.BinaryExpression;
                var constantExpression = memberExpression?.Right as System.Linq.Expressions.ConstantExpression;
                return constantExpression?.Value as Guid? ?? Guid.Empty;
            }
        }
    }
}
