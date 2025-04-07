using InfoTrackFizzBuzz.Application.Services.GameRounds.Dtos;
using InfoTrackFizzBuzz.Application.Services.GameRounds.End;
using InfoTrackFizzBuzz.Application.Services.GameRounds.Get;
using InfoTrackFizzBuzz.Application.Services.GameRounds.Submit;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackFizzBuzz.Web.Endpoints;

public class GameRounds : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapGet(GetChallenge, "/challenge")
           .MapPost(SubmitAnswer, "/submit")
           .MapPost(EndGame, "/end");
    }

    public async Task<GameChallengeResponseDto> GetChallenge(
        [FromQuery] Guid sessionId,
        ISender sender)
    {
        return await sender.Send(new GetGameChallengeRequest(sessionId));
    }

    public async Task<SubmitGameChallengeAnswerResponseDto> SubmitAnswer(
        ISender sender,
        SubmitGameChallengeAnswerRequest request)
    {
        return await sender.Send(request);
    }

    public async Task<EndGameResponseDto> EndGame(
        ISender sender,
        GameChallengeEndGameRequest request)
    {
        return await sender.Send(request);
    }
}
