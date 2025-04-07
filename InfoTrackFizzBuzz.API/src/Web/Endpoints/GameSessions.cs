
using InfoTrackFizzBuzz.Application.Services.GameSessions.Create;
using InfoTrackFizzBuzz.Application.Services.GameSessions.Dtos;
using InfoTrackFizzBuzz.Application.Services.GameSessions.Get;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackFizzBuzz.Web.Endpoints;

public class GameSessions : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
           .MapPost(StartGameSession, "/start")
           .MapGet(GetGameSessionSummaryDetails, "{id}");
    }

    public async Task<GameSessionResponseDto> StartGameSession(ISender sender)
    {
        return await sender.Send(new StartGameSessionRequest());
    }

    public async Task<GameSessionDetailsDto> GetGameSessionSummaryDetails(
        [FromRoute] Guid id,
        ISender sender)
    {
        return await sender.Send(new GetGameSessionDetailsRequest(id));
    }
}
