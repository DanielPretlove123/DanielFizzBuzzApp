using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Domain.Entities;

namespace InfoTrackFizzBuzz.Application.Interfaces
{
    public interface IGameRuleRepository : IRepository<GameRule>
    {
    }
}
