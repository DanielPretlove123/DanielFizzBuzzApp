using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Application.Interfaces;
using InfoTrackFizzBuzz.Application.Services.GameRounds.Dtos;
using InfoTrackFizzBuzz.Domain.Entities;

namespace InfoTrackFizzBuzz.Application.Services.GameRounds.Get
{
    public record GetGameChallengeRequest(Guid SessionId) : IRequest<GameChallengeResponseDto>;
    public class GetGameChallengeRequestHandler : IRequestHandler<GetGameChallengeRequest, GameChallengeResponseDto>
    {
        private readonly IGameSessionRepository _gameSessionRepository;
        private readonly IGameRuleRepository _gameRuleRepository;
        private readonly IGameRoundRepository _gameRoundRepository;
        private readonly Random _random = new();

        public GetGameChallengeRequestHandler(
            IGameSessionRepository gameSessionRepository,
            IGameRuleRepository gameRuleRepository,
            IGameRoundRepository gameRoundRepository)
        {
            _gameSessionRepository = gameSessionRepository;
            _gameRuleRepository = gameRuleRepository;
            _gameRoundRepository = gameRoundRepository;
        }

        public async Task<GameChallengeResponseDto> Handle(GetGameChallengeRequest request, CancellationToken cancellationToken)
        {
            var session = await _gameSessionRepository.GetByIdAsync(request.SessionId, cancellationToken);

            if (session == null)
            {
                throw new Exception($"Game session with ID {request.SessionId} not found.");
            }

            if (session.EndTime.HasValue)
            {
                throw new InvalidOperationException("This game session has already ended.");
            }

            var rules = await _gameRuleRepository
                .GetByCondition(x => x.IsActive == true)
                .ToListAsync();

            if (!rules.Any())
            {
                throw new InvalidOperationException("No active game rules found. Please create rules first.");
            }

            int challengeNumber = _random.Next(1, 101);
            string expectedAnswer = DetermineExpectedAnswer(challengeNumber, rules);

            var gameRound = new GameRound
            {
                GameSessionId = request.SessionId,
                ChallengeNumber = challengeNumber,
                ExpectedAnswer = expectedAnswer,
                Timestamp = DateTimeOffset.UtcNow
            };

            await _gameRoundRepository.AddAsync(gameRound, cancellationToken);

            return new GameChallengeResponseDto
            {
                RoundId = gameRound.Id,
                ChallengeNumber = challengeNumber
            };
        }

        private string DetermineExpectedAnswer(int number, IEnumerable<GameRule> rules)
        {
            var sb = new StringBuilder();
            bool ruleApplied = false;

            foreach (var rule in rules.OrderBy(r => r.Divisor))
            {
                if (number % rule.Divisor == 0)
                {
                    sb.Append(rule.OutputText);
                    ruleApplied = true;
                }
            }

            return ruleApplied ? sb.ToString() : number.ToString();
        }
    }
}
