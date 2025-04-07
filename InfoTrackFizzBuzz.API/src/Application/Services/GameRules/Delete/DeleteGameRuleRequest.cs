using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Application.Interfaces;

namespace InfoTrackFizzBuzz.Application.Services.GameRules.Delete
{
    public record DeleteGameRuleRequest(int Id) : IRequest;

    public class DeleteGameRuleRequestHandler : IRequestHandler<DeleteGameRuleRequest>
    {
        private readonly IGameRuleRepository _gameRuleRepository;

        public DeleteGameRuleRequestHandler(IGameRuleRepository gameRuleRepository)
        {
            _gameRuleRepository = gameRuleRepository;
        }

  

        public async Task Handle(DeleteGameRuleRequest request, CancellationToken cancellationToken)
        {
            var entity = await _gameRuleRepository
                .GetByCondition(x => x.Id == request.Id)
                .FirstOrDefaultAsync();
            if (entity != null)
            {
                await _gameRuleRepository.DeleteAsync(entity, cancellationToken);
            }
        }
    }
}
