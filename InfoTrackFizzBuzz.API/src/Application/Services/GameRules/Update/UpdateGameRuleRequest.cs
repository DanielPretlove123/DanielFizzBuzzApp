using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Application.Interfaces;

namespace InfoTrackFizzBuzz.Application.Services.GameRules.Update
{
    public record UpdateGameRuleRequest : IRequest
    {
        public int Id { get; init; }
        public int Divisor { get; init; }
        public string OutputText { get; init; } = string.Empty;
        public bool IsActive { get; init; }
    }

    public class UpdateGameRuleRequestHandler : IRequestHandler<UpdateGameRuleRequest>
    {
        private readonly IGameRuleRepository _gameRuleRepository;

        public UpdateGameRuleRequestHandler(IGameRuleRepository gameRuleRepository)
        {
            _gameRuleRepository = gameRuleRepository;
        }

        public async Task Handle(UpdateGameRuleRequest request, CancellationToken cancellationToken)
        {
            var entity = await _gameRuleRepository.GetByIdAsync(request.Id, cancellationToken)
                ?? throw new Exception($"Game rule with ID {request.Id} not found");

            entity.Divisor = request.Divisor;
            entity.OutputText = request.OutputText;
            entity.IsActive = request.IsActive;

            await _gameRuleRepository.UpdateAsync(entity, cancellationToken);
        }
    }
}
