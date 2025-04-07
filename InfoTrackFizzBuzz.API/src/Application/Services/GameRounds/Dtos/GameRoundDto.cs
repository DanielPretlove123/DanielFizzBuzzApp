using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InfoTrackFizzBuzz.Domain.Entities;

namespace InfoTrackFizzBuzz.Application.Services.GameRounds.Dtos
{
    public class GameRoundDto
    {
        public int Id { get; set; }
        public Guid GameSessionId { get; set; }
        public virtual GameSession GameSession { get; set; } = null!;
        public int ChallengeNumber { get; set; }
        public string ExpectedAnswer { get; set; } = string.Empty;
        public string? PlayerAnswer { get; set; }
        public bool IsCorrect { get; set; }
        public DateTimeOffset Timestamp { get; set; } = DateTimeOffset.UtcNow;
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<GameRound, GameRoundDto>();
            }
        }
    }
}
