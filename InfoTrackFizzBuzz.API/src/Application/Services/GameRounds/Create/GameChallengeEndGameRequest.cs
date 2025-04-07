using InfoTrackFizzBuzz.Application.Interfaces;
using InfoTrackFizzBuzz.Application.Services.GameRounds.Dtos;

namespace InfoTrackFizzBuzz.Application.Services.GameRounds.End;

public class GameChallengeEndGameRequest : IRequest<EndGameResponseDto>
{
    public Guid SessionId { get; set; }
}


public class GameChallengeEndGameRequestHandler : IRequestHandler<GameChallengeEndGameRequest, EndGameResponseDto>
{
    private readonly IGameSessionRepository _gameSessionRepository;
    private readonly IMapper _mapper;

    public GameChallengeEndGameRequestHandler(
        IGameSessionRepository gameSessionRepository,
        IMapper mapper)
    {
        _gameSessionRepository = gameSessionRepository;
        _mapper = mapper;
    }

    public async Task<EndGameResponseDto> Handle(GameChallengeEndGameRequest request, CancellationToken cancellationToken)
    {
        var session = await _gameSessionRepository
            .GetByCondition(s => s.Id == request.SessionId)
            .Include(s => s.Rounds)
            .FirstOrDefaultAsync();

        if (session == null)
        {
            throw new Exception($"Game session with ID {request.SessionId} not found.");
        }

        if (session.EndTime.HasValue)
        {
            throw new InvalidOperationException("This game session has already ended.");
        }

        session.EndTime = DateTimeOffset.UtcNow;
        await _gameSessionRepository.UpdateAsync(session, cancellationToken);
        var responseDto = _mapper.Map<EndGameResponseDto>(session);

        responseDto.Accuracy = session.TotalRounds > 0
            ? Math.Round((double)session.CorrectAnswers / session.TotalRounds * 100, 2)
            : 0;

        return responseDto;
    }
}
