using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Application.Interfaces;
using InfoTrackFizzBuzz.Domain.Entities;
using InfoTrackFizzBuzz.Infrastructure.Data;

namespace InfoTrackFizzBuzz.Infrastructure.Repositories
{
    public class GameRuleRepository : EfRepository<GameRule>, IGameRuleRepository
    {
        public GameRuleRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
