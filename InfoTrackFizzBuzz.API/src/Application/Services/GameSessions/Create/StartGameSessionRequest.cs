using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Application.Interfaces;
using InfoTrackFizzBuzz.Application.Services.GameSessions.Dtos;
using InfoTrackFizzBuzz.Domain.Entities;

namespace InfoTrackFizzBuzz.Application.Services.GameSessions.Create
{
    public record StartGameSessionRequest : IRequest<GameSessionResponseDto>;

    public class StartGameSessionRequestHandler : IRequestHandler<StartGameSessionRequest, GameSessionResponseDto>
    {
        private readonly IGameSessionRepository _gameSessionRepository;
        private readonly IMapper _mapper;

        public StartGameSessionRequestHandler(
            IGameSessionRepository gameSessionRepository,
            IMapper mapper)
        {
            _gameSessionRepository = gameSessionRepository;
            _mapper = mapper;
        }

        public async Task<GameSessionResponseDto> Handle(StartGameSessionRequest request, CancellationToken cancellationToken)
        {
            var newSession = new GameSession
            {
                Id = Guid.NewGuid(),
                StartTime = DateTimeOffset.UtcNow,
                TotalRounds = 0,
                CorrectAnswers = 0
            };

            await _gameSessionRepository.AddAsync(newSession);
            await _gameSessionRepository.SaveChangesAsync(cancellationToken);

            if (newSession.Id == Guid.Empty)
            {
                throw new InvalidOperationException("Failed to generate a valid session ID");
            }

            return _mapper.Map<GameSessionResponseDto>(newSession);
        }
    }
}
