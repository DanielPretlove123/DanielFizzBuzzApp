using InfoTrackFizzBuzz.Application.Interfaces;
using InfoTrackFizzBuzz.Application.Services.GameRounds.Dtos;

namespace InfoTrackFizzBuzz.Application.Services.GameRounds.Submit;

public class SubmitGameChallengeAnswerRequest : IRequest<SubmitGameChallengeAnswerResponseDto>
{
    public int RoundId { get; set; }
    public string Answer { get; set; } = string.Empty;
}


public class SubmitGameChallengeAnswerRequestHandler : IRequestHandler<SubmitGameChallengeAnswerRequest, SubmitGameChallengeAnswerResponseDto>
{
    private readonly IGameRoundRepository _gameRoundRepository;
    private readonly IGameSessionRepository _gameSessionRepository;
    private readonly IMapper _mapper;

    public SubmitGameChallengeAnswerRequestHandler(
        IGameRoundRepository gameRoundRepository,
        IGameSessionRepository gameSessionRepository,
        IMapper mapper)
    {
        _gameRoundRepository = gameRoundRepository;
        _gameSessionRepository = gameSessionRepository;
        _mapper = mapper;
    }

    public async Task<SubmitGameChallengeAnswerResponseDto> Handle(SubmitGameChallengeAnswerRequest request, CancellationToken cancellationToken)
    {
        var round = await _gameRoundRepository
            .GetByCondition(x => x.Id == request.RoundId)
            .Include(x => x.GameSession)
            .FirstOrDefaultAsync();

        if (round == null)
        {
            throw new Exception($"Game round with ID {request.RoundId} not found.");
        }

        if (round.PlayerAnswer != null)
        {
            throw new InvalidOperationException("An answer has already been submitted for this round.");
        }

        if (round.GameSession.EndTime.HasValue)
        {
            throw new InvalidOperationException("This game session has already ended.");
        }

        round.PlayerAnswer = request.Answer;
        round.IsCorrect = string.Equals(round.ExpectedAnswer, request.Answer, StringComparison.OrdinalIgnoreCase);

        var session = round.GameSession;
        session.TotalRounds++;
        if (round.IsCorrect)
        {
            session.CorrectAnswers++;
        }

        await _gameRoundRepository.UpdateAsync(round, cancellationToken);
        await _gameSessionRepository.UpdateAsync(session, cancellationToken);
        
        return _mapper.Map<SubmitGameChallengeAnswerResponseDto>(round);
    }
}
