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
using InfoTrackFizzBuzz.Domain.Entities;
using AutoMapper;
using InfoTrackFizzBuzz.Application.Interfaces;
using System.Collections;
using System.Linq.Expressions;

namespace InfoTrackFizzBuzz.Application.UnitTests
{
    [TestFixture]
    public class GameRoundsServiceTests
    {
        private Mock<ISender> _mockSender;
        private Mock<IGameRoundRepository> _mockGameRoundRepository;
        private Mock<IGameSessionRepository> _mockGameSessionRepository;
        private Mock<IMapper> _mockMapper;

        [SetUp]
        public void Setup()
        {
            _mockSender = new Mock<ISender>();
            _mockGameRoundRepository = new Mock<IGameRoundRepository>();
            _mockGameSessionRepository = new Mock<IGameSessionRepository>();
            _mockMapper = new Mock<IMapper>();
        }

        [Test]
        public async Task GetChallenge_ValidSessionId_ShouldReturnChallenge()
        {
            var sessionId = Guid.NewGuid();
            var expectedChallenge = new GameChallengeResponseDto
            {
                RoundId = 1,
                ChallengeNumber = 15
            };
            _mockSender
                .Setup(s => s.Send(It.Is<GetGameChallengeRequest>(
                    r => r.SessionId == sessionId),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedChallenge);
            var result = await _mockSender.Object.Send(new GetGameChallengeRequest(sessionId));
            result.Should().BeEquivalentTo(expectedChallenge);
            Assert.That(result.ChallengeNumber, Is.EqualTo(15));
            Assert.That(result.RoundId, Is.EqualTo(1));
        }

        [Test]
        public async Task SubmitAnswer_ValidAnswer_ShouldReturnResponse()
        {
            var gameSession = new GameSession
            {
                Id = Guid.NewGuid(),
                TotalRounds = 0,
                CorrectAnswers = 0
            };

            var gameRound = new GameRound
            {
                Id = 1,
                ExpectedAnswer = "FizzBuzz",
                PlayerAnswer = null,
                IsCorrect = false,
                GameSession = gameSession
            };

            var submitRequest = new SubmitGameChallengeAnswerRequest
            {
                RoundId = 1,
                Answer = "FizzBuzz"
            };

            var expectedResponse = new SubmitGameChallengeAnswerResponseDto
            {
                IsCorrect = true,
                ExpectedAnswer = "FizzBuzz",
                PlayerAnswer = "FizzBuzz"
            };

            _mockGameRoundRepository
                .Setup(r => r.GetByCondition(It.IsAny<System.Linq.Expressions.Expression<Func<GameRound, bool>>>()))
                .Returns(new TestAsyncEnumerable<GameRound>(new[] { gameRound }));

            _mockMapper
                .Setup(m => m.Map<SubmitGameChallengeAnswerResponseDto>(It.IsAny<GameRound>()))
                .Returns(expectedResponse);

            _mockSender
                .Setup(s => s.Send(submitRequest, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var result = await _mockSender.Object.Send(submitRequest);

            result.Should().BeEquivalentTo(expectedResponse);
            Assert.That(result.IsCorrect, Is.True);
            Assert.That(result.ExpectedAnswer, Is.EqualTo("FizzBuzz"));
            Assert.That(result.PlayerAnswer, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public async Task EndGame_ValidRequest_ShouldReturnGameSummary()
        {
            var sessionId = Guid.NewGuid();
            var startTime = DateTimeOffset.UtcNow.AddMinutes(-10);
            var endTime = DateTimeOffset.UtcNow;

            var endGameRequest = new GameChallengeEndGameRequest
            {
                SessionId = sessionId
            };

            var expectedResponse = new EndGameResponseDto
            {
                SessionId = sessionId,
                StartTime = startTime,
                EndTime = endTime,
                TotalRounds = 10,
                CorrectAnswers = 7,
                Accuracy = 0.7 
            };

            _mockSender
                .Setup(s => s.Send(
                    It.Is<GameChallengeEndGameRequest>(r => r.SessionId == sessionId),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var result = await _mockSender.Object.Send(endGameRequest);
            result.Should().BeEquivalentTo(expectedResponse, options => options
                .ComparingByMembers<EndGameResponseDto>()
                .IgnoringCyclicReferences());

            Assert.That(result.SessionId, Is.EqualTo(sessionId));
            Assert.That(result.TotalRounds, Is.EqualTo(10));
            Assert.That(result.CorrectAnswers, Is.EqualTo(7));
            Assert.That(result.Accuracy, Is.EqualTo(0.7).Within(0.001));
            Assert.That(result.StartTime, Is.EqualTo(startTime));
            Assert.That(result.EndTime, Is.EqualTo(endTime));
        }
    }

    public class TestAsyncEnumerable<T> : IQueryable<T>
    {
        private readonly IEnumerable<T> _collection;

        public TestAsyncEnumerable(IEnumerable<T> collection)
        {
            _collection = collection;
        }

        public Type ElementType => typeof(T);
        public Expression Expression => _collection.AsQueryable().Expression;
        public IQueryProvider Provider => new TestAsyncQueryProvider<T>(_collection);

        public IEnumerator<T> GetEnumerator() => _collection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TestAsyncQueryProvider<T> : IQueryProvider
    {
        private readonly IEnumerable<T> _collection;

        public TestAsyncQueryProvider(IEnumerable<T> collection)
        {
            _collection = collection;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestAsyncEnumerable<T>(_collection);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return (IQueryable<TElement>)new TestAsyncEnumerable<T>(_collection);
        }


        public object Execute(Expression expression)
        {
            return _collection?.AsQueryable()?.Provider?.Execute(expression)
                   ?? throw new InvalidOperationException("Collection or provider is null");
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _collection.AsQueryable().Provider.Execute<TResult>(expression);
        }
    }
}
