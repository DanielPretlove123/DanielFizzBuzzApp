using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Application.Interfaces;
using InfoTrackFizzBuzz.Application.Services.GameRules.Dtos;

namespace InfoTrackFizzBuzz.Application.Services.GameRules.Get
{
    public record GetGameRulesRequest : IRequest<List<GameRulesDto>>;

    public class GetGameRulesRequestHandler : IRequestHandler<GetGameRulesRequest, List<GameRulesDto>>
    {
        private readonly IGameRuleRepository _gameRuleRepository;
        private readonly IMapper _mapper;

        public GetGameRulesRequestHandler(IGameRuleRepository gameRuleRepository, IMapper mapper)
        {
            _gameRuleRepository = gameRuleRepository;
            _mapper = mapper;
        }

        public async Task<List<GameRulesDto>> Handle(GetGameRulesRequest request, CancellationToken cancellationToken)
        {
            var rules = await _gameRuleRepository
                .GetAll()
                .ToListAsync();

            return _mapper.Map<List<GameRulesDto>>(rules);
        }
    }
}
