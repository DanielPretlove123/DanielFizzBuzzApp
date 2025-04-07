
using InfoTrackFizzBuzz.Application.Services.GameRules.Create;
using InfoTrackFizzBuzz.Application.Services.GameRules.Delete;
using InfoTrackFizzBuzz.Application.Services.GameRules.Dtos;
using InfoTrackFizzBuzz.Application.Services.GameRules.Get;
using InfoTrackFizzBuzz.Application.Services.GameRules.Update;
using Microsoft.AspNetCore.Mvc;

namespace InfoTrackFizzBuzz.Web.Endpoints;

public class GameRules : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapGet(GetGameRules)
            .MapPost(CreateGameRules)
            .MapPut(UpdateGameRules, "{id}")
            .MapDelete(DeleteGameRules, "{id}");
    }


    public async Task<List<GameRulesDto>> GetGameRules(ISender sender)
    {
        return await sender.Send(new GetGameRulesRequest());
    }

    public async Task<GameRulesDto> CreateGameRules(ISender sender, CreateGameRuleRequest request)
    {
        return await sender.Send(request);
    }

    public async Task<IResult> UpdateGameRules(ISender sender, UpdateGameRuleRequest request)
    {
        await sender.Send(request);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteGameRules(ISender sender, int id)
    {
        await sender.Send(new DeleteGameRuleRequest(id));
        return Results.NoContent();
    }

}
