using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Application.Interfaces;
using InfoTrackFizzBuzz.Application.Services.GameRules.Dtos;
using InfoTrackFizzBuzz.Application.Services.GameSessions.Dtos;

namespace InfoTrackFizzBuzz.Application.Services.GameSessions.Get
{
    public record GetGameSessionDetailsRequest(Guid SessionId) : IRequest<GameSessionDetailsDto>;

    public class GetGameSessionDetailsRequestHandler : IRequestHandler<GetGameSessionDetailsRequest, GameSessionDetailsDto>
    {
        private readonly IGameSessionRepository _gameSessionRepository;
        private readonly IGameRuleRepository _gameRuleRepository;
        private readonly IMapper _mapper;

        public GetGameSessionDetailsRequestHandler(
            IGameSessionRepository gameSessionRepository,
            IGameRuleRepository gameRuleRepository,
            IMapper mapper)
        {
            _gameSessionRepository = gameSessionRepository;
            _gameRuleRepository = gameRuleRepository;
            _mapper = mapper;
        }

        public async Task<GameSessionDetailsDto> Handle(GetGameSessionDetailsRequest request, CancellationToken cancellationToken)
        {
            var session = await _gameSessionRepository
                .GetByCondition(x => x.Id == request.SessionId)
                .FirstOrDefaultAsync();

            if (session == null)
            {
                throw new Exception($"Game session with ID {request.SessionId} not found.");
            }

            var sessionDto = _mapper.Map<GameSessionDetailsDto>(session);

            sessionDto.Accuracy = session.TotalRounds > 0
                ? Math.Round((double)session.CorrectAnswers / session.TotalRounds * 100, 2)
                : 0;

            sessionDto.IsActive = session.EndTime == null;

            return sessionDto;
        }
    }
}
