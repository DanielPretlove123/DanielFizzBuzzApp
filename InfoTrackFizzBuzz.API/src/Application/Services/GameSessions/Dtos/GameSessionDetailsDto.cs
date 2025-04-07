using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Application.Services.GameRounds.Dtos;
using InfoTrackFizzBuzz.Domain.Entities;

namespace InfoTrackFizzBuzz.Application.Services.GameSessions.Dtos
{
    public class GameSessionDetailsDto
    {
        public Guid SessionId { get; set; }
        public DateTimeOffset StartTime { get; set; }
        public DateTimeOffset? EndTime { get; set; }
        public int TotalRounds { get; set; }
        public int CorrectAnswers { get; set; }
        public double Accuracy { get; set; }
        public bool IsActive { get; set; }
        public List<GameRoundDto> Rounds { get; set; } = new();

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GameSession, GameSessionDetailsDto>();
            }
        }
    }
}
