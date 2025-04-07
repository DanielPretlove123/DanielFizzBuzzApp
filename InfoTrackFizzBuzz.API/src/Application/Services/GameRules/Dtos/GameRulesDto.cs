using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Domain.Entities;

namespace InfoTrackFizzBuzz.Application.Services.GameRules.Dtos
{
    public class GameRulesDto
    {
        public int Id { get; set; }
        public int Divisor { get; set; }
        public string OutputText { get; set; } = string.Empty;
        public bool IsActive { get; set; }


        private class Mapping : Profile
        {
            public Mapping() 
            {
                CreateMap<GameRule, GameRulesDto>();
            }
        }
    }
}
