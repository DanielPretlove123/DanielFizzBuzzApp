using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Application.Interfaces;
using InfoTrackFizzBuzz.Application.Services.GameRules.Dtos;
using InfoTrackFizzBuzz.Domain.Entities;

namespace InfoTrackFizzBuzz.Application.Services.GameRules.Create
{
    public record CreateGameRuleRequest : IRequest<GameRulesDto>
    {
        public int Divisor { get; init; }
        public string OutputText { get; init; } = string.Empty;
        public bool IsActive { get; init; } = true;
    }

    public class CreateGameRuleRequestHandler : IRequestHandler<CreateGameRuleRequest, GameRulesDto>
    {
        private readonly IGameRuleRepository _gameRuleRepository;
        private readonly IMapper _mapper;

        public CreateGameRuleRequestHandler(IGameRuleRepository gameRuleRepository, IMapper mapper)
        {
            _gameRuleRepository = gameRuleRepository;
            _mapper = mapper;
        }

        public async Task<GameRulesDto> Handle(CreateGameRuleRequest request, CancellationToken cancellationToken)
        {
            var entity = new GameRule
            {
                Divisor = request.Divisor,
                OutputText = request.OutputText,
                IsActive = request.IsActive
            };

            await _gameRuleRepository.AddAsync(entity);
            return _mapper.Map<GameRulesDto>(entity);
        }
    }
}
